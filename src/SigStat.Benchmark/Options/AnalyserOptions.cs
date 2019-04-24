using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    [Verb("analyse", HelpText = "Analyser mode for analysing benchmark results.")]
    public class AnalyserOptions : OptionsBase
    {
        [Option('o', "inputDir", Required = false, Default = "", HelpText = "Input directory")]
        public string InputDirectory { get; set; }
        [Option('o', "output", Required = false, Default = "", HelpText = "Output file")]
        public string OutputFile { get; set; }

        public override Task RunAsync()
        {
            return Analyser.RunAsync(InputDirectory, OutputFile);
        }
    }
}
