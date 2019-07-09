using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    /// <summary>
    /// Command line options for monitor mode
    /// </summary>
    [Verb("monitor", HelpText = "Monitoring mode for checking HTCondor jobs or Azure Queue status.")]
    public class MonitorOptions : OptionsBase
    {
        [Option('c', "configsPath", Required = false, Default = null, HelpText = "Path of benchmark configurations directory. Defaults to Azure queue.")]
        public string ConfigsPath { get; set; }
        [Option('r', "resultsPath", Required = false, Default = null, HelpText = "Path of benchmark results directory. Defaults to Azure queue.")]
        public string ResultsPath { get; set; }
        public override Task RunAsync()
        {
            return Monitor.RunAsync(ConfigsPath, ResultsPath);
        }
    }
}
