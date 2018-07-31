using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Model
{
    public class Verifier
    {
        public ITransformation TransformPipeline { get; set; }
        public IClassification ClassifierPipeline { get; set; }
        double limit;
        List<Signature> genuines;

        public Verifier()
        {
            //TransformPipeline = new Pipeline.SequentialTransformPipeline();
        }

        public void Train(Signer signer)
        {
            Train(signer.Signatures);
        }

        public void Train(List<Signature> sigs)
        {
            //pl. constraint hogy csak Genuine-okkal tudjunk trainelni (?)

            sigs.ForEach((sig) => {
                TransformPipeline.Transform(sig);
            });

            //optimize limit with genuine sigs
            genuines = sigs.FindAll((s) => s.Origin == Origin.Genuine);
            limit = new ApproximateLimit(ClassifierPipeline).Calculate(genuines);

            //TODO: egyeb optimizalasi lehetosegek
            
        }

        public bool Test(Signature sig)
        {
            TransformPipeline.Transform(sig);

            double[] vals = new double[genuines.Count];
            for (int i = 0; i < genuines.Count; i++)
            {
                vals[i] = ClassifierPipeline.Pair(sig, genuines[i]);
                //progress++
            }
            double avg = vals.Average();
            return avg < limit;
        }

        public static Verifier BasicVerifier
        {
            get
            {
                return new Verifier()
                {
                    TransformPipeline = new TangentExtraction(),
                    ClassifierPipeline = new DTWClassifier() { FeatureDescriptor<List<double>>.Descriptor("Tangent") }
                };
            }
        }
    }
}
