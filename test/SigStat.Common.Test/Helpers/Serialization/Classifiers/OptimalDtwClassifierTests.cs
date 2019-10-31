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
            var optimalDtwClassifier = new OptimalDtwClassifier();
            var json = SerializationHelper.JsonSerialize(optimalDtwClassifier);
            JsonAssert.AreEqual(optimalDtwClassifier, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var optimalDtwClassifier = new OptimalDtwClassifier();
            var dtwJson = SerializationHelper.JsonSerialize(optimalDtwClassifier);
            var deserializedDtw = SerializationHelper.Deserialize<OptimalDtwClassifier>(dtwJson);
            Assert.AreEqual(deserializedDtw.Sampler, optimalDtwClassifier.Sampler);
            Assert.AreEqual(deserializedDtw.DistanceFunction.Method, optimalDtwClassifier.DistanceFunction.Method);
        }
    }
}
