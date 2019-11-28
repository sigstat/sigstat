using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;
using SigStat.Common.PipelineItems.Classifiers;

namespace SigStat.Common.Test.Helpers.Serialization.Classifiers
{
    [TestClass]
    public class WeightedClassifierTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var weightedClassifier = new WeightedClassifier
            {
                (new DtwClassifier(Accord.Math.Distance.Manhattan),
                    0.15),
                (new OptimalDtwClassifier(Accord.Math.Distance.Euclidean),
                    0.5),
            };
            var json = SerializationHelper.JsonSerialize(weightedClassifier);
            var expectedJson = @"{
              ""Items"": [
                {
                  ""Item1"": {
                    ""$type"": ""SigStat.Common.PipelineItems.Classifiers.DtwClassifier, SigStat.Common"",
                    ""DistanceFunction"": ""Accord.Math.Distance.Manhattan, Accord.Math"",
                    ""Features"":[],
                    ""MultiplicationFactor"": 1.0
                  },
                  ""Item2"": 0.15
                },
                {
                  ""Item1"": {
                    ""$type"": ""SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier, SigStat.Common"",
                    ""DistanceFunction"": ""Accord.Math.Distance.Euclidean, Accord.Math"",
                    ""WarpingWindowLength"": 0
                  },
                  ""Item2"": 0.5
                }
              ]
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var weightedClassifier = new WeightedClassifier
            {
                (new DtwClassifier(Accord.Math.Distance.Manhattan),
                    0.15),
                (new OptimalDtwClassifier(Accord.Math.Distance.Euclidean),
                    0.5),
            };
            var weightedJson = SerializationHelper.JsonSerialize(weightedClassifier);
            var deserializedWeightedClassifier = SerializationHelper.Deserialize<WeightedClassifier>(weightedJson);
            JsonAssert.AreEqual(weightedClassifier,deserializedWeightedClassifier);
        }
    }
}
