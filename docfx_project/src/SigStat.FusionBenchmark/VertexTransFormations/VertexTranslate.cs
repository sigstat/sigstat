using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.VertexTransFormations
{
    [JsonObject(MemberSerialization.OptOut)]
    class VertexTranslate : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Input]
        public FeatureDescriptor<Point> InputCog{ get; set; }

        [Output("Vertices")]
        public FeatureDescriptor<List<Vertex>> OutputVertices { get; set; }

        public void Transform(Signature signature)
        {
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            var cog = signature.GetFeature<Point>(InputCog);
            foreach (var vertex in vertices)
            {
                vertex.PosF = CalculateFeature(vertex.Pos, cog);
            }
            signature.SetFeature<List<Vertex>>(OutputVertices, vertices);
        }

        private static PointF CalculateFeature(Point pos, Point cog)
        {
            PointF res = new PointF();
            res.X = pos.X - cog.X;
            res.Y = pos.Y - cog.Y;
            return res;
        }
    }
}
