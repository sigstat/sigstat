using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    [Verb("generate", HelpText = "Start job generator")]
    class GeneratorOptions : OptionsBase
    {
        [Option('o', "outputDir", Required = false, Default = "Benchmarks", HelpText = "Output directory")]
        public string OutputDirectory { get; set; }

        public override Task RunAsync()
        {
            return BenchmarkGenerator.RunAsync(OutputDirectory);
        }
    }
}
