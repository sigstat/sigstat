using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SigStat.Common.Loaders;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public static class OnlineOnlineBenchmark
    {
        public static BenchmarkResults BenchMarkWithAllSigners(bool isoptimal, DataSetLoader onlineLoader)
        {
            Console.WriteLine("Online - online benchmark started");

            var onlineSigners = onlineLoader.EnumerateSigners().ToList();

            var onlinePipeline = FusionPipelines.GetOnlinePipeline();

            var benchmark = FusionPipelines.GetBenchmark(onlineSigners, isoptimal);

            foreach (var onSigner in onlineSigners)
            {
                try
                {
                    Parallel.ForEach(onSigner.Signatures, onSig =>
                    {
                        onlinePipeline.Transform(onSig);
                    }
                    );
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
