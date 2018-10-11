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
    public class OptimalFusedScoreClassifier : BaseClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        public List<SimilarityResult> SimilarityResults { get; private set; }
        private List<Signature> referenceSignatures;
        private List<Signature> trainSignatures;
        private double threshold;

        object[,] debugInfo;

        public OptimalFusedScoreClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public override double Train(List<Signature> signatures)
        {
            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'sigantures' can not be empty", nameof(signatures));

            referenceSignatures = signatures.FindAll(s => s.Origin == Origin.Genuine).Take(10).ToList();
            trainSignatures = signatures.FindAll(s => s.Origin == Origin.Genuine);
            trainSignatures.AddRange(signatures.FindAll(s => s.Origin == Origin.Forged).Take(10).ToList());

            debugInfo = new object[trainSignatures.Count + 1, referenceSignatures.Count + 1];
            for (int i = 0; i < trainSignatures.Count; i++)
            {
                debugInfo[i + 1, 0] = trainSignatures[i].ID;
            }
            for (int i = 0; i < referenceSignatures.Count; i++)
            {
                debugInfo[0, i + 1] = referenceSignatures[i].ID;
            }


            CalculateSimilarity();

            threshold = new OptimalClassifierHelper(SimilarityResults).CalculateThresholdForOptimalClassification();
            Logger.Info(this, signatures[0].Signer.ID + "_optifus", debugInfo);

            return threshold;
        }

        public override bool Test(Signature signature)
        {
            return FusedScore.CalculateFusionOfDtwAndWPathScore(signature, referenceSignatures.ToArray(), InputFeatures) <= threshold;
        }

        private void CalculateSimilarity()
        {
            SimilarityResults = new List<SimilarityResult>(trainSignatures.Count);
            for (int i = 0; i < trainSignatures.Count; i++)
            {
                var trainSig = trainSignatures[i];

                double avg = 0;
                int count = referenceSignatures.Count;
                for (int j = 0; j < referenceSignatures.Count; j++)
                {
                    if (trainSig == referenceSignatures[j])
                        count--;
                    else
                    {
                        var dist = FusedScore.CalculateFusionOfDtwAndWPathScore(trainSig, new Signature[] { referenceSignatures[j] }, InputFeatures);
                        avg += dist;
                        debugInfo[i + 1, j + 1] = dist;
                    }
                }

                double avgDist = avg / count;
                SimilarityResults.Add(new SimilarityResult(trainSig, avgDist));
            }

        }

    }
}
