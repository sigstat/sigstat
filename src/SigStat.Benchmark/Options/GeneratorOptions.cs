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
        [Option('o', "outputDir", Required = false, Default = "benchmarks", HelpText = "Output directory for storing generated benchmarks locally.")]
        public string OutputDirectory { get; set; }
        [Option('d', "databasePath", Required = false, Default = "/home/1/sigstat/databases/", HelpText = "Path of directory to read local benchmark databases from.")]
        public string DatabasePath { get; set; }

        public override Task RunAsync()
        {
            return BenchmarkGenerator.RunAsync(OutputDirectory, DatabasePath);
        }
    }
}
