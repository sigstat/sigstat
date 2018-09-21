using SigStat.Common;
using SigStat.WpfSample.Common;
using SigStat.WpfSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public class OptimalDTWClassifier : IClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        private List<Signature> originals;
        private double threshold;

        public OptimalDTWClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            originals = signatures.FindAll(s => s.Origin == Origin.Genuine);

            threshold = new OptimalClassifierHelper(ClassifierType.DTW, signatures, InputFeatures).CalculateThresholdForOptimalClassification();
            return threshold;
        }

        public bool Test(Signature signature)
        {
            double avgDist = 0;
            double count = originals.Count;
            foreach (var original in originals)
            {
                if (signature == original)
                    count--;
                else
                    avgDist += new Dtw(original, signature, InputFeatures).CalculateDtwScore();
            }
            avgDist /= count;

            return avgDist <= threshold;
        }


    }
}
