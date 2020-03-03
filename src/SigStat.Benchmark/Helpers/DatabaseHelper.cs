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
    public static class DatabaseHelper
    {
        private static  MongoClient client;
        private static  IMongoDatabase db;
        public static async Task<bool> InitializeConnection(string connectionString)
        {
            client = new MongoClient(connectionString);

            //

            db = client.GetDatabase("benchmarks");

            //catch..
            return false;
        }

        public static async Task InitializeExperiment(string experimentName)
        {
            //clear arg?
            var result = await db.GetCollection<BsonDocument>("configs")
                .DeleteManyAsync(d => d["experiment"] == experimentName);
            Console.WriteLine($"Deleted {result.DeletedCount} old configurations.");

            //insert or update experiment (date etc.)
            //await db.GetCollection<BsonDocument>("experiments")
            //    .UpdateOneAsync(
            //        d => d["name"] == experimentName, 
            //        Builders<BsonDocument>.Update.Set("date", DateTime.Now.ToString()), 
            //        new UpdateOptions{ IsUpsert = true });

            //Clear configs,results,summaries collection items of this experiment?
        }

        public static async Task InsertConfigs(string experimentName, IEnumerable<string> configs)
        {
            IEnumerable<BsonDocument> documents = configs.Select(c => new BsonDocument {
                { "experiment", experimentName },
                { "data", c },
                //{ "lockId", BsonNull.Value }
            });

            db.GetCollection<BsonDocument>("configs").InsertMany(documents);//InsertManyAsync
        }

        /// <summary>
        /// This is atomic. Returns null if no config can be locked
        /// </summary>
        /// <returns></returns>
        public static async Task<string> LockNextConfig(string experimentName, int procId)
        {
            var configs = db.GetCollection<BsonDocument>("configs");
            var result = await configs.FindOneAndUpdateAsync<BsonDocument>(d => 
                d["experiment"] == experimentName && d["lockId"] == BsonNull.Value,
                Builders<BsonDocument>.Update
                    .Set("lockId", procId),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false }
                );
            if (result == null)
                return null;
            else
                return (string)result["data"];
        }

        /// <summary>
        /// Inserts the result and deletes the locked config
        /// </summary>
        /// <returns></returns>
        public static async Task SendResults(string experimentName, int procId, string benchmarkId, string resultType, BenchmarkResults results)
        {

            var document = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize<BenchmarkResults>(results));
            document.Add("benchmark", benchmarkId);
            document.Add("machine", Environment.MachineName);
            document.Add("procId", procId);
            document.Add("end_date", DateTime.Now.ToString());
            document.Add("resultType", resultType);
            var collection = db.GetCollection<BsonDocument>("results");
            await collection.InsertOneAsync(document);

            //Delete config file
            var configs = db.GetCollection<BsonDocument>("configs");
            var result = await configs.FindOneAndDeleteAsync(d =>
                d["experiment"] == experimentName && d["lockId"] == procId);            

        }

        public static async Task SendLog(string experimentName, int procId, string benchmarkId, string logString)
        {
            var logs = db.GetCollection<BsonDocument>("logs");
            await logs.InsertOneAsync(new BsonDocument {
                { "experiment", experimentName },
                { "procId", procId },
                { "machine", System.Environment.MachineName },
                { "benchmarkId", benchmarkId },
                { "log", logString }
            });
        }

    }
}
