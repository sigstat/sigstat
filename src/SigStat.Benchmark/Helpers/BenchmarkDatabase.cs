using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SigStat.Common;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static async Task<bool> InitializeConnection(string connectionString)
        {
            client = new MongoClient(connectionString);
            db = client.GetDatabase("benchmarks");

            //TODO: Test connection, with throttling similar to Worker..
            //return false;

            return true;
        }

        public static async Task InitializeExperiment()
        {
            //TODO: add arguments to clear results, logs etc. of previous experiment run

            var result = await db.GetCollection<BsonDocument>("configs")
                .DeleteManyAsync(d => d["experiment"] == Program.Experiment);
            Console.WriteLine($"Deleted {result.DeletedCount} old configurations.");

            //TODO: Keep track of experiments and their previous runs?
            //insert or update experiment (date etc.)
            //await db.GetCollection<BsonDocument>("experiments")
            //    .UpdateOneAsync(
            //        d => d["name"] == experimentName, 
            //        Builders<BsonDocument>.Update.Set("date", DateTime.Now.ToString()), 
            //        new UpdateOptions{ IsUpsert = true });

        }

        public static async Task InsertConfigs(IEnumerable<Dictionary<string,string>> configs)
        {
            IEnumerable<BsonDocument> documents = configs.Select(c => 
                new BsonDocument(c)
                .Add("experiment", Program.Experiment));

            await db.GetCollection<BsonDocument>("configs").InsertManyAsync(documents);
        }

        /// <summary>
        /// This is atomic. Returns null if no config can be locked
        /// </summary>
        /// <returns></returns>
        public static async Task<Dictionary<string,string>> LockNextConfig(int procId)
        {
            var configs = db.GetCollection<BsonDocument>("configs");
            var result = await configs.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["experiment"] == Program.Experiment && d["lockId"] == BsonNull.Value,
                Builders<BsonDocument>.Update
                    .Set("lockId", procId),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false }
                );
            if (result == null)
                return null;
            else
                return new Dictionary<string,string>(result.ToDictionary()
                    .Select(p => KeyValuePair.Create(p.Key,p.Value.ToString())));
        }

        /// <summary>
        /// Inserts the result and deletes the locked config
        /// </summary>
        /// <returns></returns>
        public static async Task SendResults(int procId, Dictionary<string,string> benchmarkConfig, string resultType, BenchmarkResults results)
        {
            var bsonResults = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize(results));
            var document = new BsonDocument
            {
                { "experiment", Program.Experiment },
                { "config", new BsonDocument(benchmarkConfig) },
                { "machine", Environment.MachineName },
                { "procId", procId },
                { "end_date", DateTime.Now.ToString() },
                { "resultType", resultType },
                { "results", bsonResults }
            };
            var collection = db.GetCollection<BsonDocument>("results");
            await collection.InsertOneAsync(document);

            //Delete config file
            var configs = db.GetCollection<BsonDocument>("configs");
            var result = await configs.FindOneAndDeleteAsync(d =>
                d["experiment"] == Program.Experiment && d["lockId"] == procId);

        }

        public static async Task SendLog(int procId, Dictionary<string,string> benchmarkConfig, string logString, bool markExceptionOccured)
        {
            var logs = db.GetCollection<BsonDocument>("logs");
            await logs.InsertOneAsync(new BsonDocument {
                { "experiment", Program.Experiment },
                { "procId", procId },
                { "machine", System.Environment.MachineName },
                { "config", new BsonDocument(benchmarkConfig) },
                { "log", logString },
                { "exception_occured", markExceptionOccured }
            });
        }

        //IAsyncEnumerable..
        //Sort?
        public static async Task<IEnumerable<BenchmarkResults>> GetResults()
        {
            var resultsCollection = db.GetCollection<BsonDocument>("results");
            var cursor = await resultsCollection.Find(d => d["experiment"] == Program.Experiment).ToCursorAsync();
            return cursor.ToEnumerable().Select(d => BsonSerializer.Deserialize<BenchmarkResults>(d));
        }

        public static async Task<int> CountFinished()
        {
            var resultsCollection = db.GetCollection<BsonDocument>("results");
            return (int) await resultsCollection.CountDocumentsAsync(d => d["experiment"] == Program.Experiment);
        }

        public static async Task<int> CountLocked()
        {
            var configsCollection = db.GetCollection<BsonDocument>("configs");
            return (int)await configsCollection.CountDocumentsAsync(d => d["experiment"] == Program.Experiment && d["lockId"] != BsonNull.Value);
        }

        public static async Task<int> CountQueued()
        {
            var configsCollection = db.GetCollection<BsonDocument>("configs");
            return (int)await configsCollection.CountDocumentsAsync(d => d["experiment"] == Program.Experiment && d["lockId"] == BsonNull.Value);
        }
        
        public static async Task<int> CountExceptions()
        {
            var logsCollection = db.GetCollection<BsonDocument>("logs");
            return (int)await logsCollection.CountDocumentsAsync(d => d["experiment"] == Program.Experiment && d["exception_occured"] ==true);
        }

        public static async Task<string> GetGrammarRules()
        {
            var rulesCollection = db.GetCollection<BsonDocument>("rules");
            var doc = await rulesCollection.Find(d => d["experiment"] == Program.Experiment).FirstAsync();
            var rules = doc["rules"].AsBsonArray.Select(v=>v.AsString);
            return String.Join(Environment.NewLine, rules);
        }

    }
}
