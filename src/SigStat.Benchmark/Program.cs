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

        public static void AzureAuth(OptionsBase o)
        {
            if (o.AccountKey is null)
            {
                Offline = true;
                return;
            }

            if (o.AccountKey.EndsWith(".txt"))
            {
                if (!File.Exists(o.AccountKey))
                {
                    Console.WriteLine($"Account key file '{o.AccountKey}' does not exist. Switching to offline mode...");
                    Offline = true;
                    return;
                }
                o.AccountKey = File.ReadAllText(o.AccountKey);
            }

            try
            {
                var credentials = new StorageCredentials(o.AccountName, o.AccountKey);
                Account = new CloudStorageAccount(credentials, true);
                Experiment = o.Experiment;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Azure authentication failed. Switching to offline mode...");
                Offline = true;
                return;
            }
        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<MonitorOptions, WorkerOptions, GeneratorOptions, AnalyserOptions>(args)
                .MapResult<OptionsBase, Task>(o =>
                {
                    AzureAuth(o);
                    return o.RunAsync();
                },
                errs => Task.FromResult(-1));
            Console.WriteLine("Execution finished. Press any key to exit the application...");
            Console.ReadKey();
        }
    }

}

