using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    class StrokePairingDistances: PipelineBase
    {
        [Input]
        public List<Signature> OnlineSignatures { get; set; }

        [Input]
        public List<Signature> OfflineSignatures { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputOnlineTrajectory { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputOfflineTrajectory { get; set; }

        public void Calculate()
        {
            var distances = new double[OfflineSignatures.Count];
            Parallel.For(0, OfflineSignatures.Count,
            idx =>
            {
                var offlineSig = OfflineSignatures[idx];
                var offlineTrajectory = offlineSig.GetFeature<List<Vertex>>(InputOfflineTrajectory);
                var onlineSig = OnlineSignatures.Find(sig => sig.ID == offlineSig.ID && sig.Signer.ID == offlineSig.Signer.ID);
                var onlineTrajectory = onlineSig.GetFeature<List<Vertex>>(InputOnlineTrajectory);
                distances[idx] = DtwPy.Dtw(offlineTrajectory.GetOriginalXYs(), onlineTrajectory.GetOriginalXYs(), DtwPy.EuclideanDistance);
            }
            );
            for (int i = 0; i < OfflineSignatures.Count; i++)
            {
                Console.WriteLine(OfflineSignatures[i].Signer.ID + " " + OfflineSignatures[i].ID + " - {0}",
                                    distances[i]);
            }
        }

    }
}
