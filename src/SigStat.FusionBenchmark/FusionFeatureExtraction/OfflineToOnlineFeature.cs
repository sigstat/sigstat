using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class OfflineToOnlineFeature : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputTrajectory { get; set; }

        [Input]
        public int InputScale { get; set; }

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; }

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; }

        [Output("Button")]
        public FeatureDescriptor<List<bool>> OutputButton { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("FusionFeatureTransform - transform started");
            var xs = new List<double>();
            var ys = new List<double>();
            var bs = new List<bool>();
            var trajectory = signature.GetFeature<List<Vertex>>(InputTrajectory);
            for (int i = 0, cnt = 0; i < trajectory.Count; i++)
            {
                if (ScalePredicate(trajectory, i, cnt))
                {
                    xs.Add(trajectory[i].Pos.X);
                    ys.Add(trajectory[i].Pos.Y);
                    bs.Add(trajectory[i].On);
                    cnt = 0;
                }
                else
                {
                    cnt++;
                }
            }
            signature.SetFeature<List<double>>(OutputX, xs);
            signature.SetFeature<List<double>>(OutputY, ys);
            signature.SetFeature<List<bool>>(OutputButton, bs);
            this.LogInformation("FusionFeatureTransform - transform finished");
        }

        private bool ScalePredicate(List<Vertex> trajectory, int idx, int cnt)
        {
            return  (cnt >= InputScale) ||
                    (idx == 0 || idx == trajectory.Count - 1) ||
                    (idx > 0 && !Vertex.AreNeighbours(trajectory[idx - 1], trajectory[idx]) &&
                                                    !trajectory[idx - 1].Equals(trajectory[idx])) ||
                    (idx < trajectory.Count - 1 && !Vertex.AreNeighbours(trajectory[idx], trajectory[idx + 1]) &&
                                                !trajectory[idx].Equals(trajectory[idx + 1]));
        }
    }
}
