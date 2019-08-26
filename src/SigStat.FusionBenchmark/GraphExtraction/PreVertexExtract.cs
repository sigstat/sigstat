using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class PreVertexExtract : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<bool[,]> InputSkeleton { get; set; }

        [Output("Contour")]
        public FeatureDescriptor<List<Vertex>> OutputContour { get; set; }

        [Output("OutputWidthOfPen")]
        public FeatureDescriptor<double> OutputWidthOfPen { get; set; }


        public void Transform(Signature signature)
        {
            this.LogInformation("PreVertexExtract - transform started.");
            var skeleton = signature.GetFeature<bool[,]>(InputSkeleton);
            var outputVertices = new List<Vertex>();
            for (int i = 0; i < skeleton.GetLength(0); i++)
            {
                for (int j = 0; j < skeleton.GetLength(1); j++)
                {
                    if (skeleton[i, j])
                    {
                        outputVertices.Add(new Vertex(new Point(i, j)));
                    }
                }
            }
            
            foreach (var p in outputVertices)
            {
                p.Neighbours = outputVertices.FindAll(q => Vertex.AreNeighbours(p, q));
                p.Rutovitz = skeleton.GetRutovitz(p.Pos);
            }
            
            var contour = new List<Vertex>();
            foreach (var p in outputVertices)
            {
                if (p.Rutovitz == 1)
                {
                    contour.Add(p);
                }
            }
            double widthOfPen = (double)outputVertices.Count / contour.Count * 2.0;
            signature.SetFeature<List<Vertex>>(OutputContour, contour);
            signature.SetFeature<double>(OutputWidthOfPen, widthOfPen);
            this.LogInformation("Width of pen: {0}", widthOfPen);
            this.LogInformation("PreVertexExtract - transform finished.");
        }
    }
}
