using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;
using SigStat.Common.PipelineItems.Classifiers;

namespace SigStat.Common.Test.Helpers.Serialization.Classifiers
{ 
    [TestClass]
    public class OptimalDtwClassifierTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var dtwClassifier = new OptimalDtwClassifier();
            var json = SerializationHelper.JsonSerialize(dtwClassifier);
            var expectedJson = @"{
  ""DistanceFunction"": ""Accord.Math.Distance, Accord.Math, Version=3.8.0.0, Culture=neutral, PublicKeyToken=null|Euclidean|System.Double[];System.Double[]""
}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expecteddtw = new OptimalDtwClassifier();
            var dtwJson = @"{
  ""DistanceFunction"": ""Accord.Math.Distance, Accord.Math, Version=3.8.0.0, Culture=neutral, PublicKeyToken=null|Euclidean|System.Double[];System.Double[]""
}";
            var deserializedDtw = SerializationHelper.Deserialize<OptimalDtwClassifier>(dtwJson);
            Assert.AreEqual(deserializedDtw.Sampler, expecteddtw.Sampler);
            Assert.AreEqual(deserializedDtw.DistanceFunction.Method, expecteddtw.DistanceFunction.Method);
        }
    }
}
