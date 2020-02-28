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
        // monitor options

        public override Task RunAsync()
        {
            return Monitor.RunAsync();
        }
    }
}
