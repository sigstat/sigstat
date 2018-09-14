using SigStat.Common;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public class Verifier 
    {
        public IClassifier Classifier { get; set; }

        public Verifier(IClassifier classifier)
        {
            Classifier = classifier;
        }

        public double Train(Signer signer)
        {
            return Train(signer.Signatures.FindAll(s => s.Origin == Origin.Genuine));
        }

        public double Train(List<Signature> signatures)
        {
            return Classifier.Train(signatures);
        }

        public bool Test(Signature signature)
        {
            return Classifier.Test(signature);
        }

    }
}
