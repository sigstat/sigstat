using Accord.Statistics;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.WpfSample.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public class DTWClassifier : IClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        private double threshold;
        private List<Signature> originals;
        public Logger Logger { get; set; }
        public DTWClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'sigantures' can not be empty", nameof(signatures));

            originals = signatures;
            List<double> costs = new List<double>(originals.Count);
            double avg = 0;
            int n = 0;

            object[,] debugInfo = new object[originals.Count+1, originals.Count+1];

            for (int i = 0; i < originals.Count; i++)
            {
                debugInfo[i+1, 0] = signatures[i].ID;
                debugInfo[0, i+1] = signatures[i].ID;
            }

            for (int i = 0; i < originals.Count - 1; i++)
            {
                for (int j = i + 1; j < originals.Count; j++)
                {
                    double cost = new Dtw(originals[i], originals[j], InputFeatures).CalculateDtwScore();
                    debugInfo[i+1, j+1] = cost;
                    avg += cost;
                    costs.Add(cost);

                    n++;
                }
            }

            avg /= n;
            double dev = Measures.StandardDeviation(costs.ToArray(), false);
            threshold = avg + 0.8 * dev; //TODO: rendesen beállítani, valami adaptívabbat kitaláltni

            Logger.Info(this, signatures[0].Signer.ID + "-dtwclassifier-distances", debugInfo);

            return threshold;
        }

        public bool Test(Signature signature)
        {
            double avgDist = 0;
            foreach (var original in originals)
            {
                avgDist += new Dtw(original, signature, InputFeatures).CalculateDtwScore();
            }
            avgDist /= originals.Count;

            return avgDist <= threshold;
        }
    }
}
