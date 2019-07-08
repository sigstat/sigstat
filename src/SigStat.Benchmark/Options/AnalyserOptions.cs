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
        [Option('i', "inputDir", Required = false, Default = "results", HelpText = "Path of benchmark results directory.")]
        public string InputDirectory { get; set; }
        [Option('o', "output", Required = false, Default = "Summary.xlsx", HelpText = "Path of summary xlsx file.")]
        public string OutputFile { get; set; }

        public override Task RunAsync()
        {
            return Analyser.RunAsync(InputDirectory, OutputFile);
        }
    }
}
