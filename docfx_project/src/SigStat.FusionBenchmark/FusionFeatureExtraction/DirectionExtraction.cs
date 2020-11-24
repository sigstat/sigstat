using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class DirectionExtraction : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Output("Directions")]
        public FeatureDescriptor<List<double>> OutputDirections { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("DirectionExtraction - transform started");
            var xs = signature.GetFeature<List<double>>(InputX);
            var ys = signature.GetFeature<List<double>>(InputY);
            var directions = Translations.GetDirections(xs, ys);
            signature.SetFeature<List<double>>(OutputDirections, directions);
            this.LogInformation("DirectionExtraction - transform finished");
        }
    }
}
