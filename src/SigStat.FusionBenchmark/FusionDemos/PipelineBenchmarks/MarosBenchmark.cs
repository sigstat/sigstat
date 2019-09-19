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
            //marosPipeline.Logger = new SimpleConsoleLogger();
            var onlinePipeline = FusionPipelines.GetOnlinePipeline();

            foreach (var offSigner in offlineSigners)
            {
                Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                Parallel.ForEach(offSigner.Signatures, offSig =>
                {
                    marosPipeline.Transform(offSig);
                    var xySaver = FusionPipelines.GetXYSaver();
                    xySaver.Transform(offSig);
                    onlinePipeline.Transform(offSig);
                }
                );
                var listWithOnlySigner = new List<Signer>() { offSigner };
                var onlySigBenchmark = FusionPipelines.GetBenchmark(listWithOnlySigner, true);
                var onlyRes = onlySigBenchmark.Execute();
                TxtHelper.Save(TxtHelper.BenchmarkResToLines(onlyRes), "maros_offoff_09_18" + offSigner.ID);
            }
            return benchmark.Execute();
        }

    }
}
