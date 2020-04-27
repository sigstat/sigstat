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
        enum Action { Run, Refresh, Eta, Exit };

        internal static async Task RunAsync()
        {

            Action action = Action.Eta;
            DateTime lastRefresh = DateTime.Now.AddDays(-1);

            Console.WriteLine("Monitor is running. [R]efresh, [E]TA, E[x]it");
            while (action != Action.Exit)
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
                        case ConsoleKey.X:
                            action = Action.Exit;
                            break;
                    }
                }

                if (action == Action.Eta)
                {
                    action = Action.Run;
                    var stats = await BenchmarkDatabase.GetExecutionStatisticsAsync();
                    if (stats.Count == 0)
                    {
                        Console.WriteLine("Execution statistics will be available as soon as the first job finishes");
                        continue;
                    }
                    var lockedCount = await BenchmarkDatabase.CountLocked();
                    var queuedCount = await BenchmarkDatabase.CountQueued();
                    var averageJobSeconds = stats.TotalMilliseconds / stats.Count / 1000;

                    Console.WriteLine($"Finished {stats.Count} records in {stats.TotalMilliseconds / 1000 / 60 / 60} processing hours.");
                    Console.WriteLine($"The slowest job took {stats.MaxMilliseconds / 1000 / 60} minutes, the average job length is {averageJobSeconds} seconds.");
                    if (lockedCount>0)
                        Console.WriteLine($"Assuming {lockedCount} workers, the remaining {queuedCount} items will be processed in {queuedCount * averageJobSeconds / lockedCount / 60 / 60} hours");
                    else
                        Console.WriteLine("No active workers");
                    Console.WriteLine($"Collection size is {SizeToString(stats.Size)} with storage size of {SizeToString(stats.StorageSize)}");
                    Console.WriteLine($"Expired locks:{stats.ExpiredLockCount}");


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
        public static string SizeToString(long size)
        {
            if (size < 1024)
                return $"{size} bytes";
            else if (size < 1024 * 1024)
                return $"{size / 1024} kb";
            else if (size < 1024 * 1024 * 1024)
                return $"{size / 1024 / 1024} Mb";
            else
                return $"{size / 1024 / 1024 / 1024} Gb";
        }
    }
}
