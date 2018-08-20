using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    /// <summary>
    /// Classifies Signatures with the <see cref="Dtw"/> algorithm.
    /// </summary>
    public class DTWClassifier : PipelineBase, IClassification, IEnumerable
    {
        private readonly Func<double[], double[], double> distanceMethod;

        /// <summary>Initializes a new instance of the <see cref="DTWClassifier"/> class with the default Manhattan distance method.</summary>
        public DTWClassifier() : this(Accord.Math.Distance.Manhattan)
        {
        }
        ///<summary>Initializes a new instance of the <see cref="DTWClassifier"/> class with a specified distance method.</summary>
        /// <param name="distanceMethod">Accord.Math.Distance.*</param>
        public DTWClassifier(Func<double[], double[], double> distanceMethod)
        {
            this.distanceMethod = distanceMethod;
            InputFeatures = new List<FeatureDescriptor>();
        }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return InputFeatures.GetEnumerator();
        }

        /// <inheritdoc/>
        public void Add(FeatureDescriptor f)
        {
            InputFeatures.Add(f);
        }

        /// <summary>
        /// Aggregates the input features and executes the <see cref="Dtw"/> algorithm.
        /// </summary>
        /// <param name="signature1"></param>
        /// <param name="signature2"></param>
        /// <returns>Cost between <paramref name="signature1"/> and <paramref name="signature2"/></returns>
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
