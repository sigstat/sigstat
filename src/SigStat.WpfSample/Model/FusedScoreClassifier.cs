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
    public class FusedScoreClassifier : IClassifier
    {
        public List<FeatureDescriptor> InputFeatures { get; set; }

        private double threshold;
        private List<Signature> originals;
        public Logger Logger { get; set; }
        public FusedScoreClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public double Train(List<Signature> signatures)
        {
            originals = signatures;
            List<double> costs = new List<double>(originals.Count);
            double avg = 0;
            double n = 0;
            for (int i = 0; i < originals.Count - 1; i++)
            {
                for (int j = i + 1; j < originals.Count; j++)
                {
                    double cost = FusedScore.CalculateFusionOfDtwAndWPathScore(originals[i], new Signature[] { originals[j] }, InputFeatures);
                    avg += cost;
                    costs.Add(cost);

                    //avg += FusedScore.CalculateFusionOfDtwAndWPathScore(originals[i], new Signature[] { originals[j] }, InputFeatures);

                    n++;
                    
                }
            }

            avg /= n;

            double dev = Measures.StandardDeviation(costs.ToArray(), false);
            threshold = avg + 0.3 * dev; //TODO: rendesen beállítani, valami adaptívabbat kitaláltni 

            return threshold;
        }

        public bool Test(Signature signature)
        {
            return FusedScore.CalculateFusionOfDtwAndWPathScore(signature, originals.ToArray(), InputFeatures) <= threshold;
        }
    }
}
