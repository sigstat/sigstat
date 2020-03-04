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
                    var finished = BenchmarkDatabase.CountFinished();
                    var locked = BenchmarkDatabase.CountLocked();
                    var queued = BenchmarkDatabase.CountQueued();
                    var errors = BenchmarkDatabase.CountExceptions();
                    Task.WaitAll(finished, locked, queued, errors);

                    Console.WriteLine(
                        $"{DateTime.Now}: " +
                        $"Finished: {finished.Result}. " +
                        $"Locked: {locked.Result}. " +
                        $"Queued: {queued.Result}. " +
                        $"Errors: {errors.Result}.");

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
