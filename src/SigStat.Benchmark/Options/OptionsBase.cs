using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    public abstract class OptionsBase
    {
        [Option('k', "key", Required = false, HelpText = "Azure key to use for managing the job.")]
        public string AccountKey { get; set; }

        [Option('a', "account", Required = false, Default = "preprocessingbenchmark", HelpText = "Azure storage account to use for managing the job.")]
        public string AccountName { get; set; }

        [Option('e', "experiment", Required = false, Default = "default", HelpText = "Unique name for the experiment.")]
        public string Experiment { get; set; }

        public abstract Task RunAsync();
    }
}
