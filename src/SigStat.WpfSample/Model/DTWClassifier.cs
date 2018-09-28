using Accord.Statistics;
using SigStat.Common;
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

        public DTWClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            originals = signatures;
            List<double> costs = new List<double>(originals.Count);
            double avg = 0;
            int n = 0;

            for (int i = 0; i < originals.Count - 1; i++)
            {
                for (int j = i + 1; j < originals.Count; j++)
                {
                    double cost = new Dtw(originals[i], originals[j], InputFeatures).CalculateDtwScore();
                    avg += cost;
                    costs.Add(cost);

                    n++;
                    //avg += new Dtw(originals[i], originals[j], InputFeatures).CalculateDtwScore();
                }
            }

            avg /= n;
            double dev = Measures.StandardDeviation(costs.ToArray(), false);
            threshold = avg + 0.8 * dev; //TODO: rendesen beállítani, valami adaptívabbat kitaláltni

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
