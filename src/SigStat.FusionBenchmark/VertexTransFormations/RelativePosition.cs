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
    class RelativePosition : PipelineBase, ITransformation
    {

        [Input]
        public FeatureDescriptor<VertexCollection> InputVertices { get; set; }

        [Output("Vertices")]
        public FeatureDescriptor<VertexCollection> OutputVertices { get; set; }

        public static int areas = 12;

        public void Transform(Signature signature)
        {
            this.LogInformation("RelativePosition - transform started");

            var vertices = signature.GetFeature<VertexCollection>(InputVertices);
            foreach (var p in vertices.Values)
            {
                p.RelPos = CalculateFeature(vertices, p);
            }
            signature.SetFeature<VertexCollection>(OutputVertices, vertices);
            this.LogInformation("RelativePosition - transform finished");
        }

        private double[] CalculateFeature(VertexCollection vertices, Vertex vertex)
        {
            int[] cnt = new int[areas];
            cnt.Initialize();
            foreach (var p in vertices.Values)
            {
                if (p.On && p.ID != vertex.ID)
                {
                    cnt[CalculateArea(vertex, p)]++;
                }
            }
            double[] res = new double[areas];
            for (int i = 0; i < areas; i++)
            {
                res[i] = (double)cnt[i] / vertices.Count;
            }
            return res;
        }

        private int CalculateArea(Vertex p, Vertex q)
        {
            double dy = (double)(q.Pos.Y - p.Pos.Y);
            double dx = (double)(q.Pos.X - p.Pos.X);
            double angle = Math.Atan2(dy, dx);
            if (angle < 0.0)
            {
                angle += 2 * Math.PI;
            }
            int idx = (int)(angle / (2 * Math.PI / areas));
            return idx;
        }
    }
}
