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
        [Option('o', "outputDir", Required = false, Default = "Benchmarks", HelpText = "Output directory for storing generated benchmarks locally.")]
        public string OutputDirectory { get; set; }

        public override Task RunAsync()
        {
            return BenchmarkGenerator.RunAsync(OutputDirectory);
        }
    }
}
