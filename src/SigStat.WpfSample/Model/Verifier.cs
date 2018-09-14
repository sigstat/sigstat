using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.Transforms;
using SigStat.WpfSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public class MyVerifier: Verifier
    {
        public IClassifier Classifier { get; set; }

        public MyVerifier(IClassifier classifier)
        {
            Classifier = classifier;
        }


        public override void Train(List<Signature> signatures)
        {
            foreach (var sig in signatures)
            {
                new Normalize().InputFeatures = new List<FeatureDescriptor>(DerivableSvc2004Features.All);
                new FeatureExtractor(sig).GetAllDerivedSVC2004Features();
            }
            Classifier.Train(signatures);
        }

        public override bool Test(Signature signature)
        {
            new Normalize().InputFeatures = new List<FeatureDescriptor>(DerivableSvc2004Features.All);
            new FeatureExtractor(signature).GetAllDerivedSVC2004Features();

            return Classifier.Test(signature);
        }

    }
}
