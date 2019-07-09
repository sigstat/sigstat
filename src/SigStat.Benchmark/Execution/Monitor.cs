using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SigStat.Benchmark
{
    class Monitor
    {
        static CloudBlobContainer Container;
        static CloudQueue Queue;
        enum Action { Run, Refresh, Abort };

        internal static async Task RunAsync(string configsPath = null, string resultsPath = null)
        {
            if (Program.Offline)
            {
                if(configsPath==null || resultsPath == null || !Directory.Exists(configsPath) || !Directory.Exists(resultsPath))
                {
                    Console.WriteLine("Missing path.");
                    return;
                }
            }
            else
            {
                //init azure client

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

            Action action = Action.Run;
            DateTime lastRefresh = DateTime.Now.AddDays(-1);

            Console.WriteLine("Monitor is running. Press 'r' to force a refresh, press any other key to quit");
            while (action != Action.Abort)
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

                if (((DateTime.Now - lastRefresh).TotalMinutes > 1) || action == Action.Refresh)
                {
                    if (Program.Offline)
                    {
                        var finishedCnt = Directory.EnumerateFiles(resultsPath, "*.xlsx").Count();
                        var lockedCnt = Directory.EnumerateFiles(configsPath, "*.json.lock").Count();
                        var queuedCnt = Directory.EnumerateFiles(configsPath, "*.json").Count();
                        Console.WriteLine($"{DateTime.Now}: Finished: {finishedCnt}. In progress: {lockedCnt}. Queued: {queuedCnt}");
                    }
                    else
                    {
                        await Queue.FetchAttributesAsync();
                        Console.WriteLine($"{DateTime.Now}: {Queue.ApproximateMessageCount} items are in the queue. (q-quit, r-refresh)");
                    }
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
