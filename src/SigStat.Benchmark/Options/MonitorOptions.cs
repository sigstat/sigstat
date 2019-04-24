using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    [Verb("monitor", HelpText = "Monitoring mode for checking Azure Queue status.")]
    public class MonitorOptions : OptionsBase
    {
        public override Task RunAsync()
        {
            return Monitor.RunAsync();
        }
    }
}
