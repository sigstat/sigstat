using SigStat.Common.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class DTWClassifier : IClassification
    {
        private readonly FeatureDescriptor f;
        private List<Signature> training = new List<Signature>();
        DTW dtwalg;

        public DTWClassifier(FeatureDescriptor f)
        {
            this.f = f;
            dtwalg = new DTW(Accord.Math.Distance.Manhattan);
        }

        public DTWClassifier(FeatureDescriptor f, Func<double[], double[], double> DistanceMethod)
        {
            this.f = f;
            dtwalg = new DTW(DistanceMethod);
        }

        public double Test(Signature signature)
        {
            double[][] testsig = prepareData(signature);
            double[] results = new double[training.Count];
            for (int i = 0; i < training.Count; i++)
                results[i] = dtwalg.Compute(testsig, prepareData(training[i])).cost;
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

        private double[][] prepareData(Signature signature)
        {
            double[][] values = null;
            if (f.FeatureType == typeof(List<double>))
            {//1d ertekek, pl tangens
                var v = signature.GetFeature<List<double>>(f.Key);
                double[] tmp = v.ToArray();
                values = new double[tmp.Length][];
                for (int i = 0; i < tmp.Length; i++)
                    values[i] = new double[] { tmp[i] };
            }
            else if (f.FeatureType == typeof(List<double[]>))
            {
                values = signature.GetFeature<List<double[]>>(f.Key).ToArray();
            }
            //else TODO PointF
            return values;
        }
    }
}
