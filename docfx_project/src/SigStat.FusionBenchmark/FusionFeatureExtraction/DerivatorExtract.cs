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
    class DerivatorExtract : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputSequence { get; set; }

        [Output]
        public FeatureDescriptor<List<double>> OutputDiffSequence { get; set; }

        public void Transform(Signature signature)
        {
            var seq = signature.GetFeature<List<double>>(InputSequence);
            var outputSeq = seq.Differentiate();
            signature.SetFeature<List<double>>(OutputDiffSequence, outputSeq);
        }
    }
}
