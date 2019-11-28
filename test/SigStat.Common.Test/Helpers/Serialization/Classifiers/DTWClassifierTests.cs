using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;
using SigStat.Common.PipelineItems.Classifiers;

namespace SigStat.Common.Test.Helpers.Serialization.Classifiers
{
    [TestClass]
    public class DTWClassifierTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var dtwClassifier = new DtwClassifier()
            {
                DistanceFunction = Accord.Math.Distance.Manhattan,
                Features = { Features.X, Features.Y },
                MultiplicationFactor = 3
            };
            var json = SerializationHelper.JsonSerialize(dtwClassifier, true);
            var expectedJson = @"{
              ""DistanceFunction"": ""Accord.Math.Distance.Manhattan, Accord.Math"",
              ""Features"": [""X"", ""Y""],
              ""MultiplicationFactor"": 3.0
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var dtwClassifier = new DtwClassifier()
            {
                DistanceFunction = Accord.Math.Distance.Manhattan,
                Features = { Features.X, Features.Y },
                MultiplicationFactor = 3
            };
            var dtwJson = SerializationHelper.JsonSerialize(dtwClassifier);
            var deserializedDtw = SerializationHelper.Deserialize<DtwClassifier>(dtwJson);            
            JsonAssert.AreEqual(dtwClassifier, deserializedDtw);
        }
    }
}
