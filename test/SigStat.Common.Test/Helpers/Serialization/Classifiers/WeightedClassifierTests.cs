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
            JsonAssert.AreEqual(weightedClassifier, json);
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
            Assert.AreEqual(weightedClassifier.Items.Count, deserializedWeightedClassifier.Items.Count);
        }
    }
}
