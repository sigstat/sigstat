using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SigStat.PreprocessingBenchmark
{
    class Monitor
    {
        static CloudBlobContainer Container;
        static CloudQueue Queue;
        enum Action { Run, Refresh, Abort};
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

            Action action = Action.Run;
            DateTime lastRefresh = DateTime.Now.AddDays(-1);

            Console.WriteLine("Monitor is running. Press 'r' to force a refresh, press any other key to quit");
            while(action!= Action.Abort)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            action = Action.Refresh;
                            break;
                        default:
                            action = Action.Abort;
                            break;
                    }
                }
                if (((DateTime.Now- lastRefresh).TotalMinutes>1) || action == Action.Refresh)
                {
                    await Queue.FetchAttributesAsync();
                    Console.WriteLine($"{DateTime.Now}: {Queue.ApproximateMessageCount} items are in the queue. (q-quit, r-refresh)");
                    lastRefresh = DateTime.Now;
                    action = Action.Run;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
