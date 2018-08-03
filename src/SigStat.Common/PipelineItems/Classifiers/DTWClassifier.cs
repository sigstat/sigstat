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
        private readonly List<FeatureDescriptor> fs = new List<FeatureDescriptor>();
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

        public double Pair(Signature signature1, Signature signature2)
        {
            double[][] testsig1 = signature1.GetAggregateFeature(fs).ToArray();
            double[][] testsig2 = signature2.GetAggregateFeature(fs).ToArray();
            return dtwalg.Compute(testsig1, testsig2).cost;

        }

        /*public void Train(IEnumerable<Signature> signatures)
        {
            training.Clear();
            training.AddRange(signatures);
            //TODO: calculate limit, etc. here
        }*/

    }
}
