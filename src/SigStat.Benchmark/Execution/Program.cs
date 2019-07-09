using CommandLine;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using SigStat.Benchmark.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SigStat.Benchmark
{
    class Program
    {
        public static CloudStorageAccount Account;
        public static string Experiment;
        public static bool Offline = false;

        public static bool AzureAuth(OptionsBase o)
        {
            if (o.AccountKey is null)
            {
                Offline = true;
                return true;
            }

            if (o.AccountKey.EndsWith(".txt"))
            {
                if (!File.Exists(o.AccountKey))
                {
                    Console.WriteLine($"Account key file '{o.AccountKey}' does not exist. Aborting...");
                    return false;
                }
                o.AccountKey = File.ReadAllText(o.AccountKey);
            }

            try
            {
                var credentials = new StorageCredentials(o.AccountName, o.AccountKey);
                Account = new CloudStorageAccount(credentials, true);
                Experiment = o.Experiment;
                return true;
            }
            catch (Exception e)
            {
                File.WriteAllText("log.txt", e.ToString());
                Console.WriteLine("Azure authentication failed. Aborting...");
                return false;
            }
        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<MonitorOptions, WorkerOptions, GeneratorOptions, AnalyserOptions>(args)
                .MapResult<OptionsBase, Task>(o =>
                {
                    if (AzureAuth(o)) return o.RunAsync();
                    else return Task.FromResult(-1);
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

