using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class DTWClassifier : PipelineBase, IClassification, IEnumerable
    {
        private readonly Func<double[], double[], double> distanceMethod;

        public DTWClassifier() : this(Accord.Math.Distance.Manhattan)
        {
        }
        public DTWClassifier(Func<double[], double[], double> DistanceMethod)
        {
            distanceMethod = DistanceMethod;
            InputFeatures = new List<FeatureDescriptor>();
        }

        public IEnumerator GetEnumerator()
        {
            return InputFeatures.GetEnumerator();
        }

        public void Add(FeatureDescriptor f)
        {
            InputFeatures.Add(f);
        }

        public double Pair(Signature signature1, Signature signature2)
        {
            Progress = 0;
            Dtw dtwAlg = new Dtw(distanceMethod);


            double[][] testSig1 = signature1.GetAggregateFeature(InputFeatures).ToArray();
            double[][] testSig2 = signature2.GetAggregateFeature(InputFeatures).ToArray();
            Progress = 50;//..
            double cost = dtwAlg.Compute(testSig1, testSig2);
            Log(LogLevel.Info, $"Paired SigID {signature1.ID} with SigID {signature2.ID}");
            Log(LogLevel.Debug, $"Pairing result of SigID {signature1.ID} with SigID {signature2.ID}: {cost}");
            Progress = 100;
            return cost;

        }

        /*public void Train(IEnumerable<Signature> signatures)
        {
            training.Clear();
            training.AddRange(signatures);
            //TODO: calculate limit, etc. here
        }*/

    }
}
