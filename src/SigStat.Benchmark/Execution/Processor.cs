using SigStat.Benchmark.Helpers;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Benchmark.Execution
{
    class Processor
    {

        internal static async Task RunAsync()
        {

            Console.WriteLine($"{DateTime.Now}: Gathering results for experiment {Program.Experiment}...");
            var reportCount = await BenchmarkDatabase.CountFinished();
            var progress = ProgressHelper.StartNew(reportCount, 10);
            foreach (var report in BenchmarkDatabase.GetBenchmarkReports())
            {
                //Console.WriteLine(report.Config);


                progress.Value++;
            }
         
        }
    }
}
