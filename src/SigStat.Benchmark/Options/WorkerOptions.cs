using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Options
{
    /// <summary>
    /// Command line options for worker mode
    /// </summary>
    [Verb("work", HelpText = "Worker mode for processing benchmarks.")]
    class WorkerOptions : OptionsBase
    {
        [Option('p', "procId", Required = false, Default = 0, HelpText = "Worker Process Id.")]
        public int ProcessId { get; set; }

        [Option('t', "maxThreads", Required = false, Default = 0, HelpText = "Maximum number of threads the worker may use. The default value makes no restrictions.")]
        public int MaxThreads { get; set; }

        public override Task RunAsync()
        {
            return Worker.RunAsync(ProcessId, MaxThreads);
        }
    }
}
