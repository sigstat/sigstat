using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    /// <summary>
    /// Command line options for generator mode
    /// </summary>
    [Verb("generate", HelpText = "Generator mode for generating benchmarks to process.")]
    class GeneratorOptions : OptionsBase
    {
        [Option('d', "ConnectionString", Required = false, Default = null, HelpText = "MongoDB connection string in Uri format (see: https://docs.mongodb.com/manual/reference/connection-string)")]
        public string ConnectionString { get; set; }

        public override Task RunAsync()
        {
            return BenchmarkGenerator.RunAsync(ConnectionString);
        }
    }
}
