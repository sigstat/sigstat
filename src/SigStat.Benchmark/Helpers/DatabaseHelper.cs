using MongoDB.Bson;
using MongoDB.Driver;
using SigStat.Common;
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

            db = client.GetDatabase("sigstat");

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
            await db.GetCollection<BsonDocument>("experiments")
                .UpdateOneAsync(
                    d => d["name"] == experimentName, 
                    Builders<BsonDocument>.Update.Set("date", DateTime.Now.ToString()), 
                    new UpdateOptions{ IsUpsert = true });

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
        /// This is atomic, locks the config. Returns null if no config can be locked
        /// </summary>
        /// <returns></returns>
        public static async Task<string> TryGetNextConfig(string experimentName, int procId)
        {
            var configs = db.GetCollection<BsonDocument>("configs");
            var result = await configs.FindOneAndUpdateAsync<BsonDocument>(d => 
                d["experiment"] == experimentName && d["lock"] == BsonNull.Value,
                Builders<BsonDocument>.Update
                    .Set("lockId", procId)
                    .Set("date", DateTime.Now.ToString()),
                new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false }
                );
            if (result == null)
                return null;
            else
                return (string)result["data"];
            /*using (var session = await client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    //find one
                    await configs.find
                    //lock
                    //get
                }
                catch
                {
                    await session.AbortTransactionAsync();
                }
            }*/
        }

    }
}
