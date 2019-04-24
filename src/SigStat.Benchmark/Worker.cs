using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
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
        static bool LocalInput = false;

        internal static async Task RunAsync(string inputDir, string outputDir)
        {
            if (inputDir != null) LocalInput = true;

            if (LocalInput)
            {
                if (Directory.Exists(inputDir)) InputDirectory = new DirectoryInfo(inputDir);
                else
                {
                    Console.WriteLine("Input directory doesn't exist. Aborting...");
                    return;
                }

                foreach (var file in InputDirectory.GetFiles())
                {
                    var benchmark = SerializationHelper.DeserializeFromFile<VerifierBenchmark>(file.FullName);
                    LocalBenchmarks.Enqueue(benchmark);
                }
            }

            OutputDirectory = Directory.CreateDirectory(outputDir);

            if (!Program.Offline)
            {
                try
                {
                    Console.WriteLine("Initializing container: " + Program.Experiment);
                    var blobClient = Program.Account.CreateCloudBlobClient();
                    Container = blobClient.GetContainerReference(Program.Experiment);
                    if (!(await Container.ExistsAsync()))
                    {
                        Console.WriteLine("Container does not exist. Aborting...");
                        return;
                    }


                    Console.WriteLine("Initializing queue: " + Program.Experiment);
                    var queueClient = Program.Account.CreateCloudQueueClient();
                    Queue = queueClient.GetQueueReference(Program.Experiment);
                    if (!(await Queue.ExistsAsync()))
                    {
                        Console.WriteLine("Queue does not exist. Aborting...");
                        return;
                    }
                }
                catch { }
            }

            Console.WriteLine("Worker is running. Press 'a' to abort.");
            while (true)
            {
                bool error = false;
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine(DateTime.Now.ToString());
                string debugFileName = null;

                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.A)
                {
                    Console.WriteLine("Aborting...");
                    return;
                }

                var benchmark = new VerifierBenchmark();
                CloudQueueMessage msg = null;

                try
                {
                    if (LocalInput)
                    {
                        if(LocalBenchmarks.Count == 0)
                        {
                            Console.WriteLine("No more tasks in queue.");
                            return;
                        }
                        var localbenchmark = LocalBenchmarks.Peek();
                        Console.WriteLine($"{DateTime.Now}: Loading benchmark...");
                        //Console.WriteLine(localbenchmark);
                        //debugInfo.AppendLine(localbenchmark);
                        //benchmark = SerializationHelper.Deserialize<VerifierBenchmark>(localbenchmark);
                        benchmark = localbenchmark;
                    }
                    else
                    {
                        msg = await Queue.GetMessageAsync(timeOut, null, null);
                        if (msg == null)
                        {
                            Console.WriteLine("No more tasks in queue.");
                            return;
                        }
                        Console.WriteLine($"{DateTime.Now}: Loading benchmark...");
                        Console.WriteLine(msg.AsString);
                        debugInfo.AppendLine(msg.AsString);
                        benchmark = SerializationHelper.Deserialize<VerifierBenchmark>(msg.AsString);
                    }

                    var logger = new SimpleConsoleLogger();
                    logger.Logged += (m, e, l) =>
                    {
                        debugInfo.AppendLine(m);
                        if (e != null)
                            debugInfo.AppendLine(e.ToString());
                        if (l == LogLevel.Error || l == LogLevel.Critical)
                            error = true;
                    };
                    benchmark.Logger = logger;

                    Console.WriteLine($"{DateTime.Now}: Starting benchmark...");
                    var results = benchmark.Execute(true);

                    Console.WriteLine($"{DateTime.Now}: Generating results...");
                    //TODO: valami más név kéne, pl. VerifierBenchmark.ToShortString()
                    var filename = Guid.NewGuid() + ".json";
                    var fullfilename = Path.Combine(OutputDirectory.ToString(), filename);

                    //LogProcessor.Dump(logger);
                    // MongoDB 
                    // TableStorage
                    // Json => utólag, json-ben szűrni lehet
                    // DynamoDB ==> MongoDB $$$$$$$
                    // DateTime, MachineName, ....ExecutionTime,..., ResultType, Result json{40*60 táblázat}
                    //benchmark.Dump(filename, config.ToKeyValuePairs());

                    Console.WriteLine($"{DateTime.Now}: Writing results to disk...");
                    SerializationHelper.JsonSerializeToFile<BenchmarkResults>(results, fullfilename);

                    if(!Program.Offline)
                    {
                        Console.WriteLine($"{DateTime.Now}: Uploading results...");

                        var blob = Container.GetBlockBlobReference($"Results/{filename}");
                        await blob.DeleteIfExistsAsync();
                        await blob.UploadFromFileAsync(fullfilename);
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.ToString());
                    debugInfo.AppendLine(exc.ToString());
                    error = true;
                }
                if (error)
                {
                    debugFileName = $"{debugFileName ?? Guid.NewGuid().ToString()}.txt";
                    var cloudFilename = debugFileName;

                    debugFileName = Path.Combine(OutputDirectory.ToString(), debugFileName);
                    File.WriteAllText(debugFileName, debugInfo.ToString());
                    var blob = Container.GetBlockBlobReference(cloudFilename);
                    await blob.DeleteIfExistsAsync();
                    await blob.UploadFromFileAsync(debugFileName);
                }

                if (!LocalInput) await Queue.DeleteMessageAsync(msg);
                else LocalBenchmarks.Dequeue();
                }
            }
        }
    }
