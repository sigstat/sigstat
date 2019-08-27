using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public static class OnlineOnlineBenchmark
    {
        public static BenchmarkResults BenchMarkWithAllSigners(bool isoptimal)
        {
            Console.WriteLine("Online - online benchmark started");
            var onlineLoader = FusionPipelines.GetOnlineLoader();

            var onlineSigners = onlineLoader.EnumerateSigners().ToList();

            var onlinePipeline = FusionPipelines.GetOnlinePipeline();

            var benchmark = FusionPipelines.GetBenchmark(onlineSigners, isoptimal);

            foreach (var onSigner in onlineSigners)
            {
                Parallel.ForEach(onSigner.Signatures, onSig =>
                {
                    onlinePipeline.Transform(onSig);
                }
                );
            }
            return benchmark.Execute();
        }
    }
}
