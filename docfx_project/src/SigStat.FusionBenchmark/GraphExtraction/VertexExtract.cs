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
using System.Linq;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class VertexExtract: PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<bool[,]> InputSkeleton { get; set; }

        [Output("Vertices")]
        public FeatureDescriptor<List<Vertex>> OutputVertices { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("VertexExtract - transform started.");
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

            ///A StrokeExtract lépés miatt, stroke kinyeréséhez trükk
            var strokeEnds = outputVertices.StrokeEnds();
            foreach (var p in strokeEnds)
            {
                HashSet<Vertex> neighbours = new HashSet<Vertex>(p.Neighbours);
                foreach (var q in p.Neighbours)
                {
                    q.Neighbours.RemoveAll(neigbour => neighbours.Contains(neigbour));
                }
            }
            signature.SetFeature<List<Vertex>>(OutputVertices, outputVertices);
            this.LogInformation("VertexExtract transform finished - " + outputVertices.Count.ToString() + " vertices extracted.");

        }

    }
}
