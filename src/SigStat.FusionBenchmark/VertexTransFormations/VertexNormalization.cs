using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;
using SigStat.FusionBenchmark.FusionMathHelper;
using System.Drawing;

namespace SigStat.FusionBenchmark.VertexTransFormations
{
    internal class VertexNormalization : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Input]
        public Tuple<double, double> InputRange { get; set; }

        [Output("Vertices")]
        public FeatureDescriptor<List<Vertex>> OutputVertices { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("VertexNormalization - transfrom started");
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            double fromRange = InputRange.Item1;
            double toRange = InputRange.Item2;
            var xs = new List<double>();
            var ys = new List<double>();
            vertices.ForEach(vertex => xs.Add((double)vertex.Pos.X));
            vertices.ForEach(vertex => ys.Add((double)vertex.Pos.Y));
            xs = Translations.Normalize(xs, fromRange, toRange);
            ys = Translations.Normalize(ys, fromRange, toRange);
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].PosF = new PointF((float)xs[i], (float)ys[i]);
            }
            signature.SetFeature<List<Vertex>>(OutputVertices, vertices);
            this.LogInformation("VertexNormalization - transfrom finished");
        }

    }
}
