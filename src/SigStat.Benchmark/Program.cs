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

        public static bool AzureAuth(OptionsBase o)
        {
            if (o.AccountKey.EndsWith(".txt"))
            {
                if (!File.Exists(o.AccountKey))
                {
                    Console.WriteLine($"Account key file '{o.AccountKey}' does not exist.");
                    return false;
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
                Console.WriteLine($"Account key is invalid. ");
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<MonitorOptions, WorkerOptions, GeneratorOptions, AnalyserOptions>(args)
                .MapResult<OptionsBase, Task>(o =>
                {
                    if (!AzureAuth(o)) return Task.FromResult(-1);

                    return o.RunAsync();
                },
                errs => Task.FromResult(-1));
            Console.WriteLine("Execution finished. Press any key to exit the application...");
            Console.ReadKey();
        }
    }

}

