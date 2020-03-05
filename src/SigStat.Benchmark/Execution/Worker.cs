using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Model;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark
{
    class Worker
    {
        static TimeSpan timeOut = new TimeSpan(0, 30, 0);

        static Queue<VerifierBenchmark> LocalBenchmarks = new Queue<VerifierBenchmark>();

        static BenchmarkBuilder benchmarkBuilder;
        static VerifierBenchmark CurrentBenchmark;
        static string CurrentBenchmarkId;
        static BenchmarkResults CurrentResults;
        static string CurrentResultType;
        static int ProcessId;
        static GrammarEngine.ProductionRule[] rules;

        internal static async Task RunAsync(int procId, int maxThreads)
        {
            //stop worker process after 3 days
            DateTime stopTime = DateTime.Now.AddHours(71);

            ProcessId = procId;
            //delayed start
            await Task.Delay(100 * ProcessId);

            string rulesString = await BenchmarkDatabase.GetGrammarRules();
            rules = GrammarEngine.ParseRules(rulesString);

            benchmarkBuilder = new BenchmarkBuilder();

            Console.WriteLine($"{DateTime.Now}: Worker is running.");
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("Press 'A' to abort.");
            }

            while (DateTime.Now < stopTime)
            {
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine(DateTime.Now.ToString());

                if (!Console.IsInputRedirected)
                {
                    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.A)
                    {
                        Console.WriteLine($"{DateTime.Now}: Aborting...");
                        return;
                    }
                }


                var logger = new SimpleConsoleLogger();//default log level: Information
                logger.Logged += (m, e, l) =>
                {
                    debugInfo.AppendLine(m);
                    if (e != null)
                        debugInfo.AppendLine(e.ToString());
                    if (l == LogLevel.Error || l == LogLevel.Critical)
                        CurrentResultType = "Error";
                };

                CurrentBenchmark = await GetNextBenchmark();
                if (CurrentBenchmark is null)
                    return;
                CurrentBenchmark.Logger = logger;

                Console.WriteLine($"{DateTime.Now}: Starting benchmark...");

                try
                {
                    if (maxThreads>0)
                        CurrentResults = CurrentBenchmark.Execute(maxThreads);
                    else
                        CurrentResults = CurrentBenchmark.Execute(true);//default: no restriction 
                    CurrentResultType = "Success";
                }
                catch (Exception exc)
                {
                    CurrentResultType = "Error";
                    Console.WriteLine(exc.ToString());
                    debugInfo.AppendLine(exc.ToString());
                    //Save to File?

                    await BenchmarkDatabase.SendLog(ProcessId, CurrentBenchmarkId, debugInfo.ToString(), markExceptionOccured: true);

                    continue;
                }

                //Send log even if no error
                //await BenchmarkDatabase.SendLog(ProcessId, CurrentBenchmarkId, debugInfo.ToString(), markErrorOccured: false);

                Console.WriteLine($"{DateTime.Now}: Writing results to MongoDB...");
                await BenchmarkDatabase.SendResults(procId, CurrentBenchmarkId, CurrentResultType, CurrentResults);

                //LogProcessor.Dump(logger);
                // MongoDB 
                // TableStorage
                // Json => utólag, json-ben szűrni lehet
                // DynamoDB ==> MongoDB $$$$$$$
                // DateTime, MachineName, ....ExecutionTime,..., ResultType, Result json{40*60 táblázat}
                //benchmark.Dump(filename, config.ToKeyValuePairs());

            }
        }

        internal static async Task<VerifierBenchmark> GetNextBenchmark()
        {
            Console.WriteLine($"{DateTime.Now}: Looking for unprocessed configurations...");
            string config = null;

            int tries = 3;
            while (tries > 0)
            {//Try get next configuration 3 times
                config = await BenchmarkDatabase.LockNextConfig(ProcessId);
                if (config == null)
                    tries--;
                else break;
            }

            if (config == null)
            {
                Console.WriteLine($"{DateTime.Now}: No more tasks in queue.");
                return null;
            }

            CurrentBenchmarkId = config;
            Console.WriteLine($"{DateTime.Now}: Building benchmark {CurrentBenchmarkId} ...");
            var configDict = GrammarEngine.ParseSentence(CurrentBenchmarkId, rules);
            return benchmarkBuilder.Build(configDict);
        }
    }
}
