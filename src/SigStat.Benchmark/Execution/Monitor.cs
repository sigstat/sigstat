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
        enum Action { Run, Refresh, Abort };

        internal static async Task RunAsync()
        {        

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
                    var queued = BenchmarkDatabase.CountQueued();
                    var locked = BenchmarkDatabase.CountLocked();
                    var faulted = BenchmarkDatabase.CountFaulted();
                    var finished = BenchmarkDatabase.CountFinished();
                    Task.WaitAll(queued, locked, faulted, finished);

                    Console.WriteLine(
                        $"{DateTime.Now}: " +
                        $"Queued: {queued.Result}. " +
                        $"Locked: {locked.Result}. " +
                        $"Faulted: {faulted.Result}." +
                        $"Finished: {finished.Result}. ");

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
