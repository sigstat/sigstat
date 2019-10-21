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
            var dtwClassifier = new DtwClassifier();
            var json = SerializationHelper.JsonSerialize(dtwClassifier);
            var expectedJson = @"{
  ""DistanceFunction"": ""Accord.Math.Distance, Accord.Math, Version=3.8.0.0, Culture=neutral, PublicKeyToken=null|Manhattan|System.Double[];System.Double[]"",
  ""Features"":[],
  ""MultiplicationFactor"": 1.0
}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expecteddtw = new DtwClassifier();
            var dtwJson = @"{
  ""DistanceFunction"": ""Accord.Math.Distance, Accord.Math, Version=3.8.0.0, Culture=neutral, PublicKeyToken=null|Manhattan|System.Double[];System.Double[]"",
  ""Features"":[],
  ""MultiplicationFactor"": 1.0
}";
            var deserializedDtw = SerializationHelper.Deserialize<DtwClassifier>(dtwJson);
            Assert.AreEqual(deserializedDtw.MultiplicationFactor, expecteddtw.MultiplicationFactor);
            Assert.AreEqual(deserializedDtw.DistanceFunction.Method, expecteddtw.DistanceFunction.Method);
        }
    }
}
