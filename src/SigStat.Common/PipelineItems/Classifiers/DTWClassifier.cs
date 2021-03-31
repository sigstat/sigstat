﻿using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;
using SigStat.Common.Logging;

namespace SigStat.Common.PipelineItems.Classifiers
{

    /// <summary>
    /// Represents a trained model for <see cref="DtwClassifier"/>
    /// </summary>
    public class DtwSignerModel : ISignerModel
    {

        /// <inheritdoc/>
        public string SignerID { get; set; }

        /// <summary>
        /// A list a of genuine signatures used for training
        /// </summary>
        public List<KeyValuePair<string, double[][]>> GenuineSignatures { get; set; }
        /// <summary>
        /// A threshold, that will be used for classification. Signatures with
        /// an average DTW distance from the genuines above this threshold will
        /// be classified as forgeries
        /// </summary>
        public double Threshold { get; set; }

        /// <summary>
        /// DTW distance matrix of the genuine signatures
        /// </summary>
        public DistanceMatrix<string, string, double> DistanceMatrix  { get; set; }


    }
    /// <summary>
    /// Classifies Signatures with the DTW algorithm.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class DtwClassifier : PipelineBase, IDistanceClassifier
    {
        /// <summary>
        /// The function used to calculate the distance between two data points during DTW calculation
        /// </summary>
        [JsonConverter(typeof(DistanceFunctionJsonConverter))]
        public Func<double[], double[], double> DistanceFunction { get; set; }

        /// <summary>
        /// Gets or sets the features to consider during distance calculation
        /// </summary>
        [Input]
        public List<FeatureDescriptor> Features { get; set; }

        /// <summary>
        /// Gets or sets the multiplication factor to be used during threshold calculation
        /// </summary>
        public double MultiplicationFactor { get; set; } = 1;

        /// <summary>Initializes a new instance of the <see cref="DtwClassifier"/> class with the default Manhattan distance method.</summary>
        public DtwClassifier() : this(Accord.Math.Distance.Manhattan)
        {
        }
        ///<summary>Initializes a new instance of the <see cref="DtwClassifier"/> class with a specified distance method.</summary>
        /// <param name="distanceMethod">Accord.Math.Distance.*</param>
        public DtwClassifier(Func<double[], double[], double> distanceMethod)
        {
            this.DistanceFunction = distanceMethod;
            Features = new List<FeatureDescriptor>();//
        }

        /// <inheridoc/>
        public ISignerModel Train(List<Signature> signatures)
        {
            if (signatures == null || signatures.Count == 0)
                throw new ArgumentException("Argument 'signatures' can not be null or an empty list", nameof(signatures));
            var signerID = signatures[0].Signer?.ID;
            var genuines = signatures.Where(s => s.Origin == Origin.Genuine)
                .Select(s => new { s.ID, Features = s.GetAggregateFeature(Features).ToArray() }).ToList();
            var distanceMatrix = new DistanceMatrix<string, string, double>();
            foreach (var i in genuines)
            {
                foreach (var j in genuines)
                {
                    if (distanceMatrix.ContainsKey(j.ID, i.ID))
                    {
                        distanceMatrix[i.ID, j.ID] = distanceMatrix[j.ID, i.ID];
                    }
                    else if (i == j)
                    {
                        distanceMatrix[i.ID, j.ID] = 0;
                    }
                    else
                    {
                        var distance = DtwImplementations.ExactDtwWikipedia(i.Features, j.Features, DistanceFunction);
                        distanceMatrix[i.ID, j.ID] = distance;
                        this.LogTrace(new ClassifierDistanceLogState(signerID, signerID, i.ID, j.ID, distance));

                    }

                }
            }

            var distances = distanceMatrix.GetValues().Where(v => v != 0);
            var mean = distances.Average();
            var stdev = Math.Sqrt(distances.Select(d => (d - mean) * (d - mean)).Sum() / (distances.Count()-1));
           
            
            return new DtwSignerModel
            {
                SignerID = signerID,
                GenuineSignatures = genuines.Select(g => new KeyValuePair<string, double[][]>(g.ID, g.Features)).ToList(),
                DistanceMatrix = distanceMatrix,
                Threshold = mean + MultiplicationFactor * stdev
            };
        }

        /// <inheridoc/>
        public double Test(ISignerModel model, Signature signature)
        {
            var dtwModel = (DtwSignerModel)model;
            var testSignature = signature.GetAggregateFeature(Features).ToArray();
            var distances = new double[dtwModel.GenuineSignatures.Count];

            for (int i = 0; i < dtwModel.GenuineSignatures.Count; i++)
            {
                distances[i] = DtwImplementations.ExactDtwWikipedia(dtwModel.GenuineSignatures[i].Value, testSignature, DistanceFunction);
                dtwModel.DistanceMatrix[signature.ID, dtwModel.GenuineSignatures[i].Key] = distances[i];
                this.LogTrace(new ClassifierDistanceLogState(model.SignerID, signature?.Signer.ID, dtwModel.GenuineSignatures[i].Key, signature.ID, distances[i]));
            }

            // returns value between 0 and 1, how confident is the decision about genuineness
            // 1 -> conident genuine, 0 -> not confident genuine = confident forged
            return Math.Max(1 - (distances.Average() / dtwModel.Threshold) / 2, 0);
            
        }
    }
}
