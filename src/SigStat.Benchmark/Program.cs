using CommandLine;
using MongoDB.Driver;
using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SigStat.Benchmark
{
    class Program
    {
        public static MongoClient client;
        public static IMongoDatabase db;
        public static string Experiment;

        public static async Task<bool> MongoInit(OptionsBase o)
        {
            return await DatabaseHelper.InitializeConnection(o.ConnectionString);
            //var collection = db.GetCollection<BsonDocument>("experiments");
            
            //catch (Exception e)
            //{
            //    //mongodb connection failed
            //    return false;
            //}
        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<MonitorOptions, WorkerOptions, GeneratorOptions, AnalyserOptions>(args)
                .MapResult<OptionsBase, Task>(async o =>
                {
                    if (await MongoInit(o))
                        await o.RunAsync();
                    else
                        return;
                },
                errs => Task.FromResult(-1));
            Console.WriteLine($"{DateTime.Now}: Execution finished.");
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();
            }

            

        }
    }

}

