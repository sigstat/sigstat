using SigStat.Common.Loaders;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public static class DistanceViewing
    {
        public static void Calculate(string[] ids, DataSetLoader offlineLoader, DataSetLoader onlineLoader)
        {

            var offlineSigners = offlineLoader.EnumerateSigners(signer => ids.Contains(signer.ID)).ToList();
            var onlineSigners = onlineLoader.EnumerateSigners(signer => ids.Contains(signer.ID)).ToList();

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

                var distViewer = FusionPipelines.GetDistanceMatrixViewer(onSigner.Signatures, offSigner.Signatures);
                TxtHelper.Save(TxtHelper.ArrayToLines(distViewer.Calculate()), "distancematrix" + offSigner.ID);
            }
        }
    }
}
