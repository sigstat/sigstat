using SigStat.Benchmark.Helpers;
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
        enum Action { Run, Refresh,Eta, Abort };

        internal static async Task RunAsync()
        {        

            Action action = Action.Eta;
            DateTime lastRefresh = DateTime.Now.AddDays(-1);

            Console.WriteLine("Monitor is running. Press 'r' to force a refresh, press 'e' to calculate ETA, press any other key to quit");
            while (action != Action.Abort)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            action = Action.Refresh;
                            break;
                        case ConsoleKey.E:
                            action = Action.Eta;
                            break;
                        default:
                            action = Action.Abort;
                            break;
                    }
                }

                if (action == Action.Eta)
                {
                    var stats = await BenchmarkDatabase.GetExecutionStatisticsAsync();
                    var lockedCount = await BenchmarkDatabase.CountLocked();
                    var queuedCount = await BenchmarkDatabase.CountQueued();
                    var averageJobSeconds = stats.TotalMilliseconds / stats.Count / 1000;

                    Console.WriteLine($"Finished {stats.Count} records in {stats.TotalMilliseconds / 1000 / 60 / 60} processing hours."); 
                    Console.WriteLine($"The slowest job took {stats.MaxMilliseconds/1000/60} minutes, the average job length is {averageJobSeconds} seconds.");
                    Console.WriteLine($"Assuming {lockedCount} workers, the remaining {queuedCount} items will be processed in {queuedCount*averageJobSeconds/lockedCount/60/60} hours");
                    action = Action.Run;
                }
                else if (((DateTime.Now - lastRefresh).TotalMinutes > 1) || action == Action.Refresh)
                {
                    var queued = BenchmarkDatabase.CountQueued();
                    var locked = BenchmarkDatabase.CountLocked();
                    var faulted = BenchmarkDatabase.CountFaulted();
                    var finished = BenchmarkDatabase.CountFinished();
                    Task.WaitAll(queued, locked, faulted, finished);
                    var total = queued.Result + locked.Result + faulted.Result + finished.Result;
                    var percent = 100 * finished.Result / total;

                    Console.WriteLine(
                        $"{DateTime.Now}: Queued: {queued.Result} Locked: {locked.Result} Faulted: {faulted.Result} Finished: {finished.Result} Progress: {percent}%");

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
