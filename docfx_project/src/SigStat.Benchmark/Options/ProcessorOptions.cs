using CommandLine;
using SigStat.Benchmark.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    /// <summary>
    /// Command line options for analyser mode
    /// </summary>
    [Verb("process", HelpText = "Analyser mode for analysing benchmark results.")]
    public class ProcessorOptions : OptionsBase
    {
        public override async Task RunAsync()
        {
            await Processor.RunAsync();
        }
    }
}
