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
using SigStat.FusionBenchmark.GraphExtraction;

namespace SigStat.FusionBenchmark.OfflineFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class CogExtraction : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Output("Cog")]
        public FeatureDescriptor<Point> OutputCog { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("CogExtraction - transform started");
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            var cog = new Point(0, 0);
            foreach (var p in vertices)
            {
                cog.X += p.Pos.X;
                cog.Y += p.Pos.Y;
            }
            cog.X /= vertices.Count;
            cog.Y /= vertices.Count;
            signature.SetFeature<Point>(OutputCog, cog);
            this.LogInformation("Cog: " + cog.X.ToString() + " " + cog.Y.ToString());
            this.LogInformation("CogExtraction - transform finished");

        }
    }
}
