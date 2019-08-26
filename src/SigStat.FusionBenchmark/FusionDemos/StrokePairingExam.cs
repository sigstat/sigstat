using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SigStat.FusionBenchmark.VisualHelpers;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public static class StrokePairingExam
    {
        public static void CalculateForID(string[] ids)
        {
            var offlineLoader = FusionPipelines.GetOfflineLoader();
            var onlineLoader = FusionPipelines.GetOnlineLoader();

            var offlineSigners = offlineLoader.EnumerateSigners(signer => ids.Contains(signer.ID)).ToList();
            var onlineSigners = onlineLoader.EnumerateSigners(signer => ids.Contains(signer.ID)).ToList();


            var offlinePipeline = FusionPipelines.GetOfflinePipeline();
            var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, false, "001");
            foreach (var offSigner in offlineSigners)
            {
                Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                var onSigner = onlineSigners.Find(signer => signer.ID == offSigner.ID);
                Parallel.ForEach(offSigner.Signatures, offSig =>
                {
                    Console.WriteLine("Preprocess - " + offSig.Signer.ID + "_" + offSig.ID + "started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    offlinePipeline.Transform(offSig);
                    var onSig = onSigner.Signatures.Find(sig => sig.ID == offSig.ID);
                    var onlinePipeline = FusionPipelines.GetOnlineToOfflinePipeline(offSig.GetFeature(FusionFeatures.Bounds));
                    onlinePipeline.Transform(onSig);
                }
                );

                Parallel.ForEach(offSigner.Signatures, offSig =>
                {
                    Console.WriteLine("Process - " + offSig.Signer.ID + "_" + offSig.ID + "started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    fusionPipeline.Transform(offSig);
                }
                );
            }

            foreach (var offSigner in offlineSigners)
            {
                var onSigner = onlineSigners.Find(signer => signer.ID == offSigner.ID);
                var pairingDists = new StrokePairingDistances
                {
                    InputOfflineTrajectory = FusionFeatures.Trajectory,
                    InputOnlineTrajectory = FusionFeatures.BaseTrajectory,
                    OfflineSignatures = offSigner.Signatures,
                    OnlineSignatures = onSigner.Signatures
                };
                pairingDists.Calculate();
            }

        }
    }
}
