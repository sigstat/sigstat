using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization.Samplers
{
    [TestClass]
    public class UniversalSamplerTests
    {
        [TestMethod]
        public void TestSerialize()
        {
            var universalSampler = new UniversalSampler(5,20);
            var json = SerializationHelper.JsonSerialize(universalSampler);
            var expectedJson = "{\r\n  \"TrainingCount\": 5,\r\n  \"TestCount\": 20\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedUniversalSampler = new UniversalSampler(5,20);
            var universalNSamplerJson = "{\r\n  \"TrainingCount\": 5,\r\n  \"TestCount\": 20\r\n}";
            var deserializedUniversalNSampler = SerializationHelper.Deserialize<UniversalSampler>(universalNSamplerJson);
            Assert.AreEqual(deserializedUniversalNSampler.TrainingCount, expectedUniversalSampler.TrainingCount);
            Assert.AreEqual(deserializedUniversalNSampler.TestCount, expectedUniversalSampler.TestCount);
        }
    }
}
