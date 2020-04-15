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
        [Option('r', "report", Required = false, Default = "report.xlsx", HelpText = "Name of the generated report file (e.g. something.xlsx)")]
        public string ReportFilePath { get; set; }

        public override async Task RunAsync()
        {
            await Analyser.RunAsync(ReportFilePath);
        }
    }
}
