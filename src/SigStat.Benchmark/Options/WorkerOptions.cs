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
        [Option('i', "inputDir", Required = false, Default = "benchmarks", HelpText = "Input directory for loading benchmarks locally. Azure Queue will be used by default.")]
        public string InputDirectory { get; set; }

        [Option('o', "outputDir", Required = false, Default = "results", HelpText = "Output directory for storing benchmark results locally.")]
        public string OutputDirectory { get; set; }

        [Option('p', "procId", Required = false, Default = 0, HelpText = "Worker Process Id.")]
        public int ProcessId { get; set; }

        [Option('t', "maxThreads", Required = false, Default = 0, HelpText = "Maximum number of threads the worker may use. The default value makes no restrictions.")]
        public int MaxThreads { get; set; }

        public override Task RunAsync()
        {
            return Worker.RunAsync(InputDirectory, OutputDirectory, ProcessId, MaxThreads);
        }
    }
}
