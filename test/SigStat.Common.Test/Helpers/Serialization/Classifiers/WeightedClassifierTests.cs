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
            var weightedClassifier = new WeightedClassifierTests();
            var json = SerializationHelper.JsonSerialize(weightedClassifier);
            var expectedJson = @"{}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedWeightedClassifier = new WeightedClassifier();
            var dtwJson = @"{}";
            var deserializedWeightedClassifier = SerializationHelper.Deserialize<WeightedClassifier>(dtwJson);
            Assert.AreEqual(expectedWeightedClassifier.Items.Count, deserializedWeightedClassifier.Items.Count);
        }
    }
}
