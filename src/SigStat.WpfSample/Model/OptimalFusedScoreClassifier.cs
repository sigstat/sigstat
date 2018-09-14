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
    public class OptimalFusedScoreClassifier : IClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        private List<Signature> originals;
        private double threshold;

        public OptimalFusedScoreClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            originals = signatures.FindAll(s => s.Origin == Origin.Genuine);

            threshold = new OptimalClassifierHelper(ClassifierType.FusedScore, signatures, InputFeatures).CalculateThresholdForOptimalClassification();
            return threshold;
        }

        public bool Test(Signature signature)
        {
            return FusedScore.CalculateFusionOfDtwAndWPathScore(signature, originals.ToArray(), InputFeatures) <= threshold;
        }
    }
}
