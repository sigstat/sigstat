using MongoDB.Bson;
using MongoDB.Driver;
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
            //add/edit item in experiments collection, date etc.
            //Clear configs,results,summaries collection items of this experiment
        }

        public static async Task InsertConfigs(string experimentName, List<string> configs)
        {
            IEnumerable<BsonDocument> documents = configs.Select(c => new BsonDocument {
                { "experiment", experimentName },
                { "data", c }
            });

            db.GetCollection<BsonDocument>("configs").InsertMany(documents);//InsertManyAsync
        }

    }
}
