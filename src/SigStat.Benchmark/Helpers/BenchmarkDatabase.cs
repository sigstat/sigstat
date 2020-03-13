using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
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
                d["exception"] == BsonNull.Value &&
                d["lockDate"] == BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> lockedFilter = d =>
                d["results"] == BsonNull.Value &&
                d["exception"] == BsonNull.Value &&
                d["lockDate"] != BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> faultedFilter = d =>
                d["exception"] != BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> finishedFilter = d =>
                d["results"] != BsonNull.Value;

        private static readonly Expression<Func<BsonDocument, bool>> lockableFilter = d =>
                d["result"] == BsonNull.Value && 
                ((d["exception"] == BsonNull.Value && d["lockDate"] == BsonNull.Value)
                 || d["lockDate"].AsBsonDateTime < new BsonDateTime(DateTime.Now.AddHours(-1)));

        public static async Task<bool> InitializeConnection(string connectionString)
        {
            client = new MongoClient(connectionString);
            db = client.GetDatabase("benchmarks");
            experimentCollection = db.GetCollection<BsonDocument>(Program.Experiment);

            //TODO: Test connection, with throttling similar to Worker..
            //return false;

            return true;
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
        /// This does not remove locks, results and logs on existing items.
        /// </summary>
        /// <param name="configs"></param>
        /// <returns>Number of new configurations inserted.</returns>
        public static async Task<int> UpsertConfigs(IEnumerable<string> configs)
        {
            var bulkOps = new List<WriteModel<BsonDocument>>();
            foreach (var c in configs)
            {
                var upsertOne = new UpdateOneModel<BsonDocument>(
                    Builders<BsonDocument>.Filter.Where(d => d["config"] == c),
                    new BsonDocument
                    {
                        {"$set", 
                            new BsonDocument{ 
                                {"config", c}
                                //{"old", d} //Idea: Use ReplaceOneModel & store old version?
                            }
                        }
                    })
                { IsUpsert = true };
                bulkOps.Add(upsertOne);
            }
            var result = await experimentCollection.BulkWriteAsync(bulkOps, 
                new BulkWriteOptions { IsOrdered = false });//enables parallel exec
            return configs.Count() - (int)result.MatchedCount;
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
        public static async Task SendResults(int procId, string benchmarkConfig, string resultType, BenchmarkLogModel results)
        {
            var bsonResults = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize(results));

            var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["config"] == benchmarkConfig && d["procId"] == procId && d["machine"] == Environment.MachineName,
                Builders<BsonDocument>.Update
                    .Set("end_date", DateTime.Now)
                    .Set("resultType", resultType)
                    .Set("results", bsonResults),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });

        }

        /// <summary>
        /// Send log after exception.
        /// </summary>
        public static async Task SendException(int procId, string benchmarkConfig, string logString)
        {
            var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["config"] == benchmarkConfig && d["procId"] == procId && d["machine"] == Environment.MachineName,
                Builders<BsonDocument>.Update
                    .Set("end_date", DateTime.Now)
                    //.Set("resultType", "exception")
                    .Set("exception", logString),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false });

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
            return (int) await experimentCollection.CountDocumentsAsync(queuedFilter);
        }
        public static async Task<int> CountLocked()
        {
            return (int) await experimentCollection.CountDocumentsAsync(lockedFilter);
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
        public static async Task<int> ClearExperiment()
        {
            var result = await experimentCollection.DeleteManyAsync(d => true);
            return (int)result.DeletedCount;
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
                .Unset("exception")
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
