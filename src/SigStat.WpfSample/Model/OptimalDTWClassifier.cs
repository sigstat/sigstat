using NDtw;
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
    public class OptimalDTWClassifier : IClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }
        public DtwType DtwType { get; set; } = DtwType.MyDtw;
        public List<SimilarityResult> SimilarityResults { get; private set; }
        

        private List<Signature> referenceSignatures;
        private List<Signature> trainSignatures;
        private double threshold;

        object[,] debugInfo;


        public Logger Logger { get; set; }

        public OptimalDTWClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public OptimalDTWClassifier(List<FeatureDescriptor> inputFeatures, DtwType dtwType) : this(inputFeatures)
        {
            DtwType = dtwType;
        }

        public double Train(List<Signature> signatures)
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

            Logger.Info(this, signatures[0].Signer.ID + "_optidtw", debugInfo);
            return threshold;
        }

        public bool Test(Signature signature)
        {
            return GetAvgDistFromReferences(signature) <= threshold;
        }

        private void CalculateSimilarity()
        {
            SimilarityResults = new List<SimilarityResult>(trainSignatures.Count);
            foreach (var testSig in trainSignatures)
            {
                SimilarityResults.Add(new SimilarityResult(testSig, GetAvgDistFromReferences(testSig)));
            }
        }

        private double GetAvgDistFromReferences(Signature sig)
        {
            double avgDist = 0;
            int count = referenceSignatures.Count;
            foreach (var refSig in referenceSignatures)
            {
                if (sig == refSig)
                    count--;
                else
                {
                    //var dist = new Dtw(sig, refSig, InputFeatures).CalculateDtwScore();
                    var dist = GetCost(sig, refSig, DtwType);
                    avgDist += dist;
                    debugInfo[trainSignatures.IndexOf(sig) + 1, referenceSignatures.IndexOf(refSig) + 1] = dist;
                }
            }
            avgDist /= count;

            return avgDist;
        }

        private double GetCost(Signature sig1, Signature sig2, DtwType dtwType)
        {
            switch (dtwType)
            {
                case DtwType.NDtw:
                    //hatalmas hack, csak egy-egy featurere működik, kombinációra nem
                    return new NDtw.Dtw(sig1.GetFeature<List<double>>(InputFeatures[0]).ToArray(), sig2.GetFeature<List<double>>(InputFeatures[0]).ToArray(),
                        DistanceMeasure.Manhattan).GetCost();
                case DtwType.FrameworkDtw:
                    return new SigStat.Common.Algorithms.Dtw(Accord.Math.Distance.Manhattan)
                        .Compute(sig1.GetAggregateFeature(InputFeatures).ToArray(), sig2.GetAggregateFeature(InputFeatures).ToArray());
                case DtwType.MyDtw:
                    return new Common.Dtw(sig1, sig2, InputFeatures).CalculateDtwScore();
                default:
                    throw new ArgumentException(nameof(dtwType) + typeof(DtwType) + "not exists");
            }
        }
    }
}
