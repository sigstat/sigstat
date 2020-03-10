using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Logging;
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
        private static IMongoCollection<BsonDocument> experimentCollection;


        public static async Task<bool> InitializeConnection(string connectionString)
        {
            client = new MongoClient(connectionString);
            db = client.GetDatabase("benchmarks");
            experimentCollection = db.GetCollection<BsonDocument>(Program.Experiment);

            //TODO: Test connection, with throttling similar to Worker..
            //return false;

            return true;
        }

        public static async Task<bool> InitializeExperiment(string rulesString)
        {
            bool experimentExists = await (await db.ListCollectionsAsync(new ListCollectionsOptions
            {
                Filter = new BsonDocument("name", Program.Experiment)
            })).AnyAsync();

            //Clear previous experiment
            if (experimentExists)
            {
                if (!Console.IsInputRedirected)
                {
                    Console.WriteLine("This experiment already exists. Clear previous results? (y/n)");
                    if (Console.ReadKey(true).Key != ConsoleKey.Y)
                    {
                        return false;
                    }
                }
            }

            Console.WriteLine($"{DateTime.Now}: Clearing...");
            var result = await experimentCollection.DeleteManyAsync(d => true);
            Console.WriteLine($"{DateTime.Now}: Deleted {result.DeletedCount} documents.");

            Console.WriteLine($"{DateTime.Now}: Setting grammar rules...");
            await setGrammarRules(rulesString);

            //TODO: Keep track of previous runs of the experiment?
            //TODO: Store initialization DateTime?

            return true;
        }

        public static async Task InsertConfigs(IEnumerable<string> configs)
        {
            IEnumerable<BsonDocument> documents = configs.Select(c => 
                new BsonDocument()
                .Add("config", c));

            await experimentCollection.InsertManyAsync(documents);
        }

        /// <summary>
        /// This is atomic. Returns null if no config can be locked
        /// </summary>
        /// <returns></returns>
        public static async Task<string> LockNextConfig(int procId)
        {
            var result = await experimentCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["result"] == BsonNull.Value && ((d["exception"] == BsonNull.Value && d["lockDate"] == BsonNull.Value) || d["lockDate"].AsBsonDateTime < new BsonDateTime(DateTime.Now.AddHours(-1))),
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
        /// Inserts the results
        /// </summary>
        /// <returns></returns>
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
            return (int) await experimentCollection.CountDocumentsAsync(d =>
                d["results"] == BsonNull.Value &&
                d["exception"] == BsonNull.Value &&
                d["lockDate"] == BsonNull.Value);
        }
        public static async Task<int> CountLocked()
        {
            return (int) await experimentCollection.CountDocumentsAsync(d =>
                d["results"] == BsonNull.Value &&
                d["exception"] == BsonNull.Value &&
                d["lockDate"] != BsonNull.Value);
        }

        public static async Task<int> CountFinished()
        {
            return (int) await experimentCollection.CountDocumentsAsync(d => 
                d["results"] != BsonNull.Value);
        }

        public static async Task<int> CountExceptions()
        {
            return (int) await experimentCollection.CountDocumentsAsync(d => 
                d["exception"] != BsonNull.Value);
        }

        //TODO: Review rules collection
        private static async Task setGrammarRules(string rulesString)
        {
            var rulesCollection = db.GetCollection<BsonDocument>("rules");
            var result = await rulesCollection.FindOneAndUpdateAsync<BsonDocument>(d =>
                d["experiment"] == Program.Experiment,
                Builders<BsonDocument>.Update
                    .Set("rulesString", rulesString),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = true });

        }

        //TODO: Review rules collection
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
