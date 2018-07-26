using SigStat.Common.Algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class DTWClassifier : IClassification, IEnumerable
    {
        private readonly List<FeatureDescriptor> fs;
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

            int len = signature.GetFeature<List<double>>(fs[0].Key).Count;
            values = new double[len][];
            for(int i = 0; i < len; i++)
                values[i] = new double[fs.Count];//dim

            for (int iF = 0; iF < fs.Count; iF++)
            {
                var v = signature.GetFeature<List<double>>(fs[iF].Key);
                for (int i = 0; i < len; i++)
                {
                    values[i][iF] = v[i];
                }
            }

            //TODO: pl lehetne aggregalt featuret atadni: PointF az X és Y bol



            return values;
        }
    }
}
