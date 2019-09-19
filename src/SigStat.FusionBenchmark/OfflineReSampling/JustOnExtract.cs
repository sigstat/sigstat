using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.OfflineReSampling
{
    class JustOnExtract: PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> Input { get; set; }

        [Output("JustOn")]
        public FeatureDescriptor<List<double>> Output { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("JustOnExtract - transform started");
            var bs = signature.GetFeature<List<bool>>(InputButton);
            var seq = signature.GetFeature<List<double>>(Input);

            if (seq.Count != bs.Count)
            {
                throw new ArgumentException();
            }

            var res = new List<double>();
            for (int i = 0; i < bs.Count; i++)
            {
                if (bs[i])
                {
                    res.Add(seq[i]);
                }
            }
            if (res.Count == 0)
            {
                throw new Exception();
            }
            signature.SetFeature<List<double>>(Output, res);
            this.LogInformation("seq: {0} - res: {1} ", bs.Count, res.Count);
            this.LogInformation("JustOnExtract - transform finished");

        }

    }
}
