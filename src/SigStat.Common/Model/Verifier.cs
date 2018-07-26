using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model
{
    public class Verifier
    {
        public ITransformation TransformPipeline { get; set; }
        public IClassification ClassifierPipeline { get; set; }

        public Verifier()
        {
            TransformPipeline = new Pipeline.SequentialTransformPipeline();
        }

        public void Train(Signer signer)
        {
            Train(signer.Signatures);
        }

        public void Train(List<Signature> sigs)
        {
            sigs.ForEach((sig) => {
                TransformPipeline.Transform(sig);
            });
            ClassifierPipeline.Train(sigs);
        }

        public bool Test(Signature sig)
        {
            TransformPipeline.Transform(sig);
                return ClassifierPipeline.Test(sig);
        }

    }
}
