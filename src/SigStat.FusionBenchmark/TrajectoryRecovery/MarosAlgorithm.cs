using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.FusionFeatureExtraction;

namespace SigStat.FusionBenchmark.TrajectoryRecovery
{
    [JsonObject(MemberSerialization.OptOut)]
    class MarosAlgorithm : PipelineBase, ITransformation
    {

        [Input]
        public FeatureDescriptor<List<StrokeComponent>> InputComponents { get; set; }

        [Output("Trajectory")]
        public FeatureDescriptor<List<Vertex>> OutputTrajectory { get; set; }

        public void Transform(Signature signature)
        {
            var components = signature.GetFeature<List<StrokeComponent>>(InputComponents);
            var strokes = new List<Stroke>();
            foreach (var comp in components)
            {
                Stroke lefterStroke = null; 
                comp.Strokes.ForEach(stroke =>
                {
                    if (ReferenceEquals(null, lefterStroke) || stroke.First().Pos.X < lefterStroke.First().Pos.X)
                    { lefterStroke = stroke; }
                }
                );
                strokes.Add(lefterStroke);
            }
            strokes.Sort((p, q) => { return p.First().Pos.X < q.First().Pos.X ? -1 : 1; });
            var trajectory = new List<Vertex>();
            foreach (var stroke in strokes)
            {
                trajectory.AddRange(stroke);
            }
            signature.SetFeature<List<Vertex>>(OutputTrajectory, trajectory);
        }
         
    }
}
