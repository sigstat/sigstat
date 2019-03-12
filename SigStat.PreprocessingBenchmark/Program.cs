using CommandLine;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Threading.Tasks;

namespace SigStat.PreprocessingBenchmark
{
    class Program
    {
        public static CloudStorageAccount Account;
        public static string Experiment;
        public enum Mode
        {
            Worker,
            Monitor,
            JobGenerator
        }
        public class Options
        {
            [Option('m', "mode", Default =Mode.Worker, Required = false, HelpText = "Monitor|Worker|GenerateJobs")]
            public Mode Mode { get; set; }

            [Option('k', "key", Required = true, HelpText = "Azure key to use for managing the job.")]
            public string AccountKey { get; set; }

            [Option('a', "account", Required = false, Default = "preprocessingbenchmark", HelpText = "Azure storage account to use for managing the job.")]
            public string AccountName { get; set; }

            [Option('e', "experiment", Required = false, Default = "default", HelpText = "Unique name for the experiment")]
            public string Experiment { get; set; }


        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args)
                .MapResult<Options,Task>(o =>
                   {
                       var credentials = new StorageCredentials(o.AccountName, o.AccountKey);
                       Account = new CloudStorageAccount(credentials, true);
                       Experiment = o.Experiment;

                       switch (o.Mode)
                       {
                           case Mode.Monitor:
                               Console.WriteLine("Running Monitor");
                               return Monitor.RunAsync();
                           case Mode.Worker:
                               Console.WriteLine("Running Worker");
                               return Worker.RunAsync();
                           case Mode.JobGenerator:
                               Console.WriteLine("Running JobGenerator");
                               return JobGenerator.RunAsync();
                           default:
                               throw new NotSupportedException();
                       }
                   },
                   errs => Task.FromResult(-1));
            Console.WriteLine("Execution finished. Press any key to exit the application...");
            Console.ReadKey();
        }

    }
}
