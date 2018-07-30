using SigStat.Common.Algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class DTWClassifier : IClassification, IEnumerable
    {
        private readonly List<FeatureDescriptor> fs = new List<FeatureDescriptor>();
        private List<Signature> training = new List<Signature>();
        DTW dtwalg;

        public DTWClassifier()
        {
            dtwalg = new DTW(Accord.Math.Distance.Manhattan);
        }

        public DTWClassifier(Func<double[], double[], double> DistanceMethod)
        {
            dtwalg = new DTW(DistanceMethod);
        }

        public IEnumerator GetEnumerator()
        {
            return fs.GetEnumerator();
        }

        public void Add(FeatureDescriptor f)
        {
            fs.Add(f);
        }

        public double Test(Signature signature)
        {
            double[][] testsig = signature.GetAggregateFeature(fs).ToArray();
            double[] results = new double[training.Count];
            for (int i = 0; i < training.Count; i++)
                results[i] = dtwalg.Compute(testsig, training[i].GetAggregateFeature(fs).ToArray()).cost;
            double atlag = 0;
            foreach (double d in results)
                atlag += d;
            atlag /= results.Length;
            return atlag;
        }

        public void Train(IEnumerable<Signature> signatures)
        {
            training.Clear();
            training.AddRange(signatures);
            //TODO: calculate limit, etc. here
        }

    }
}
