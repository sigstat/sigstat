using Newtonsoft.Json;
using SigStat.Common.Algorithms;
using SigStat.Common.Algorithms.Classifiers;
using SigStat.Common.Algorithms.Distances;
using SigStat.Common.Helpers.Serialization;
using SigStat.Common.Logging;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    /// <summary>
    /// This <see cref="IDistanceClassifier"/> implementation will consider both test and 
    /// training samples and claculate the threshold to separate the original and forged
    /// signatures to approximate EER. Note that this classifier is not applicable for 
    /// real world scenarios. It was developed to test the theoratical boundaries of 
    /// threshold based classification
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.Pipeline.IDistanceClassifier" />
    [JsonObject(MemberSerialization.OptOut)]
    public class OneClassNearestNeighborClassifier : PipelineBase, IClassifier
    {
        #region SignerModel
        /// <summary>
        /// Represents a trained model for <see cref="OneClassNearestNeighborClassifier"/>
        /// </summary>
        public class SignerModel : ISignerModel
        {
            /// <inheritdoc/>
            public string SignerID { get; set; }

            /// <summary>
            /// Precalculated distances of known signatures
            /// </summary>
            public DistanceMatrix<string, string, double> DistanceCache { get; set; }

            /// <summary>
            /// A list a of genuine signatures used for training
            /// </summary>
            public List<KeyValuePair<string, double[][]>> TrainingSignatures { get; set; }
        }
        #endregion

        /// <summary>
        /// <see cref="FeatureDescriptor"/>s to consider during classification
        /// </summary>
        [Input]
        public List<FeatureDescriptor> Features { get; set; } = new List<FeatureDescriptor>();

        /// <summary>
        /// The function used to calculate the distance between two data sequences
        /// </summary>
        public IDistance<double[][]> DistanceFunction { get; set; }

        /// <summary>
        /// The J parameter of the <see cref="Ocjknn"/> classifier
        /// </summary>
        public int J { get; set; }
        /// <summary>
        /// The K parameter of the <see cref="Ocjknn"/> classifier
        /// </summary>
        public int K { get; set; }
        /// <summary>
        /// The Threshold parameter of the <see cref="Ocjknn"/> classifier
        /// </summary>
        public double Threshold { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="OneClassNearestNeighborClassifier"/> class.
        /// </summary>
        /// <param name="j">The J parameter of the <see cref="Ocjknn"/> classifier</param>
        /// <param name="k">The K parameter of the <see cref="Ocjknn"/> classifier</param>
        /// <param name="threshold">The K parameter of the <see cref="Ocjknn"/> classifier</param>
        /// <param name="distanceFunction">The distance function.</param>
        public OneClassNearestNeighborClassifier(int j = 1, int k = 1, double threshold = 1, IDistance<double[][]> distanceFunction = null)
        {
            J = j;
            K = k;
            Threshold = threshold;
            DistanceFunction = distanceFunction ?? new DtwDistance();
        }

        /// <inheritdoc/>
        public ISignerModel Train(List<Signature> signatures)
        {
            return Train(signatures, null);
        }

        /// <summary>
        /// Trains the specified signatures based on a precalculated distance matrix
        /// </summary>
        /// <param name="signatures">The signatures.</param>
        /// <param name="distanceMatrix">The distance matrix may contain all the distance pairs for the signatures. If you ommit this parameter, 
        /// distances will be calculated automatically using <see cref="DistanceFunction"/>.</param>
        /// <returns></returns>
        public ISignerModel Train(List<Signature> signatures, DistanceMatrix<string, string, double> distanceMatrix)
        {

            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'sigantures' can not be empty", nameof(signatures));

            var signerID = signatures[0].Signer?.ID;
            var trainSignaturesFeatures = signatures.Select(s => new KeyValuePair<string, double[][]>(s.ID, s.GetAggregateFeature(Features).ToArray())).ToList();

            // calculate the values for distance matrix, if it has not been passed as argument to the function
            if (distanceMatrix == null)
            {

                distanceMatrix = new DistanceMatrix<string, string, double>();

                foreach (var s1 in trainSignaturesFeatures)
                {
                    foreach (var s2 in trainSignaturesFeatures)
                    {
                        if (distanceMatrix.ContainsKey(s1.Key, s2.Key))
                            continue;
                        var distance = DistanceFunction.Calculate(s1.Value, s2.Value);
                        distanceMatrix[s1.Key, s2.Key] = distance;
                        distanceMatrix[s2.Key, s1.Key] = distance;
                        this.LogTrace(new ClassifierDistanceLogState(signerID, signerID, s1.Key, s2.Key, distance));
                    }
                }
            }

            SignerModel model = new SignerModel
            {
                SignerID = signerID,
                DistanceCache = distanceMatrix,
                TrainingSignatures = trainSignaturesFeatures
            };
            return model;

        }

        /// <inheritdoc/>
        public double Test(ISignerModel signerModel, Signature testSignature)
        {
            var model = (SignerModel)signerModel;
            var testSignatureFeatures = testSignature.GetAggregateFeature(Features).ToArray();
            foreach (var item in model.TrainingSignatures)
            {
                //avoid costly calculations, if the distance has already been calculated
                if (model.DistanceCache.ContainsKey(testSignature.ID, item.Key) || testSignature.ID == item.Key)
                    continue;

                 
                var distance = DistanceFunction.Calculate(testSignatureFeatures, item.Value);
                model.DistanceCache[testSignature.ID, item.Key] = distance;
                model.DistanceCache[item.Key, testSignature.ID] = distance;
            }

            return Ocjknn.Test(testSignature.ID, model.TrainingSignatures.Select(s => s.Key), J, K, Threshold, model.DistanceCache.GetDistance);
        }



    }
}
