using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Benchmark.Model;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark
{
    class Worker
    {
        static TimeSpan timeOut = new TimeSpan(0, 30, 0);
        static CloudBlobContainer Container;
        static CloudQueue Queue;
        static DirectoryInfo InputDirectory;
        static DirectoryInfo OutputDirectory;

        static Queue<VerifierBenchmark> LocalBenchmarks = new Queue<VerifierBenchmark>();

        static VerifierBenchmark CurrentBenchmark;
        static CloudQueueMessage CurrentMessage;
        static BenchmarkResults CurrentResults;
        static string CurrentResultType;
        static int i = 0;

        internal static async Task RunAsync(string inputDir, string outputDir)
        {
            OutputDirectory = Directory.CreateDirectory(outputDir);

            var initSuccess = await Init(inputDir);
            if (!initSuccess) return;

            Console.WriteLine("Worker is running. Press 'A' to abort.");

            while (true)
            {
                i++;
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine(DateTime.Now.ToString());

                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.A)
                {
                    Console.WriteLine("Aborting...");
                    return;
                }

                CurrentBenchmark = await GetNextBenchmark();
                if (CurrentBenchmark is null) return;

                var logger = new SimpleConsoleLogger();
                logger.Logged += (m, e, l) =>
                {
                    debugInfo.AppendLine(m);
                    if (e != null)
                        debugInfo.AppendLine(e.ToString());
                    if (l == LogLevel.Error || l == LogLevel.Critical)
                        CurrentResultType = "Error";
                };
                CurrentBenchmark.Logger = logger;

                Console.WriteLine($"{DateTime.Now}: Starting benchmark...");

                try
                {
                    CurrentResults = CurrentBenchmark.Execute(true);
                    CurrentResultType = "Success";
                }
                catch (Exception exc)
                {
                    CurrentResultType = "Error";
                    Console.WriteLine(exc.ToString());
                    debugInfo.AppendLine(exc.ToString());
                    //TODO: work with benchmark filename instead of 'i'
                    var debugFileName = $"Result_{i}_Log.txt";

                    debugFileName = Path.Combine(OutputDirectory.ToString(), debugFileName);
                    File.WriteAllText(debugFileName, debugInfo.ToString());

                    if (!Program.Offline)
                    {
                        var blob = Container.GetBlockBlobReference($"Results/{debugFileName}");
                        await blob.DeleteIfExistsAsync();
                        await blob.UploadFromFileAsync(debugFileName);
                    }
                }

                await ProcessResults();

                //LogProcessor.Dump(logger);
                // MongoDB 
                // TableStorage
                // Json => utólag, json-ben szűrni lehet
                // DynamoDB ==> MongoDB $$$$$$$
                // DateTime, MachineName, ....ExecutionTime,..., ResultType, Result json{40*60 táblázat}
                //benchmark.Dump(filename, config.ToKeyValuePairs());
            }
        }

        internal static async Task<bool> Init(string inputDir)
        {
            if (Program.Offline)
            {
                InputDirectory = new DirectoryInfo(inputDir);
                if (!InputDirectory.Exists)
                {
                    Console.WriteLine("Input directory doesn't exist. Aborting...");
                    return false;
                }

                foreach (var file in InputDirectory.GetFiles())
                {
                    var benchmark = SerializationHelper.DeserializeFromFile<VerifierBenchmark>(file.FullName);
                    LocalBenchmarks.Enqueue(benchmark);
                }
            }
            else
            {
                Console.WriteLine("Initializing container: " + Program.Experiment);
                var blobClient = Program.Account.CreateCloudBlobClient();
                Container = blobClient.GetContainerReference(Program.Experiment);
                if (!(await Container.ExistsAsync()))
                {
                    Console.WriteLine("Container does not exist. Aborting...");
                    return false;
                }

                Console.WriteLine("Initializing queue: " + Program.Experiment);
                var queueClient = Program.Account.CreateCloudQueueClient();
                Queue = queueClient.GetQueueReference(Program.Experiment);
                if (!(await Queue.ExistsAsync()))
                {
                    Console.WriteLine("Queue does not exist. Aborting...");
                    return false;
                }
            }
            return true;
        }

        internal static async Task<VerifierBenchmark> GetNextBenchmark()
        {
            if (Program.Offline)
            {
                if (LocalBenchmarks.Count == 0)
                {
                    Console.WriteLine("No more tasks in queue.");
                    return null;
                }
                Console.WriteLine($"{DateTime.Now}: Loading benchmark...");
                return LocalBenchmarks.Dequeue();
            }
            else
            {
                CurrentMessage = await Queue.GetMessageAsync(timeOut, null, null);
                if (CurrentMessage == null)
                {
                    Console.WriteLine("No more tasks in queue.");
                    return null;
                }
                Console.WriteLine($"{DateTime.Now}: Loading benchmark...");
                return SerializationHelper.Deserialize<VerifierBenchmark>(CurrentMessage.AsString);
            }
        }

        internal static async Task ProcessResults()
        {
            //Valami más név kéne, pl. VerifierBenchmark.ToString()
            var filename = $"Result_{i}.json";
            var fullfilename = Path.Combine(OutputDirectory.ToString(), filename);

            if (Program.Offline)
            {
                Console.WriteLine($"{DateTime.Now}: Writing results to disk...");
                SerializationHelper.JsonSerializeToFile<BenchmarkResults>(CurrentResults, fullfilename);
            }
            else
            {
                Console.WriteLine($"{DateTime.Now}: Writing results to disk...");
                SerializationHelper.JsonSerializeToFile<BenchmarkResults>(CurrentResults, fullfilename);

                Console.WriteLine($"{DateTime.Now}: Uploading results to Azure Blob store...");
                var blob = Container.GetBlockBlobReference($"Results/{filename}");
                await blob.DeleteIfExistsAsync();
                await blob.UploadFromFileAsync(fullfilename);

                Console.WriteLine($"{DateTime.Now}: Writing results to Azure Table store...");
                CloudTableClient tableClient = Program.Account.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(Program.Experiment + "FinalResults");
                await table.CreateIfNotExistsAsync();

                var finalResultEntity = new DynamicTableEntity();
                finalResultEntity.PartitionKey = filename;

                finalResultEntity.RowKey = CurrentResultType;
                finalResultEntity.Properties.Add("Machine", EntityProperty.CreateEntityPropertyFromObject(System.Environment.MachineName));
                finalResultEntity.Properties.Add("Frr", EntityProperty.CreateEntityPropertyFromObject(CurrentResults.FinalResult.Frr));
                finalResultEntity.Properties.Add("Far", EntityProperty.CreateEntityPropertyFromObject(CurrentResults.FinalResult.Far));
                finalResultEntity.Properties.Add("Aer", EntityProperty.CreateEntityPropertyFromObject(CurrentResults.FinalResult.Aer));
                
                var tableOperation = TableOperation.InsertOrReplace(finalResultEntity);
                await table.ExecuteAsync(tableOperation);

                table = tableClient.GetTableReference(Program.Experiment + "SignerResults");
                await table.CreateIfNotExistsAsync();

                var signerResultEntity = new DynamicTableEntity();
                signerResultEntity.PartitionKey = filename;

                foreach (var signerResult in CurrentResults.SignerResults)
                {
                    signerResultEntity.RowKey = signerResult.Signer;
                    signerResultEntity.Properties["Frr"] = EntityProperty.CreateEntityPropertyFromObject(signerResult.Frr);
                    signerResultEntity.Properties["Far"] = EntityProperty.CreateEntityPropertyFromObject(signerResult.Far);
                    signerResultEntity.Properties["Aer"] = EntityProperty.CreateEntityPropertyFromObject(signerResult.Aer);

                    tableOperation = TableOperation.InsertOrReplace(signerResultEntity);
                    await table.ExecuteAsync(tableOperation);
                }

                Console.WriteLine($"{DateTime.Now}: Writing results to MongoDB...");
                var document = BsonSerializer.Deserialize<BsonDocument>(SerializationHelper.JsonSerialize<BenchmarkResults>(CurrentResults));
                document.Add("Benchmark", filename);
                document.Add("Machine", System.Environment.MachineName);
                document.Add("Time", DateTime.Now);
                document.Add("ResultType", CurrentResultType);
                var client = new MongoClient("mongodb+srv://sigstat:sigstat@benchmarktest-4josb.azure.mongodb.net/test?retryWrites=true");
                var database = client.GetDatabase("benchmarks");
                var collection = database.GetCollection<BsonDocument>("results");
                await collection.InsertOneAsync(document);

                await Queue.DeleteMessageAsync(CurrentMessage);
            }
        }
    }
}
