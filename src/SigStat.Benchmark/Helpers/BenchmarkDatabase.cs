using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using SigStat.Benchmark.Execution;
using SigStat.Benchmark.Model;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SigStat.Benchmark.Analyser;

namespace SigStat.Benchmark.Helpers
{
    static class States
    {
        public const string Queued = "queued";
        public const string Locked = "locked";
        public const string Faulted = "faulted";
        public const string Finished = "finished";
    }
    /// <summary>
    /// dal for sigstat benchmarks database
    /// </summary>
    public static class BenchmarkDatabase
    {
        private static MongoClient client;
        private static IMongoDatabase db;
        private static IMongoCollection<BsonDocument> experimentCollection;
        private static object syncObj = new object();


        private static readonly Expression<Func<BsonDocument, bool>> queuedFilter = d =>
                d["state"] == States.Queued;

        private static readonly Expression<Func<BsonDocument, bool>> lockedFilter = d =>
                d["state"] == States.Locked;

        private static readonly Expression<Func<BsonDocument, bool>> faultedFilter = d =>
                d["state"] == States.Faulted;

        private static readonly Expression<Func<BsonDocument, bool>> finishedFilter = d =>
                d["state"] == States.Finished;


        private static readonly FilterDefinition<BsonDocument> expiredLockFilter =
             Builders<BsonDocument>.Filter.And(
                    lockedFilter,
                    Builders<BsonDocument>.Filter.Lt(d => d["lockDate"], new BsonDateTime(DateTime.Now.AddHours(-3))));
        /// <summary>
        /// This filter ensures to only lock items that are in the queue or have been locked for 1+ hours.
        /// </summary>
        /// <remarks>
        /// Since BsonDateTime comparison does not work in Expression filters, this must be defined with Filter Builders.
        /// </remarks>
        private static readonly FilterDefinition<BsonDocument> lockableFilter =
            Builders<BsonDocument>.Filter.Or(expiredLockFilter, queuedFilter);

        public static async Task InitializeConnection(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (!connectionString.StartsWith("mongodb://"))
            {
                try
                {
                    Console.WriteLine("Loading connection string from file");
                    connectionString = await File.ReadAllTextAsync(connectionString);
                }
                catch (IOException exc)
                {
                    throw new IOException($"Couldn't load connection string from '{connectionString}'", exc);
                }
            }


            Console.WriteLine("Connecting to server...");
            client = new MongoClient(connectionString);
            Console.WriteLine("Connecting to database...");
            db = client.GetDatabase("Benchmark");
            Console.WriteLine($"Connecting to {Program.Experiment} collection...");
            experimentCollection = db.GetCollection<BsonDocument>(Program.Experiment);
            Console.WriteLine("Connection ready");

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
        public static async Task<int> UpsertConfigs(IEnumerable<string> configs, int batchSize = 10, int skipCount = 0)
        {
            int totalCount = configs.Count();
            int processedCount = skipCount;
            int lastProcessedCount = skipCount;
            int throttlingDelay = 0;
            Stopwatch sw = Stopwatch.StartNew();

            var indexCursor = await experimentCollection.Indexes.ListAsync();
            var indexes = await indexCursor.ToListAsync();
            if (!indexes.Any(i => i["name"] == "config_1"))
                await experimentCollection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>("{ config: 1 }", new CreateIndexOptions() { Name = "config_1" }));
            if (!indexes.Any(i => i["name"] == "state_1"))
                await experimentCollection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>("{ state: 1 }", new CreateIndexOptions() { Name = "state_1" }));
            if (!indexes.Any(i => i["name"] == "state_1_duration_1"))
                await experimentCollection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>("{ state: 1, \"results.KeyValueGroups.Execution.dict.Duration\": 1 }", new CreateIndexOptions() { Name = "state_1_duration_1" }));

            foreach (var configBatch in configs.Skip(skipCount).ToArrays(batchSize))
            {
                //create write models
                var bulkOps = configBatch.Select(config => new UpdateOneModel<BsonDocument>(
                    Builders<BsonDocument>.Filter.Where(d => d["config"] == config),
                    Builders<BsonDocument>.Update
                        .SetOnInsert(d => d["config"], config)
                        .Set(d => d["state"], States.Queued))
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

            var result = await experimentCollection.FindOneAndUpdateAsync(lockableFilter,
                Builders<BsonDocument>.Update
                    .Set("lockDate", DateTime.Now)
                    .Set("state", States.Locked)
                    .Set("machine", Environment.MachineName)
                    .Set("procId", procId),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });

            if (result == null)
                return null;
            else
                return (string)result["config"];

        }

