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
        public FeatureDescriptor<VertexCollection> OutputVertices { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("VertexExtract - transform started.");
            var skeleton = signature.GetFeature<bool[,]>(InputSkeleton);
            var outputVertices = new VertexCollection();
            for (int i = 0; i < skeleton.GetLength(0); i++)
            {
                for (int j = 0; j < skeleton.GetLength(1); j++)
                {
                    if (skeleton[i, j])
                    {
                        int iD = outputVertices.Count;
                        outputVertices.Add(new Vertex(iD, new Point(i, j)));
                    }
                }
            }



            foreach (var p in outputVertices.Values)
            {
                p.Neighbours = outputVertices.Values.Where(q => IsNeighbour(p, q)).ToList();
                //p.Neighbours = new List<Vertex>();
                //foreach (var q in outputVertices.Values)
                //{
                //    if (isNeighbour(p, q))
                //    {
                //        p.Neighbours.Add(q);
                //    }
                //}
            }
            signature.SetFeature<VertexCollection>(OutputVertices, outputVertices);
            this.LogInformation("VertexExtract transform finished - " + outputVertices.Count.ToString() + " vertices extracted.");

        }

        bool IsNeighbour(Vertex p, Vertex q)
        {
            return (Math.Abs(p.Pos.X - q.Pos.X)) <= 1 && (Math.Abs(p.Pos.Y - q.Pos.Y) <= 1) && p.Pos != q.Pos;
        }
    }
}
