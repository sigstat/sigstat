using Newtonsoft.Json;
using SigStat.Common.Algorithms;
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
    public class OptimalDtwClassifier : PipelineBase, IDistanceClassifier
    {
        #region SignerModel
        /// <summary>
        /// Represents a trained model for <see cref="OptimalDtwClassifier"/>
        /// </summary>
        public class OptimalDtwSignerModel : ISignerModel
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
        struct SignatureDistance
        {
            public string ID;
            public Origin Origin;
            public double Distance;
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
        /// The function used to calculate the distance between two data points during DTW calculation
        /// </summary>
        [JsonConverter(typeof(DistanceFunctionJsonConverter))]
        public Func<double[], double[], double> DistanceFunction { get; set; }

        /// <summary>
        /// Length of the warping window to be used with DTW
        /// </summary>
        public int WarpingWindowLength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimalDtwClassifier"/> class.
        /// </summary>
        /// <param name="distanceFunction">The distance function.</param>
        public OptimalDtwClassifier(Func<double[], double[], double> distanceFunction = null)
        {
            DistanceFunction = distanceFunction ?? Accord.Math.Distance.Euclidean;
        }

        /// <inheritdoc/>
        public ISignerModel Train(List<Signature> signatures)
        {

            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'sigantures' can not be empty", nameof(signatures));

            var signerID = signatures[0].Signer?.ID;

            var trainSignatures = Sampler.SampleReferences(signatures).Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();
            var testGenuine = Sampler.SampleGenuineTests(signatures).Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();
            var testForged = Sampler.SampleForgeryTests(signatures).Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();

            var testSignatures = testGenuine.Concat(testForged).ToList();

            var dtwDistances = new DistanceMatrix<string, string, double>();



            foreach (var train in trainSignatures)
            {
                foreach (var test in trainSignatures.Concat(testSignatures))
                {
                    var distance = DtwPy.Dtw(train.Values, test.Values, DistanceFunction);
                    dtwDistances[test.ID, train.ID] = distance;
                    this.LogTrace(new ClassifierDistanceLogState(signerID, signerID, train.ID, test.ID, distance));
                }
            }

            var averageDistances =
                testSignatures.Select(test => new SignatureDistance
                {
                    ID = test.ID,
                    Origin = test.Origin,
                    Distance = trainSignatures.Where(train => train.ID != test.ID).Select(train => dtwDistances[test.ID, train.ID]).Average()
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


            OptimalDtwSignerModel model = new OptimalDtwSignerModel()
            {
                SignerID = signerID,
                DistanceMatrix = dtwDistances,
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
            var model = (OptimalDtwSignerModel)signerModel;
            var distance = model.SignatureDistanceFromTraining[signature.ID];
            if (distance <= model.Threshold)
                return 1;
            else
                return 0;

        }



    }
}
