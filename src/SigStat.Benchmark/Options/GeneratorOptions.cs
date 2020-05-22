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
        [Option('r', "rules", Required = false, Default = null, HelpText = "Path to the file containing grammar rules that describe the combinations.")]
        public string RulesFilePath { get; set; }

        [Option('b', "batchSize", Required = false, Default = 10, HelpText = "The number of configurations to upload in a single batch.")]
        public int BatchSize { get; set; }
        //TODO: options to clear previous experiment. Deafult: clear all

        public override Task RunAsync()
        {
            return Generator.RunAsync(RulesFilePath, BatchSize);
        }
    }
}
