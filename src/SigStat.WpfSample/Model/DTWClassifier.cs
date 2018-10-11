using Accord.Statistics;
using NDtw;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.WpfSample.Common;
using SigStat.WpfSample.Helpers;
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
        public DtwType DtwType { get; set; }

        private double threshold;
        private List<Signature> originals;
        public Logger Logger { get; set; }
        public DTWClassifier(List<FeatureDescriptor> inputFeatures)
        {
            InputFeatures = inputFeatures;
        }

        public DTWClassifier(List<FeatureDescriptor> inputFeatures, DtwType dtwType) : this(inputFeatures)
        {
            DtwType = dtwType;
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

            List<object[]> debugInfo = new List<object[]>();
            //object[,] debugInfo = new object[originals.Count+1, originals.Count+1];

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
                    //double cost = new Dtw(originals[i], originals[j], InputFeatures).CalculateDtwScore();
                    double cost = GetCost(originals[i], originals[j], DtwType);
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

            Logger.Info(this, signatures[0].Signer.ID + "-dtwclassifier-distances", debugInfo);

            return threshold;
        }

        public bool Test(Signature signature)
        {
            var debugInfo = (List<object[]>)Logger.ObjectEntries[signature.Signer.ID + "-dtwclassifier-distances"];
            var debugRow = new object[originals.Count + 1];
            debugRow[0] = signature.ID;

            double avgDist = 0;
            foreach (var original in originals)
            {
                //var dist = new Dtw(original, signature, InputFeatures).CalculateDtwScore();
                var dist = GetCost(original, signature, DtwType);
                avgDist += dist;
                debugRow[originals.IndexOf(original) + 1] = dist;
            }
            debugInfo.Add(debugRow);

            avgDist /= originals.Count;

            return avgDist <= threshold;
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
                    throw new ArgumentException(nameof(dtwType) + typeof(DtwType)+ "not exists");
            }
        }
    }
}
