using Newtonsoft.Json;
using SigStat.Common.Algorithms;
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
    public class NearestNeighborEerClassifier : PipelineBase
    {
        #region SignerModel
        /// <summary>
        /// Represents a trained model for <see cref="NearestNeighborEerClassifier"/>
        /// </summary>
        public class SignerModel : ISignerModel
        {
            /// <inheritdoc/>
            public string SignerID { get; set; }
            /// <summary>
            /// Gets or sets the signature distance from training.
            /// </summary>
            public Dictionary<string, double> SignatureDistanceFromTraining { get; set; }
            /// <summary>
            /// A threshold, that will be used for classification. Signatures with
            /// an average DTW distance from the genuines above this threshold will
            /// be classified as forgeries
            /// </summary>
            public double Threshold { get; set; }


            /// <summary>
            /// Gets or sets the error rates corresponding to specific thresholds
            /// </summary>
            public List<KeyValuePair<double, ErrorRate>> ErrorRates { get; set; }
            /// <summary>
            /// DTW distance matrix of the signatures
            /// </summary>
            public DistanceMatrix<string, string, double> DistanceMatrix { get; set; }
        }
        #endregion
        struct SignatureDistance : IEquatable<SignatureDistance>
        {
            public string ID;
            public Origin Origin;
            public double Distance;

            public bool Equals(SignatureDistance other)
            {
                return
                    ID == other.ID
                    && Origin.Equals(other.Origin)
                    && Math.Abs(Distance - other.Distance).EqualsZero();
            }
        }

        /// <summary>
        /// <see cref="FeatureDescriptor"/>s to consider during classification
        /// </summary>
        [Input]
        public List<FeatureDescriptor> Features { get; set; }

        /// <summary>
        /// <see cref="Sampler"/> used for selecting training and test sets during a benchmark
        /// </summary>
        public Sampler Sampler { get; set; }


        /// <summary>
        /// The function used to calculate the distance between two data sequences
        /// </summary>
        public IDistance<double[][]> DistanceFunction { get; set; }

        /// <summary>
        /// The number of nearest neighbors to consider during evaluation. Set it to null to use all training signatures.
        /// </summary>
        public int? NearestNeighborCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimalDtwClassifier"/> class.
        /// </summary>
        /// <param name="nearestNeighborCount">The number of nearest neighbours to consider during classification</param>
        /// <param name="distanceFunction">The distance function.</param>
        public NearestNeighborEerClassifier(int? nearestNeighborCount, IDistance<double[][]> distanceFunction = null)
        {
            NearestNeighborCount = nearestNeighborCount;
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

            var trainSignatures = Sampler.SampleReferences(signatures);
            var testGenuine = Sampler.SampleGenuineTests(signatures);
            var testForged = Sampler.SampleForgeryTests(signatures);
            var testSignatures = testGenuine.Concat(testForged).ToList();

            // calculate the values for distance matrix, if it has been passed as argument to the function
            if (distanceMatrix == null)
            {

                var trainSignaturesFeatures = trainSignatures.Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();
                var testSignatureFeatures = testSignatures.Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();


                distanceMatrix = new DistanceMatrix<string, string, double>();

                if (NearestNeighborCount == null)
                    NearestNeighborCount = trainSignatures.Count;

                if (NearestNeighborCount > trainSignatures.Count-1)
                    throw new ArgumentNullException("NearestNeighbourCount can not be larger or equal to the number of training signatures", nameof(signatures));

                if (NearestNeighborCount <= 0)
                    throw new ArgumentNullException("NearestNeighbourCount can not be larger than the number of training signatures", nameof(signatures));

                // The distances among training signatures aren't neccessarily needed for calculations. 
                // We calculate them anyways, as they may be needed for posprocessing the data 
                foreach (var train in trainSignaturesFeatures)
                {
                    foreach (var test in trainSignaturesFeatures.Concat(testSignatureFeatures))
                    {
                        var distance = DistanceFunction.Calculate(train.Values, test.Values);
                        distanceMatrix[test.ID, train.ID] = distance;
                        this.LogTrace(new ClassifierDistanceLogState(signerID, signerID, train.ID, test.ID, distance));
                    }
                }
            }


            // Calculate optimal threshold to approximate Equal Error Rate

            var averageDistances =
                testSignatures.Select(test => new SignatureDistance
                {
                    ID = test.ID,
                    Origin = test.Origin,
                    Distance = trainSignatures.Where(train => train.ID != test.ID)
                                              .Select(train => distanceMatrix[test.ID, train.ID])
                                              .OrderBy(d=>d)
                                              .Take(NearestNeighborCount?? trainSignatures.Count)
                                              .Average()
                })
                .OrderBy(d => d.Distance)
                .ToList();



            List<double> thresholds = new List<double>();
            thresholds.Add(0);
            for (int i = 0; i < averageDistances.Count - 1; i++)
            {
                thresholds.Add((averageDistances[i].Distance + averageDistances[i + 1].Distance) / 2);
            }

            thresholds.Add(averageDistances[averageDistances.Count - 1].Distance + 1);

            var errorRates = thresholds.Select(th => new KeyValuePair<double, ErrorRate>(
                th,
                CalculateErrorRate(th, averageDistances)
            )).ToList();


            SignerModel model = new SignerModel
            {
                SignerID = signerID,
                DistanceMatrix = distanceMatrix,
                SignatureDistanceFromTraining = averageDistances.ToDictionary(sig => sig.ID, sig => sig.Distance),
                ErrorRates = errorRates,
                Threshold = errorRates.First(e => e.Value.Far >= e.Value.Frr).Key
            };
            return model;

        }

        private ErrorRate CalculateErrorRate(double threshold, List<SignatureDistance> distances)
        {
            int genuineCount = 0, genuineError = 0;
            int forgedCount = 0, forgedError = 0;
            foreach (var item in distances)
            {
                switch (item.Origin)
                {
                    case Origin.Genuine:
                        genuineCount++;
                        if (item.Distance > threshold)
                            genuineError++;
                        break;
                    case Origin.Forged:
                        forgedCount++;
                        if (item.Distance <= threshold)
                            forgedError++;
                        break;
                    case Origin.Unknown:
                    default:
                        throw new NotSupportedException();
                }
            }

            return new ErrorRate { Far = (double)forgedError / forgedCount, Frr = (double)genuineError / genuineCount };
        }

        /// <inheritdoc/>
        public double Test(ISignerModel signerModel, Signature signature)
        {
            // We are not doing real classification here, just reusing the previously calculated results
            var model = (SignerModel)signerModel;
            var distance = model.SignatureDistanceFromTraining[signature.ID];
            if (distance <= model.Threshold)
                return 1;
            else
                return 0;

        }



    }
}
