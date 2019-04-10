using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
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
        static string OutputDirectory;

        internal static async Task RunAsync(string outputDir)
        {
            OutputDirectory = outputDir;

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

            if (!string.IsNullOrEmpty(OutputDirectory) && !Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);


            DateTime lastRefresh = DateTime.Now.AddDays(-1);

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
                    string filename = config.ToShortString() + ".xlsx";
                    if (!string.IsNullOrWhiteSpace(OutputDirectory))
                        filename = Path.Combine(OutputDirectory, filename);
                    benchmark.Dump(filename, config.ToKeyValuePairs());
                    Console.WriteLine($"{DateTime.Now}: Uploading results...");

                    string cloudFilename = config.ToShortString() + ".xlsx";
                    var blob = Container.GetBlockBlobReference(cloudFilename);
                    await blob.DeleteIfExistsAsync();
                    await blob.UploadFromFileAsync(filename);
                }
                catch (Exception exc)
                {
                    debugInfo.AppendLine(exc.ToString());
                    error = true;
                }
                if (error)
                {
                    debugFileName = $"{debugFileName ?? Guid.NewGuid().ToString()}.txt";
                    var cloudFilename = debugFileName;

                    if (!string.IsNullOrWhiteSpace(OutputDirectory))
                        debugFileName = Path.Combine(OutputDirectory, debugFileName);
                    File.WriteAllText(debugFileName, debugInfo.ToString());
                    var blob = Container.GetBlockBlobReference(cloudFilename);
                    await blob.DeleteIfExistsAsync();
                    await blob.UploadFromFileAsync(debugFileName);

                }

                await Queue.DeleteMessageAsync(msg);
            }
        }
    }
}
