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

namespace SigStat.FusionBenchmark.GraphExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class PointClassification : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<VertexCollection> InputVertices { get; set; }

        [Output]
        public FeatureDescriptor<VertexCollection> OutputEndPoints { get; set; }

        [Output]
        public FeatureDescriptor<VertexCollection> OutputCrossingPoints { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("PointClassification - GraphBased transform started.");
            var vertices = signature.GetFeature<VertexCollection>(InputVertices);
            var endPoints = new VertexCollection();
            var crossingPoints = new VertexCollection();
            foreach (var p in vertices.Values)
            {
                if (p.Degree() == 1)
                    endPoints.Add(p);
                if (p.Degree() > 2)
                    crossingPoints.Add(p);
            }
            signature.SetFeature<VertexCollection>(OutputEndPoints, endPoints);
            signature.SetFeature<VertexCollection>(OutputCrossingPoints, crossingPoints);
        }
    }
}
