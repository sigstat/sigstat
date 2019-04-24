using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    [Verb("work", HelpText = "Worker mode for processing benchmarks.")]
    class WorkerOptions : OptionsBase
    {
        [Option('i', "inputDir", Required = false, HelpText = "Input directory for loading benchmarks locally. Azure Queue will be used by default.")]
        public string InputDirectory { get; set; }

        [Option('o', "outputDir", Required = false, Default = "Results", HelpText = "Output directory for storing benchmark results locally.")]
        public string OutputDirectory { get; set; }

        public override Task RunAsync()
        {
            return Worker.RunAsync(InputDirectory, OutputDirectory);
        }
    }
}
