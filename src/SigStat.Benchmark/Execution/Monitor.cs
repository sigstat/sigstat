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

        internal static async Task RunAsync(string configsPath = null, string resultsPath = null)
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

                    //TODO: mongodb get collection configs, count d=>d.Count

                    //var finishedCnt = Directory.EnumerateFiles(resultsPath, "*.xlsx").Count();
                    //var lockedCnt = Directory.EnumerateFiles(configsPath, "*.json.lock").Count();
                    //var queuedCnt = Directory.EnumerateFiles(configsPath, "*.json").Count();
                    //Console.WriteLine($"{DateTime.Now}: Finished: {finishedCnt}. In progress: {lockedCnt}. Queued: {queuedCnt}");

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
