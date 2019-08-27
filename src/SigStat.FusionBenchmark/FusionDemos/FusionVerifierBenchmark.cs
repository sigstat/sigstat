using SigStat.Common;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public class FusionBenchmarkResults
    {
        public BenchmarkResults OffOffResults { get; set; }
        public BenchmarkResults OffOnResults { get; set; }
        public BenchmarkResults OnOffResults { get; set; }
        public BenchmarkResults OnOnResults { get; set; }
    }

    public static class FusionVerifierBenchmark
    {

        public static FusionBenchmarkResults BenchMarkingWithAllSigners(bool isoptimal)
        {
            var offlineLoader = FusionPipelines.GetOfflineLoader();
            var onlineLoader = FusionPipelines.GetOnlineLoader();

            var offlineSigners = offlineLoader.EnumerateSigners().ToList();
            var onlineSigners = onlineLoader.EnumerateSigners().ToList();


            var offlinePipeline = FusionPipelines.GetOfflinePipeline();
            var onlinePipeline = FusionPipelines.GetOnlinePipeline();
            var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, false, "001");

            var offOnSigners = new List<Signer>();
            var onOffSigners = new List<Signer>();

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

                var strokepairingdists = new StrokePairingDistances
                {
                    InputOfflineTrajectory = FusionFeatures.Trajectory,
                    InputOnlineTrajectory = FusionFeatures.Trajectory,
                    OfflineSignatures = offSigner.Signatures,
                    OnlineSignatures = onSigner.Signatures
                };
                var pairingRes = strokepairingdists.Calculate();
                TxtHelper.Save(TxtHelper.TuplesToLines(pairingRes), "pairdist_" + offSigner.ID);
                ///
                var offonSigner = FusionPipelines.GetMixedSigner(offSigner, onSigner);
                var onoffSigner = FusionPipelines.GetMixedSigner(onSigner, offSigner);
                offOnSigners.Add(offonSigner);
                onOffSigners.Add(onoffSigner);

                var onlyoffoff = FusionPipelines.GetBenchmarkWithOnlySigner(offSigner, isoptimal);
                var onlyoffon = FusionPipelines.GetBenchmarkWithOnlySigner(offonSigner, isoptimal);
                var onlyonoff = FusionPipelines.GetBenchmarkWithOnlySigner(onoffSigner, isoptimal);
                var onlyonon = FusionPipelines.GetBenchmarkWithOnlySigner(onSigner, isoptimal);

                TxtHelper.Save(TxtHelper.BenchmarkResToLines(onlyoffoff.Execute()), "fusion_offoff_" + offSigner.ID);
                TxtHelper.Save(TxtHelper.BenchmarkResToLines(onlyoffon.Execute()), "fusion_offon_" + offSigner.ID);
                TxtHelper.Save(TxtHelper.BenchmarkResToLines(onlyonoff.Execute()), "fusion_onoff_" + offSigner.ID);
                TxtHelper.Save(TxtHelper.BenchmarkResToLines(onlyonon.Execute()), "fusion_onon_" + offSigner.ID);
                ////
                var distViewer = FusionPipelines.GetDistanceMatrixViewer(onSigner.Signatures, offSigner.Signatures);
                TxtHelper.Save(TxtHelper.ArrayToLines(distViewer.Calculate()), "distancematrix" + offSigner.ID);


            }


            var offoffBenchmark = FusionPipelines.GetBenchmark(offlineSigners, isoptimal);
            var offonBenchmark = FusionPipelines.GetBenchmark(offOnSigners, isoptimal);
            var onoffBenchamrk = FusionPipelines.GetBenchmark(onOffSigners, isoptimal);
            var ononBenchmark = FusionPipelines.GetBenchmark(onlineSigners, isoptimal);


            return new FusionBenchmarkResults
            {
                OffOffResults = offoffBenchmark.Execute(),
                OffOnResults = offonBenchmark.Execute(),
                OnOffResults = onoffBenchamrk.Execute(),
                OnOnResults = ononBenchmark.Execute()
            };

        }

    }
}
