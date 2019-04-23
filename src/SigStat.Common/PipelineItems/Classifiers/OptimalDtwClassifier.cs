using Newtonsoft.Json;
using SigStat.Common.Algorithms;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    [JsonObject(MemberSerialization.OptOut)]
    public class OptimalDtwClassifier : PipelineBase, IClassifier
    {
        #region SignerModel
        /// <summary>
        /// Represents a trained model for <see cref="OptimalDtwClassifier"/>
        /// </summary>
        public class OptimalDtwSignerModel : ISignerModel
        {
            public Dictionary<string, double> SignatureDistanceFromTraining { get; set; }
            /// <summary>
            /// A threshold, that will be used for classification. Signatures with
            /// an average DTW distance from the genuines above this threshold will
            /// be classified as forgeries
            /// </summary>
            public double Threshold { get; set; }


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

        [Input]
        
        public List<FeatureDescriptor> Features { get; set; }

        public Sampler Sampler { get; set; }
        [JsonIgnore]
        public Func<double[], double[], double> DistanceFunction { get; set; }

        public OptimalDtwClassifier(Func<double[], double[], double> distanceFunction = null)
        {
            DistanceFunction = distanceFunction ?? Accord.Math.Distance.Euclidean;
        }

        public ISignerModel Train(List<Signature> signatures)
        {

            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));
            if (signatures.Count == 0)
                throw new ArgumentException("'sigantures' can not be empty", nameof(signatures));

            var trainSignatures = Sampler.SampleReferences(signatures).Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();
            var testGenuine = Sampler.SampleGenuineTests(signatures).Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();
            var testForged = Sampler.SampleForgeryTests(signatures).Select(s => new { s.ID, s.Origin, Values = s.GetAggregateFeature(Features).ToArray() }).ToList();

            var testSignatures = testGenuine.Concat(testForged).ToList();

            var dtwDistances = new DistanceMatrix<string, string, double>();



            foreach (var train in trainSignatures)
            {
                foreach (var test in trainSignatures.Concat(testSignatures))
                {
                    dtwDistances[test.ID, train.ID] = DtwPy.Dtw(train.Values, test.Values, DistanceFunction);
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
