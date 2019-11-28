using Newtonsoft.Json;
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
    [JsonObject(MemberSerialization.OptOut)]
    public class StrokePairingDistances: PipelineBase
    {
        [Input]
        public List<Signature> OnlineSignatures { get; set; }

        [Input]
        public List<Signature> OfflineSignatures { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputOnlineTrajectory { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputOfflineTrajectory { get; set; }

        private static readonly double PenWidth = 7.0;

        public List<Tuple<string, string, double>> Calculate()
        {
            var distances = new double[OfflineSignatures.Count];
            Parallel.For(0, OfflineSignatures.Count,
            idx =>
            {
                var offlineSig = OfflineSignatures[idx];
                var offlineTrajectory = offlineSig.GetFeature<List<Vertex>>(InputOfflineTrajectory);
                var onlineSig = OnlineSignatures.Find(sig => sig.ID == offlineSig.ID && sig.Signer.ID == offlineSig.Signer.ID);
                var onlineTrajectory = onlineSig.GetFeature<List<Vertex>>(InputOnlineTrajectory);
                distances[idx] = DtwPy.Dtw(offlineTrajectory.GetOriginalXYs(), onlineTrajectory.GetOriginalXYs(), PenDistance);
            }
            );
            var res = new List<Tuple<string, string, double>>();
            for (int i = 0; i < OfflineSignatures.Count; i++)
            {
                res.Add(new Tuple<string, string, double>(OfflineSignatures[i].Signer.ID, OfflineSignatures[i].ID, distances[i]));
            }
            return res;
        }

        public static double PenDistance(double[] vec1, double[] vec2)
        {
            if (vec1.Length != vec2.Length || vec1.Length != 2)
            {
                throw new ArgumentException();
            }
            double sum = 0.0;
            for (int i = 0; i < vec1.Length; i++)
            {
                sum += (vec2[i] - vec1[i]) * (vec2[i] - vec1[i]);
            }
            double dist = Math.Sqrt(sum);
            return dist < PenWidth ? 0.0 : dist;
        }
    }
}
