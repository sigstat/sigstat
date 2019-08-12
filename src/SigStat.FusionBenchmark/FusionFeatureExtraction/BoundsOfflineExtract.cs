using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class BoundsOfflineExtract : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputVertices { get; set; }

        [Output("Bounds")]
        public FeatureDescriptor<RectangleF> OutputBounds { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("BoundsOfflineExtract - transform started");
            var vertices = signature.GetFeature<List<Vertex>>(InputVertices);
            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minY = float.MaxValue;
            float maxY = float.MinValue;

            foreach (var p in vertices)
            {
                minX = Math.Min((float)p.Pos.X, minX);
                minY = Math.Min((float)p.Pos.Y, minY);
                maxX = Math.Max((float)p.Pos.X, maxX);
                maxY = Math.Max((float)p.Pos.Y, maxY);
            }
            float width = maxX - minX;
            float height = maxY - minY;
            var bounds = new RectangleF(minX, minY, width, height);
            signature.SetFeature<RectangleF>(OutputBounds, bounds);
            this.LogInformation("Width - Height: {0} - {1}", width, height);
            this.LogInformation("BoundsOfflineExtract - transform finished");
        }
    }
}
