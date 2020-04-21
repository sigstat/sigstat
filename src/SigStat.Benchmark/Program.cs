using System;
using System.Threading.Tasks;
using CommandLine;
using SigStat.Benchmark.Helpers;
using SigStat.Benchmark.Options;

namespace SigStat.Benchmark
{
    class Program
    {
        public static string Experiment { get; set; }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing...");
            await Parser.Default.ParseArguments<MonitorOptions, WorkerOptions, GeneratorOptions, AnalyserOptions>(args)
                .MapResult<OptionsBase, Task>(async o =>
                {
                    Experiment = o.Experiment;
                    try
                    {
                        await BenchmarkDatabase.InitializeConnection(o.Connection);
                        await o.RunAsync();
                    }
                    catch (TimeoutException tex)
                    {
                        //TODO: catch rare mongo exceptions
                        Console.WriteLine($"{DateTime.Now}: Database connection timed out.");
                        Console.WriteLine(tex.Message.ToString());
                    }
                },
                errs => Task.FromResult(-1));
            Console.WriteLine($"{DateTime.Now}: Execution finished.");
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();
            }

        }
    }

}

