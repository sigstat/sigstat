using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SigStat.FusionBenchmark.VisualHelpers;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.FusionFeatureExtraction;
using SigStat.Common.Loaders;

namespace SigStat.FusionBenchmark.FusionDemos
{

    class OfflineOfflineBenchmark
    {
        public static BenchmarkResults BenchMarkingWithAllSigners(bool isoptimal, DataSetLoader offlineLoader, DataSetLoader onlineLoader)
        {
            var offlineSigners = offlineLoader.EnumerateSigners().ToList();
            var onlineSigners = onlineLoader.EnumerateSigners().ToList();

            var benchmark = FusionPipelines.GetBenchmark(offlineSigners, isoptimal);

            var offlinePipeline = FusionPipelines.GetOfflinePipeline();
            var onlinePipeline = FusionPipelines.GetOnlinePipeline();
            var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, false, 0);


            foreach (var offSigner in offlineSigners)
            {
                Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                var onSigner = onlineSigners.Find(signer => signer.ID == offSigner.ID);
                Parallel.ForEach(offSigner.Signatures, offSig =>
                {
                    offlinePipeline.Transform(offSig);
                    var onSig = onSigner.Signatures.Find(sig => sig.ID == offSig.ID);
                    var onToOnPipeline = FusionPipelines.GetHackedOnToOnPipeline(offSig.GetFeature(FusionFeatures.Bounds));
                    onToOnPipeline.Transform(onSig);
                    onlinePipeline.Transform(onSig);
                }
                );
                Parallel.ForEach(offSigner.Signatures, offSig =>
                {
                    fusionPipeline.Transform(offSig);
                    onlinePipeline.Transform(offSig);
                }
                );

               
                var listWithOnlySigner = new List<Signer>() { offSigner };
                var onlySigBenchmark = FusionPipelines.GetBenchmark(listWithOnlySigner, true);
                var onlyRes = onlySigBenchmark.Execute();
                TxtHelper.Save(TxtHelper.BenchmarkResToLines(onlyRes), "offoff" + offSigner.ID);
            }
            return benchmark.Execute();
        }


        
    }
}
