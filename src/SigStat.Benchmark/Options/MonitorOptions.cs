using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    [Verb("monitor", HelpText = "Start monitoring")]
    public class MonitorOptions : OptionsBase
    {
        public override Task RunAsync()
        {
            return Monitor.RunAsync();
        }
    }
}
