using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace SigStat.Benchmark.Helpers
{
    /// <summary>
    /// dal for sigstat benchmarks database
    /// </summary>
    public static class BenchmarkDatabase
    {
        private static MongoClient client;
        private static IMongoDatabase db;
        private static IMongoCollection<BsonDocument> experimentCollection;

        private static readonly Expression<Func<BsonDocument, bool>> queuedFilter = d =>
                d["results"] == BsonNull.Value &&
                d["errorLog"] == BsonNull.Value &&
                d["lockDate"] == BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> lockedFilter = d =>
                d["results"] == BsonNull.Value &&
                d["errorLog"] == BsonNull.Value &&
                d["lockDate"] != BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> faultedFilter = d =>
                d["errorLog"] != BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> finishedFilter = d =>
                d["results"] != BsonNull.Value;

        /// <summary>
        /// This filter ensures to only lock items that are in the queue or have been locked for 1+ hours.
        /// </summary>
        /// <remarks>
        /// Since BsonDateTime comparison does not work in Expression filters, this must be defined with Filter Builders.
        /// </remarks>
        private static readonly FilterDefinition<BsonDocument> lockableFilter = Builders<BsonDocument>.Filter
            .Or(
                queuedFilter,
                Builders<BsonDocument>.Filter.And(
                    lockedFilter,
                    Builders<BsonDocument>.Filter.Lt(d => d["lockDate"], new BsonDateTime(DateTime.Now.AddHours(-1)))
            ));

        public static async Task InitializeConnection(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (!connectionString.StartsWith("mongodb://"))
            {
                try
                {
                    connectionString = await File.ReadAllTextAsync(connectionString);
                }
                catch (IOException exc)
                {
                    throw new IOException($"Couldn't load connection string from '{connectionString}'", exc);
                }
            }


            client = new MongoClient(connectionString);
            db = client.GetDatabase("Benchmark");
            experimentCollection = db.GetCollection<BsonDocument>(Program.Experiment);


        }

        public static async Task<bool> ExperimentExists()
        {
            return await (await db.ListCollectionsAsync(new ListCollectionsOptions
            {
                Filter = new BsonDocument("name", Program.Experiment)
            })).AnyAsync();
        }

        /// <summary>
        /// Insert congifurations if they don't exist already.
        /// Due to the 400 RU/s restriction, documents are sent in small batches 
        /// and client-side throttling is applied when required.
        /// Note: This method does not remove locks, results and logs on existing items.
        /// </summary>
        /// <param name="configs"></param>
        /// <returns>Number of new configurations inserted.</returns>
        public static async Task<int> UpsertConfigs(IEnumerable<string> configs, int batchSize = 10)
        {
            int totalCount = configs.Count();
            int processedCount = 0;
            int lastProcessedCount = 0;
            int throttlingDelay = 0;
            Stopwatch sw = Stopwatch.StartNew();

            foreach (var configBatch in configs.ToArrays(batchSize))
            {
                //create write models
                var bulkOps = configBatch.Select(config => new UpdateOneModel<BsonDocument>(
                    Builders<BsonDocument>.Filter.Where(d => d["config"] == config),
                    Builders<BsonDocument>.Update.SetOnInsert(d => d["config"], config))
                { IsUpsert = true });
                bool isAcknowledged = false;
                int retryCount = 21;

                while (!isAcknowledged && retryCount > 0)
                {
                    await Task.Delay(throttlingDelay);
                    retryCount--;
                    try
                    {
                        var res = await experimentCollection.BulkWriteAsync(bulkOps,
                            new BulkWriteOptions { IsOrdered = false });//enables parallel execution

                        if (res.IsAcknowledged)
                        {
                            isAcknowledged = true;
                            processedCount += configBatch.Length;
                        }
                        else
                        {
                            Console.WriteLine($"Operation was not acknowledged. Retrying ({retryCount})...");
                        }

                    }
                    catch (MongoCommandException exc)
                    {
                        if (exc.Code != 16500)
                            throw;
                        throttlingDelay += 10;
                        Console.WriteLine($"Throttling limit reached. Adjusting delay between calls to {throttlingDelay} milliseconds. Retrying ({retryCount})...");
                        await Task.Delay(1000);
                    }
                }

                if (retryCount == 0)
                    throw new Exception("Retry limit exceeded");

                if (sw.Elapsed.Seconds > 10)
                {
                    var rps = (processedCount - lastProcessedCount) * 1000 / sw.ElapsedMilliseconds;
                    lastProcessedCount = processedCount;
                    sw.Restart();
                    Console.WriteLine($"{DateTime.Now}: {processedCount}/{totalCount} ({rps}/second)");
                }
            }
            return processedCount; // configs.Count()/* - matchedCnt*/;//TODO: fix: First request matches more configs???
        }

        /// <summary>
        /// This is atomic. Returns null if no config can be locked
        /// </summary>
        /// <returns>Configuration string</returns>
        public static async Task<string> LockNextConfig(int procId)
        {
            int retryCount = 21;
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    var result = await experimentCollection.FindOneAndUpdateAsync(lockableFilter,
                        Builders<BsonDocument>.Update
                            .Set("lockDate", DateTime.Now)
                            .Set("machine", Environment.MachineName)
                            .Set("procId", procId),
                        new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });

                    if (result == null)
                        return null;
                    else
                        return (string)result["config"];
                }
                catch (MongoCommandException exc)
                {
                    if (exc.Code != 16500)
                        throw;
                    Console.WriteLine($"Throttling limit reached. Retrying ({retryCount})...");
                    await Task.Delay(new Random().Next(10000));
                }
            }
            throw new Exception("Retry limit exceeded");
        }

        /// <summary>
        /// Add results to a specified benchmark item.
        /// </summary>
        public static async Task SendResults(int procId, string benchmarkConfig, BenchmarkLogModel results)
        {
            int retryCount = 21;
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    var bsonResults = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize(results));

            var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["config"] == benchmarkConfig && d["procId"] == procId && d["machine"] == Environment.MachineName,
                Builders<BsonDocument>.Update
                    .Set("end_date", DateTime.Now)
                    .Set("resultType", "Success")
                    .Set("results", bsonResults),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });

                }
                catch (MongoCommandException exc)
                {
                    if (exc.Code != 16500)
                        throw;
                    Console.WriteLine($"Throttling limit reached. Retrying ({retryCount})...");
                    await Task.Delay(new Random().Next(10000));
                }
            }
            throw new Exception("Retry limit exceeded");
        }

        /// <summary>
        /// Send log after exception.
        /// </summary>
        public static async Task SendErrorLog(int procId, string benchmarkConfig, string logString)
        {
            int retryCount = 21;
            while (retryCount > 0)
            {
                retryCount--;
                try
                {
                    var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["config"] == benchmarkConfig && d["procId"] == procId && d["machine"] == Environment.MachineName,
                Builders<BsonDocument>.Update
                    .Set("end_date", DateTime.Now)
                    .Set("resultType", "Error")
                    .Set("errorLog", logString),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });
                }
                catch (MongoCommandException exc)
                {
                    if (exc.Code != 16500)
                        throw;
                    Console.WriteLine($"Throttling limit reached. Retrying ({retryCount})...");
                    await Task.Delay(new Random().Next(10000));

                }
            }
            throw new Exception("Retry limit exceeded");

        }

        //IAsyncEnumerable..
        //Sort?
        public static async Task<IEnumerable<BenchmarkResults>> GetResults()
        {
            throw new NotImplementedException();
            //var cursor = await experimentCollection.Find(d => van result).ToCursorAsync();
            //return cursor.ToEnumerable().Select(d => BsonSerializer.Deserialize<@@@>(d));
        }

        public static async Task<int> CountQueued()
        {
            return (int)await experimentCollection.CountDocumentsAsync(queuedFilter);
        }
        public static async Task<int> CountLocked()
        {
            return (int)await experimentCollection.CountDocumentsAsync(lockedFilter);
        }

        public static async Task<int> CountFaulted()
        {
            return (int)await experimentCollection.CountDocumentsAsync(faultedFilter);
        }

        public static async Task<int> CountFinished()
        {
            return (int)await experimentCollection.CountDocumentsAsync(finishedFilter);
        }

        /// <returns>Number of deleted configurations.</returns>
        public static async Task ClearExperiment()
        {
            await db.DropCollectionAsync(Program.Experiment);
        }

        /// <summary>
        /// Remove locks from unfinished configurations.
        /// </summary>
        /// <returns>Number of locks removed.</returns>
        public static async Task<int> RemoveLocks()
        {
            var result = await experimentCollection.UpdateManyAsync(lockedFilter,
               Builders<BsonDocument>.Update
                .Unset("lockDate"),
               new UpdateOptions() { IsUpsert = false });
            return (int)result.MatchedCount;
        }

        /// <summary>
        /// Remove locks and logs from configurations that ran into exceptions.
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ResetFaulted()
        {
            var result = await experimentCollection.UpdateManyAsync(faultedFilter,
               Builders<BsonDocument>.Update
                .Unset("errorLog")
                .Unset("lockDate"),
               new UpdateOptions() { IsUpsert = false });
            return (int)result.MatchedCount;
        }

        public static async Task SetGrammarRules(string rulesString)
        {
            var rulesCollection = db.GetCollection<BsonDocument>("rules");
            var result = await rulesCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["experiment"] == Program.Experiment,
                Builders<BsonDocument>.Update
                    .Set("rulesString", rulesString),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = true });

        }

        public static async Task<string> GetGrammarRules()
        {
            var rulesCollection = db.GetCollection<BsonDocument>("rules");
            var doc = await rulesCollection
                .Find(d => d["experiment"] == Program.Experiment)
                .FirstAsync();
            return doc["rulesString"].AsString;
        }

    }
}
