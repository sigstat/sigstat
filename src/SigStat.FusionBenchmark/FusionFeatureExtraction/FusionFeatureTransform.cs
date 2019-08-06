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
    class FusionFeatureTransform : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputTrajectory { get; set; }

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
            foreach (var p in trajectory)
            {
                xs.Add((double)p.PosF.X);
                ys.Add((double)p.PosF.Y);
                bs.Add(p.On);
            }
            signature.SetFeature<List<double>>(OutputX, xs);
            signature.SetFeature<List<double>>(OutputY, ys);
            signature.SetFeature<List<bool>>(OutputButton, bs);
            this.LogInformation("FusionFeatureTransform - transform finished");
        }
    }
}
