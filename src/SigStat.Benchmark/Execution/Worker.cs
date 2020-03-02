using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Model;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark
{
    class Worker
    {
        static TimeSpan timeOut = new TimeSpan(0, 30, 0);

        static Queue<VerifierBenchmark> LocalBenchmarks = new Queue<VerifierBenchmark>();

        static VerifierBenchmark CurrentBenchmark;
        static string CurrentBenchmarkId;
        static BenchmarkResults CurrentResults;
        static string CurrentResultType;
        static int ProcessId;

        internal static async Task RunAsync(int procId, int maxThreads)
        {
            //stop worker process after 3 days
            DateTime stopTime = DateTime.Now.AddHours(71);

            ProcessId = procId;
            //delayed start
            await Task.Delay(100 * ProcessId);
            
            //var initSuccess = await Init(inputDir);
            //if (!initSuccess) return;

            Console.WriteLine($"{DateTime.Now}: Worker is running.");
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("Press 'A' to abort.");
            }

            while (DateTime.Now < stopTime)
            {
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine(DateTime.Now.ToString());

                if (!Console.IsInputRedirected)
                {
                    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.A)
                    {
                        Console.WriteLine($"{DateTime.Now}: Aborting...");
                        return;
                    }
                }


                var logger = new SimpleConsoleLogger();//default log level: Information
                logger.Logged += (m, e, l) =>
                {
                    debugInfo.AppendLine(m);
                    if (e != null)
                        debugInfo.AppendLine(e.ToString());
                    if (l == LogLevel.Error || l == LogLevel.Critical)
                        CurrentResultType = "Error";
                };

                CurrentBenchmark = await GetNextBenchmark();
                if (CurrentBenchmark is null)
                    return;
                CurrentBenchmark.Logger = logger;

                Console.WriteLine($"{DateTime.Now}: Starting benchmark...");

                try
                {
                    if (maxThreads>0)
                        CurrentResults = CurrentBenchmark.Execute(maxThreads);
                    else
                        CurrentResults = CurrentBenchmark.Execute(true);//default: no restriction 
                    CurrentResultType = "Success";
                }
                catch (Exception exc)
                {
                    CurrentResultType = "Error";
                    Console.WriteLine(exc.ToString());
                    debugInfo.AppendLine(exc.ToString());
                    var debugFileName = $"Result_{CurrentBenchmarkId}_Log.txt";

                    debugFileName = Path.Combine(OutputDirectory.ToString(), debugFileName);
                    File.WriteAllText(debugFileName, debugInfo.ToString());

                    if (!Program.Offline)
                    {
                        var blob = Container.GetBlockBlobReference($"Results/{debugFileName}");
                        await blob.DeleteIfExistsAsync();
                        await blob.UploadFromFileAsync(debugFileName);
                    }
                    continue;
                }

                await ProcessResults();

                if(Program.Offline)
                {//delete input config and lock after processing
                    File.Delete(Path.Combine(InputDirectory.ToString(), CurrentBenchmarkId + ".json"));
                    File.Delete(Path.Combine(InputDirectory.ToString(), CurrentBenchmarkId + ".json.lock"));
                }
                else
                    await Queue.DeleteMessageAsync(CurrentMessage);

                //LogProcessor.Dump(logger);
                // MongoDB 
                // TableStorage
                // Json => utólag, json-ben szűrni lehet
                // DynamoDB ==> MongoDB $$$$$$$
                // DateTime, MachineName, ....ExecutionTime,..., ResultType, Result json{40*60 táblázat}
                //benchmark.Dump(filename, config.ToKeyValuePairs());
            }
        }

        //internal static async Task<bool> Init(string inputDir)
        //{
        //        Console.WriteLine("Initializing container: " + Program.Experiment);
        //        
        //        //get configs collection
        //
        //        Console.WriteLine("Initializing queue: " + Program.Experiment);
        //        var queueClient = Program.Account.CreateCloudQueueClient();
        //        Queue = queueClient.GetQueueReference(Program.Experiment);
        //        if (!(await Queue.ExistsAsync()))
        //        {
        //            Console.WriteLine("Queue does not exist. Aborting...");
        //            return false;
        //        }
        //
        //    return true;
        //}

        internal static async Task<VerifierBenchmark> GetNextBenchmark()
        {
            Console.WriteLine($"{DateTime.Now}: Looking for unprocessed configurations...");
            string next = null;

            int tries = 3;
            while (tries > 0)
            {//Try get next configuration 3 times
                next = await DatabaseHelper.TryGetNextConfig(Program.Experiment, ProcessId);
                if (next == null)
                    tries--;
                else break;
            }

            if (next == null)
            {
                Console.WriteLine($"{DateTime.Now}: No more tasks in queue.");
                return null;
            }

            Console.WriteLine($"{DateTime.Now}: Loading benchmark {CurrentBenchmarkId}...");
            return SerializationHelper.Deserialize<VerifierBenchmark>(next);
        }

        internal static async Task ProcessResults()
        {
            //CurrentBenchmarkFileId helyett: VerifierBenchmark.ToString()
            var filename = $"Result_{CurrentBenchmarkId}.xlsx";
            var fullfilename = Path.Combine(OutputDirectory.ToString(), filename);

            if (Program.Offline)
            {
                Console.WriteLine($"{DateTime.Now}: Writing results to disk...");
                CurrentBenchmark.Dump(fullfilename, CurrentBenchmark.Parameters);

            }
            else
            {
                //TODO: szinten excelezni json results helyett

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
            }
        }
    }
}
