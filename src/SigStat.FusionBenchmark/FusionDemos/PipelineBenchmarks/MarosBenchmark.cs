using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos.PipelineBenchmarks
{
    public static class MarosBenchmark
    {
        public static BenchmarkResults BenchmarkingWithAllSigners(bool isoptimal, DataSetLoader offlineLoader)
        {
            var offlineSigners = offlineLoader.EnumerateSigners().ToList();

            var benchmark = FusionPipelines.GetBenchmark(offlineSigners, isoptimal);

            var marosPipeline = FusionPipelines.GetMarosPipeline();
            var onlinePipeline = FusionPipelines.GetOnlinePipeline();

            foreach (var offSigner in offlineSigners)
            {
                try
                {
                    Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    Parallel.ForEach(offSigner.Signatures, offSig =>
                    {
                        marosPipeline.Transform(offSig);
                        onlinePipeline.Transform(offSig);
                    }
                    );
                    Console.WriteLine(offSigner.ID + " finished at " + DateTime.Now.ToString("h:mm:ss tt"));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return benchmark.Execute();
        }

    }
}
