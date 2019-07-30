using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using Newtonsoft.Json;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.FusionFeatureExtraction;

namespace SigStat.FusionBenchmark.VertexTransFormations
{


    class VertexNormalization: PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<VertexCollection> InputVertices { get; set; }

        [Output("Vertices")]
        public FeatureDescriptor<VertexCollection> OutputVertices { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("VertexNormalization - transform started");
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MaxValue;
            var vertices = signature.GetFeature<VertexCollection>(InputVertices);
            foreach (var p in vertices.Values)
            {
                if (p.On)
                {
                    Point pos = p.Pos;
                    minX = Math.Min(pos.X, minX);
                    minY = Math.Min(pos.Y, minY);
                    maxX = Math.Max(pos.X, maxX);
                    maxY = Math.Max(pos.Y, maxY);
                }
            }
            foreach (var p in vertices.Values)
            {
                if (p.On)
                {
                    p.PosD = new PointD(NormalizeInt(p.Pos.X, minX, maxX), NormalizeInt(p.Pos.Y, minY, maxY));
                }
                else
                {
                    p.PosD = new PointD(-100.0, -100.0);
                }
            }
            signature.SetFeature<VertexCollection>(OutputVertices, vertices);
            this.LogInformation("VertexNormalization - transform finished");

        }

        private static double NormalizeInt(int val, int minVal, int Maxval) {
            val -= minVal;
            Maxval -= minVal;
            float length = Maxval - minVal;
            return val / length;
        }
    }
}
