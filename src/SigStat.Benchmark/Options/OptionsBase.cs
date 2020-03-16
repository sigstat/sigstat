using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    /// <summary>
    /// Base class for command line options containing common options
    /// </summary>
    public abstract class OptionsBase
    {
        private const string DefaultConnectionString = "mongodb://localhost:27017/";

        [Option('c', "connection", Required = false, Default = DefaultConnectionString, HelpText = "MongoDB connection string in Uri format (see: https://docs.mongodb.com/manual/reference/connection-string). Defaults to localhost.")]
        public string ConnectionString { get; set; }

        [Option('e', "experiment", Required = false, Default = "test", HelpText = "Unique name for the experiment. Default: test")]
        public string Experiment { get; set; }

        public abstract Task RunAsync();
    }
}