        /// <summary>
        /// Add results to a specified benchmark item.
        /// </summary>
        public static async Task SendResults(int procId, string benchmarkConfig, BenchmarkLogModel results)
        {

            var bsonResults = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize(results));

            var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["config"] == benchmarkConfig && d["procId"] == procId && d["machine"] == Environment.MachineName,
                Builders<BsonDocument>.Update
                    .Set("end_date", DateTime.Now)
                    .Set("state", States.Finished)
                    .Set("results", bsonResults)
                    .Set("dtw", "wiki"),

                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });
            return;


        }

        internal async static Task SendResults<T>(string config, string key, T value)
        {
            await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d => d["config"] == config && d["machine"] == Environment.MachineName,
               Builders<BsonDocument>.Update
                   .Set<T>(key, value)
                    .Set("end_date", DateTime.Now)
                    .Set("state", States.Finished)
                   ,
               new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });
        }

        /// <summary>
        /// Send log after exception.
        /// </summary>
        public static async Task SendErrorLog(string benchmarkConfig, string logString)
        {
            var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["config"] == benchmarkConfig && d["machine"] == Environment.MachineName,
                Builders<BsonDocument>.Update
                    .Set("end_date", DateTime.Now)
                    .Set("errorLog", logString)
                    .Set("state", States.Faulted),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });
            return;
        }

        public struct ExecutionStatistics
        {
            public long TotalMilliseconds;
            public long MaxMilliseconds;
            public long Count;
            public long Size;
            public long StorageSize;
            public long ExpiredLockCount;
        }
        public static async Task<ExecutionStatistics> GetExecutionStatisticsAsync()
        {
            var result = new ExecutionStatistics();
            //db.PreprocessingBenchmark.aggregate([
            //   { $match: { state: "finished" } },
            //   { $group: 
            //       { 
            //       _id: "1", 
            //       total: { $sum: { $toLong: "$results.KeyValueGroups.Execution.dict.Duration" }},
            //       max: { $max: { $toLong: "$results.KeyValueGroups.Execution.dict.Duration" }},
            //       count: { $sum: 1}
            //       } 
            //    }
            //])
            result.ExpiredLockCount = await experimentCollection.CountDocumentsAsync(expiredLockFilter);

            var stats = await db.RunCommandAsync<BsonDocument>("{collstats: '" + Program.Experiment + "'}");
            result.Size = stats["size"].ToInt64();
            result.StorageSize = stats["storageSize"].ToInt64();



            var match = new BsonDocument { { "$match", new BsonDocument { { "state", "finished" } } } };
            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", "1" },
                                { "count", new BsonDocument{{"$sum", 1}}},
                                { "total", new BsonDocument{{"$sum", new BsonDocument{{"$toLong", "$results.KeyValueGroups.Execution.dict.Duration" } }} } },
                                { "max", new BsonDocument{{"$max", new BsonDocument{{"$toLong", "$results.KeyValueGroups.Execution.dict.Duration" } }} } }

                            }
                  }
                };

            var pipeline = new[] { match, group };
            var cursor = await experimentCollection.AggregateAsync<BsonDocument>(pipeline);
            var queryResult = await cursor.FirstOrDefaultAsync();
            if (queryResult != null)
            {
                result.Count = queryResult["count"].ToInt64();
                result.MaxMilliseconds = queryResult["max"].ToInt64();
                result.TotalMilliseconds = queryResult["total"].ToInt64();
            }
            return result;

        }

        static Dictionary<string, PropertyInfo> reportLineProperties = typeof(ReportLine)
            .GetProperties().ToDictionary(p => p.Name, p => p);
        public static IEnumerable<ReportLine> GetResults()
        {
            var cursor = experimentCollection.FindSync(finishedFilter,
                new FindOptions<BsonDocument, BsonDocument>() { Projection = "{\"results.KeyValueGroups\": 1, config: 1, classification: 1}" });

            //var bsonResults = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize(results));

            foreach (var bson in cursor.ToEnumerable())
            {
                var execution = bson["results"]["KeyValueGroups"]["Execution"]["dict"];
                var parameters = bson["results"]["KeyValueGroups"]["Parameters"]["dict"].AsBsonDocument;
                var classification = bson["classification"].AsBsonArray;
                var benchmarkResults = bson["results"]["KeyValueGroups"]["BenchmarkResults"]["dict"];
                var reportLine = new ReportLine()
                {
                    Key = bson["config"].AsString,
                    Date = execution["Date"].AsString,
                    Agent = execution["Agent"].AsString,
                    Duration = execution["Duration"].AsString,



                    FRR = benchmarkResults["FRR"].AsDouble,
                    FAR = benchmarkResults["FAR"].AsDouble,
                    AER = benchmarkResults["AER"].AsDouble,

                    ClassificationResults = classification.ToList().Select(b=>BsonSerializer.Deserialize<ClassificationResult>(b.AsBsonDocument)).ToList()

                };
                foreach (var element in parameters.Elements)
                {
                    reportLineProperties[element.Name].SetValue(reportLine, element.Value.AsString);
                }


                yield return reportLine;
            }
        }

        public static async Task<BenchmarkReport> LockNextBenchmarkReport()
        {
            var bson = await experimentCollection.FindOneAndUpdateAsync(lockableFilter,
                   Builders<BsonDocument>.Update
                       .Set("lockDate", DateTime.Now)
                       .Set("state", States.Locked)
                       .Set("machine", Environment.MachineName),
                   new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });

            if (bson == null)
                return null;


            if (!bson.Contains("results"))
            {
                Console.WriteLine("An enqueued config did not contain any results. Config: " + bson["config"].AsString);
                return null;
            }


            //TODO: this manual bson parsing is highly inefficient
            BenchmarkReport report = new BenchmarkReport();
            report.Config = bson["config"].AsString;
            var resultsBlock = bson["results"]["KeyValueGroups"]["Parameters"]["dict"];
            report.Split = resultsBlock["Split"].AsString;
            report.Database = resultsBlock["Database"].AsString;
            report.Distance = resultsBlock["Distance"].AsString;

            report.SignerResults = new List<SignerResults>();
            foreach (var signer in bson["results"]["SignerResults"].AsBsonDocument)
            {
                var signerDoc = signer.Value.AsBsonDocument;
                SignerResults signerResult = new SignerResults(signerDoc["SignerID"].AsString)
                {
                    Far = signerDoc["Far"].AsDouble,
                    Frr = signerDoc["Frr"].AsDouble,
                    Aer = signerDoc["Aer"].AsDouble,
                };
                var rows = signerDoc["DistanceMatrix"].AsBsonArray;
                var colHeaders = rows[0].AsBsonArray.Select(v => v.ToString()).ToArray();
                var rowHeaders = rows.Select(r => r.AsBsonArray[0].ToString()).ToArray();

                for (int rowIndex = 1; rowIndex < rows.Count; rowIndex++)
                {
                    var row = rows[rowIndex].AsBsonArray;
                    for (int colIndex = 1; colIndex < row.Count; colIndex++)
                    {
                        signerResult.DistanceMatrix[rowHeaders[rowIndex], colHeaders[colIndex]] = ParseDouble(row[colIndex]);
                    }
                }
                report.SignerResults.Add(signerResult);
            }
            return report;
        }

        private static double ParseDouble(BsonValue bsonValue)
        {
            if (bsonValue.IsDouble)
                return bsonValue.AsDouble;
            if (bsonValue.IsString)
            {
                switch (bsonValue.AsString)
                {
                    case "NaN": return double.NaN;
                    case "Infinity": return double.PositiveInfinity;
                }
            }
            throw new NotSupportedException("Unsupported double value: " + bsonValue?.ToString());
        }

        public static string ToJson(BsonDocument bson)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BsonBinaryWriter(stream))
                {
                    BsonSerializer.Serialize(writer, typeof(BsonDocument), bson);
                }
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new Newtonsoft.Json.Bson.BsonReader(stream))
                {
                    var sb = new StringBuilder();
                    var sw = new StringWriter(sb);
                    using (var jWriter = new JsonTextWriter(sw))
                    {
                        jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                        jWriter.WriteToken(reader);
                    }
                    return sb.ToString();
                }
            }
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
                .Set("state", States.Queued)
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
                .Set("state", States.Queued)
                .Unset("errorLog")
                .Unset("end_date"),

               new UpdateOptions() { IsUpsert = false });
            return (int)result.MatchedCount;
        }
        public static async Task<int> RequeueFinished()
        {
            var result = await experimentCollection.UpdateManyAsync(finishedFilter,
               Builders<BsonDocument>.Update
                .Set("state", States.Queued)
                .Unset("end_date"),
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
                .FirstOrDefaultAsync();
            if (doc == null)
                return "";
            return doc["rulesString"].AsString;
        }

    }
}
