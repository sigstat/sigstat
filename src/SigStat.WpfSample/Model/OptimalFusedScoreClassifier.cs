using SigStat.Common;
using SigStat.Common.Helpers;
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

        public List<SimilarityResult> SimilarityResults { get; private set; }
        private List<Signature> referenceSignatures;
        private List<Signature> trainSignatures;
        private double threshold;
        public Logger Logger { get; set; }

        public OptimalFusedScoreClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            referenceSignatures = signatures.FindAll(s => s.Origin == Origin.Genuine).Take(10).ToList();
            trainSignatures = signatures.FindAll(s => s.Origin == Origin.Genuine);
            trainSignatures.AddRange(signatures.FindAll(s => s.Origin == Origin.Forged).Take(10).ToList());

            CalculateSimilarity();

            threshold = new OptimalClassifierHelper(SimilarityResults).CalculateThresholdForOptimalClassification();
            return threshold;
        }

        public bool Test(Signature signature)
        {
            return FusedScore.CalculateFusionOfDtwAndWPathScore(signature, referenceSignatures.ToArray(), InputFeatures) <= threshold;
        }

        private void CalculateSimilarity()
        {
            SimilarityResults = new List<SimilarityResult>(trainSignatures.Count);
            for (int i = 0; i < trainSignatures.Count; i++)
            {
                var trainSig = trainSignatures[i];
                var dist = FusedScore.CalculateFusionOfDtwAndWPathScore(trainSig, referenceSignatures.ToArray(), InputFeatures);
                SimilarityResults.Add(new SimilarityResult(trainSig, dist));
            }

        }

    }
}
