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
            TestHelper.AssertJson(dtwClassifier, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var dtwClassifier = new DtwClassifier();
            var dtwJson = SerializationHelper.JsonSerialize(dtwClassifier);
            var deserializedDtw = SerializationHelper.Deserialize<DtwClassifier>(dtwJson);
            TestHelper.AssertJson(dtwClassifier, deserializedDtw);
        }
    }
}
