using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SigStat.PreprocessingBenchmark
{
    class JobGenerator
    {
        static CloudBlobContainer Container;
        static CloudQueue Queue;


        internal static async Task RunAsync()
        {
            Console.WriteLine("Initializing container: "+Program.Experiment);
            var blobClient = Program.Account.CreateCloudBlobClient();
            Container = blobClient.GetContainerReference(Program.Experiment);
            await Container.CreateIfNotExistsAsync();


            Console.WriteLine("Initializing queue: "+Program.Experiment);
            var queueClient = Program.Account.CreateCloudQueueClient();
            Queue = queueClient.GetQueueReference(Program.Experiment);
            await Queue.CreateIfNotExistsAsync();

            await Queue.FetchAttributesAsync();
            if ((Queue.ApproximateMessageCount??0) >0)
            {
                Console.WriteLine($"There are {Queue.ApproximateMessageCount} jobs pending in the queue. Should I clear them? [Y|N]");
                if (Console.ReadKey(true).Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Aborting");
                    return;
                }
                await Queue.ClearAsync();

            }

            Console.WriteLine("Generating benchmark configurations");
            var configs = BenchmarkConfig.GenerateConfigurations();
            Console.WriteLine($"Enqueueing {configs.Count} combinations");
            configs = configs.Take(10).ToList();

            for (int i = 0; i < configs.Count; i++)
            {
                Console.WriteLine($"{i+1}/{configs.Count}");
                await Queue.AddMessageAsync(new CloudQueueMessage(configs[i].ToJsonString()));
            }
            Console.WriteLine("Ready");



        }
    }
}
