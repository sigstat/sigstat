using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.WpfSample.Common;

namespace SigStat.WpfSample.Model
{
    public class FusedScoreClassifier : IClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        private double threshold;
        private List<Signature> originals;

        public FusedScoreClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            originals = signatures;
            double avg = 0;
            double n = 0;
            for (int i = 0; i < originals.Count - 1; i++)
            {
                for (int j = 1; j < originals.Count; j++)
                {
                    avg += FusedScore.CalculateFusionOfDtwAndWPathScore(originals[i], new Signature[] { originals[j] }, InputFeatures);
                    n++;
                }
            }

            //avg /= (originals.Count * (originals.Count - 1) / 2);
            avg /= n;
            threshold = avg;

            return avg;
        }

        public bool Test(Signature signature)
        {
            return FusedScore.CalculateFusionOfDtwAndWPathScore(signature, originals.ToArray(), InputFeatures) <= threshold;
        }
    }
}
