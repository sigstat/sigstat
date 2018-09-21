using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.Transforms;
using SigStat.WpfSample.Common;
using SigStat.WpfSample.Helpers;
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
        private List<ITransformation> pipeline = new List<ITransformation>() ;

        public MyVerifier(IClassifier classifier)
        {
            Classifier = classifier;

            foreach (var fd in DerivableSvc2004Features.All)
            {
                pipeline.Add(new Normalize().Input(fd).Output(fd));
            }
        }

        //Normalize nem rendeltetésszerűen működik, ha többször normalizálok abból lehet probléma?
        public override void Train(List<Signature> signatures)
        {
            foreach (var sig in signatures)
            {
                foreach (var item in pipeline)
                {
                    item.Transform(sig);
                }

                //ClassifierHelper.Normalize(sig, new List<FeatureDescriptor>(DerivableSvc2004Features.All));
                new FeatureExtractor(sig).GetAllDerivedSVC2004Features();
            }
            Classifier.Train(signatures);
        }

        public override bool Test(Signature sig)
        {
            foreach (var item in pipeline)
            {
                item.Transform(sig);
            }
            //ClassifierHelper.Normalize(signature, new List<FeatureDescriptor>(DerivableSvc2004Features.All));
            new FeatureExtractor(sig).GetAllDerivedSVC2004Features();

            return Classifier.Test(sig);
        }

    }
}
