using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    [Verb("work", HelpText = "Start worker")]
    class WorkerOptions : OptionsBase
    {
        [Option('o', "outputDir", Required = false, Default = "", HelpText = "Output directory")]
        public string OutputDirectory { get; set; }

        public override Task RunAsync()
        {
            return Worker.RunAsync(OutputDirectory);
        }
    }
}
