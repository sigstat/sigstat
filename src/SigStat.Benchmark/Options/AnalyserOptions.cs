using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    /// <summary>
    /// Command line options for analyser mode
    /// </summary>
    [Verb("analyse", HelpText = "Analyser mode for analysing benchmark results.")]
    public class AnalyserOptions : OptionsBase
    {
        //input: results
        //output: summaries

        public override async Task RunAsync()
        {
            await Analyser.RunAsync();
        }
    }
}
