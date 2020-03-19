using Microsoft.Extensions.Logging;
using SigStat.Benchmark.Helpers;
using SigStat.Common;
using SigStat.Common.Logging;
using System;
using System.Collections.Generic;
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
                StringBuilder errorLog = new StringBuilder();
                errorLog.AppendLine(DateTime.Now.ToString());

                if (!Console.IsInputRedirected)
                {
                    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.A)
                    {
                        Console.WriteLine($"{DateTime.Now}: Aborting...");
                        return;
                    }
                }

                var reportInformationLogger = new ReportInformationLogger();
                var logger = new CompositeLogger
                {
                    Loggers = new List<ILogger>
                    {
                        reportInformationLogger,
                        new SimpleConsoleLogger()
                    }
                };
                logger.Logged += (m, e, l) =>
                {
                    if (l == LogLevel.Error || l == LogLevel.Critical || e != null)
                    {
                        errorLog.AppendLine(m);
                        if (e != null)
                            errorLog.AppendLine(e.ToString());
                        CurrentResultType = "Error";
                    }
                };

                CurrentBenchmark = await GetNextBenchmark();
                if (CurrentBenchmark is null)
                    return;
                CurrentBenchmark.Logger = logger;

                Console.WriteLine($"{DateTime.Now}: Starting benchmark...");

                try
                {
                    CurrentResultType = "Success";
                    if (maxThreads>0)
                        CurrentBenchmark.Execute(maxThreads);
                    else
                        CurrentBenchmark.Execute(true);//default: no restriction
                }
                catch (Exception exc)
                {
                    CurrentResultType = "Error";
                    Console.WriteLine(exc.ToString());
                    errorLog.AppendLine(exc.ToString());
                }

                if (CurrentResultType == "Success")
                {
                    Console.WriteLine($"{DateTime.Now}: Analyzing report...");
                    var logmodel = LogAnalyzer.GetBenchmarkLogModel(reportInformationLogger.ReportLogs);
                    Console.WriteLine($"{DateTime.Now}: Writing results to MongoDB...");
                    await BenchmarkDatabase.SendResults(ProcessId, CurrentBenchmarkId, logmodel);
                }
                else if (CurrentResultType=="Error")
                {
                    Console.WriteLine($"{DateTime.Now}: Writing error log to MongoDB...");
                    //Save to File?
                    await BenchmarkDatabase.SendErrorLog(ProcessId, CurrentBenchmarkId, errorLog.ToString());
                }

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
