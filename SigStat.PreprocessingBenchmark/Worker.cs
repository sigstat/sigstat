using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.PreprocessingBenchmark
{
    class Worker
    {
        static TimeSpan timeOut = new TimeSpan(0, 30, 0);
        static CloudBlobContainer Container;
        static CloudQueue Queue;

        internal static async Task RunAsync()
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

            DateTime lastRefresh = DateTime.Now.AddDays(-1);

            Console.WriteLine("Worker is running. Press 'a' to abort.");
            while (true)
            {
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine(DateTime.Now.ToString());
                string debugFileName = null;

                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.A)
                {
                    Console.WriteLine("Aborting...");
                    return;
                }

                var msg = await Queue.GetMessageAsync(timeOut, null, null);
                if (msg == null)
                {
                    Console.WriteLine("No more tasks in queue.");
                    return;
                }
                try
                {
                    Console.WriteLine($"{DateTime.Now}: Loading benchmark...");
                    Console.WriteLine(msg.AsString);
                    debugInfo.AppendLine(msg.AsString);
                    var config = BenchmarkConfig.FromJsonString(msg.AsString);
                    debugFileName = config.ToShortString();
                    Console.WriteLine($"{DateTime.Now}: Building benchmark...");
                    var benchmark = BenchmarkBuilder.Build(config);
                    var logger = new SimpleConsoleLogger();
                    logger.Logged += (s, m) => debugInfo.AppendLine(m);
                    benchmark.Logger = logger;
                    Console.WriteLine($"{DateTime.Now}: Starting benchmark...");
                    //HACK: temporarily disable benchmark
                    var results = benchmark.Execute(true);
                    Console.WriteLine($"{DateTime.Now}: Generating results...");
                    string filename = config.ToShortString() + ".xlsx";
                    benchmark.Dump(filename, config.ToKeyValuePairs());
                    Console.WriteLine($"{DateTime.Now}: Uploading results...");

                    var blob = Container.GetBlockBlobReference(filename);
                    await blob.DeleteIfExistsAsync();
                    await blob.UploadFromFileAsync(filename);
                }
                catch (Exception exc)
                {
                    debugFileName = $"{debugFileName ?? Guid.NewGuid().ToString()}.txt";
                    debugInfo.AppendLine(exc.ToString());
                    File.WriteAllText(debugFileName, debugInfo.ToString());
                    var blob = Container.GetBlockBlobReference(debugFileName);
                    await blob.DeleteIfExistsAsync();
                    await blob.UploadFromFileAsync(debugFileName);
                }

                await Queue.DeleteMessageAsync(msg);
            }
        }
    }
}
