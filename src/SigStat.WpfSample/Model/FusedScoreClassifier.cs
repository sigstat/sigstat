using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.WpfSample.Common;

namespace SigStat.WpfSample.Model
{
    public class FusedScoreClassifier : BaseClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        private double threshold;
        private List<Signature> originals;

        public FusedScoreClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public override double Train(List<Signature> signatures)
        {
            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'sigantures' can not be empty", nameof(signatures));

            originals = signatures;
            List<double> costs = new List<double>(originals.Count);
            double avg = 0;
            double n = 0;

            List<object[]> debugInfo = new List<object[]>();
            //new object[originals.Count + 1, originals.Count + 1];

            var debugRow = new object[signatures.Count + 1];
            for (int i = 0; i < originals.Count; i++)
            {
                debugRow[i + 1] = signatures[i].ID;
            }
            debugInfo.Add(debugRow);

            for (int i = 0; i < originals.Count - 1; i++)
            {
                debugRow = new object[signatures.Count + 1];
                debugRow[0] = signatures[i].ID;
                for (int j = i + 1; j < originals.Count; j++)
                {
                    double cost = FusedScore.CalculateFusionOfDtwAndWPathScore(originals[i], new Signature[] { originals[j] }, InputFeatures);
                    debugRow[j + 1] = cost;
                    avg += cost;
                    costs.Add(cost);

                    n++;
                }
                debugInfo.Add(debugRow);
            }

            avg /= n;

            double dev = Measures.StandardDeviation(costs.ToArray(), false);
            threshold = avg + 0.8 * dev; //TODO: rendesen beállítani, valami adaptívabbat kitaláltni 

            Logger.Info(this, signatures[0].Signer.ID + "-fusclassifier-distances", debugInfo);

            return threshold;
        }

        public override double Test(Signature signature)
        {
            var debugInfo = (List<object[]>)Logger.ObjectEntries[signature.Signer.ID + "-fusclassifier-distances"];
            var debugRow = new object[originals.Count + 1];
            debugRow[0] = signature.ID;

            double avg = 0;

            int count = originals.Count;
            for (int j = 0; j < originals.Count; j++)
            {
                if (signature == originals[j])
                    count--;
                else
                {
                    var dist = FusedScore.CalculateFusionOfDtwAndWPathScore(signature, new Signature[] { originals[j] }, InputFeatures);
                    avg += dist;
                    debugRow[j + 1] = dist;
                }
            }
            debugInfo.Add(debugRow);
            return CalculateTestResult(avg / count,threshold);
        }
    }
}
