using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SigStat.Common.PipelineItems.Classifiers
{
    /// <summary>
    /// Represents a trained model for <see cref="DtwClassifier"/>
    /// </summary>
    public class DtwSignerModel: ISignerModel
    {
        /// <summary>
        /// A list a of genuine signatures used for training
        /// </summary>
        public List<double[][]> GenuineSignatures { get; set; }
        /// <summary>
        /// A threshold, that will be used for classification. Signatures with
        /// an average DTW distance from the genuines above this threshold will
        /// be classified as forgeries
        /// </summary>
        public double Threshold;

        /// <summary>
        /// DTW distance matrix of the genuine signatures
        /// </summary>
        public double[,] DistanceMatrix;
    }
    /// <summary>
    /// Classifies Signatures with the <see cref="Dtw"/> algorithm.
    /// </summary>
    public class DtwClassifier : PipelineBase, IClassifier
    {
        private readonly Func<double[], double[], double> distanceMethod;

        [Input]
        public List<FeatureDescriptor> InputFeatures { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DtwClassifier"/> class with the default Manhattan distance method.</summary>
        public DtwClassifier() : this(Accord.Math.Distance.Manhattan)
        {
        }
        ///<summary>Initializes a new instance of the <see cref="DtwClassifier"/> class with a specified distance method.</summary>
        /// <param name="distanceMethod">Accord.Math.Distance.*</param>
        public DtwClassifier(Func<double[], double[], double> distanceMethod)
        {
            this.distanceMethod = distanceMethod;
            InputFeatures = new List<FeatureDescriptor>();//
        }

        /// <inheridoc/>
        public ISignerModel Train(List<Signature> signatures)
        {
            var genuines = signatures.Where(s => s.Origin == Origin.Genuine)
                .Select(s => s.GetAggregateFeature(InputFeatures).ToArray()).ToList();
            Dtw dtwAlg = new Dtw(distanceMethod);
            var distanceMatrix = new double[genuines.Count, genuines.Count];
            distanceMatrix.SetValues(-1);
            for (int i = 0; i < genuines.Count; i++)
            {
                for (int j = 0; j < genuines.Count; j++)
                {
                    if (distanceMatrix[j, i] != -1)
                    {
                        distanceMatrix[i, j] = distanceMatrix[j, i];
                    }
                    else if (i == j)
                    {
                        distanceMatrix[i, j] = 0;
                    }
                    else
                    {
                        distanceMatrix[i, j] = dtwAlg.Compute(genuines[i], genuines[j]); ;
                    }
                }
            }

            var distances = distanceMatrix.GetValues().Where(v => v != 0);
            var mean = distances.Average();
            var stdev = Math.Sqrt(distances.Select(d => (d - mean) * (d - mean)).Sum() / distances.Count());
            return new DtwSignerModel
            {
                GenuineSignatures = genuines,
                DistanceMatrix = distanceMatrix,
                Threshold = mean + stdev
            };
        }

        /// <inheridoc/>
        public double Test(ISignerModel model, Signature signature)
        {
            var dtwModel = (DtwSignerModel)model;
            var testSignature = signature.GetAggregateFeature(InputFeatures).ToArray();
            var distances = new double[dtwModel.GenuineSignatures.Count];
            Dtw dtwAlg = new Dtw(distanceMethod);

            for (int i = 0; i < dtwModel.GenuineSignatures.Count; i++)
            {
                distances[i] = dtwAlg.Compute(dtwModel.GenuineSignatures[i], testSignature);
            }

            // TODO: return values between 0 and 1
            if (distances.Average() > dtwModel.Threshold)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
