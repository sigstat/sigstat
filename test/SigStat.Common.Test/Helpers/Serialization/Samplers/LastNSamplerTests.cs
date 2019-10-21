using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization.Samplers
{
    [TestClass]
    public class LastNSamplerTests
    {
        [TestMethod]
        public void TestSerialize()
        {
            var lastNSampler = new LastNSampler {N = 10};
            var json = SerializationHelper.JsonSerialize(lastNSampler);
            var expectedJson = "{\r\n  \"N\": 10\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedLastNSampler = new LastNSampler { N = 10 };
            var lastNSamplerJson = "{\r\n  \"N\": 10\r\n}";
            var deserializedLastNSampler = SerializationHelper.Deserialize<LastNSampler>(lastNSamplerJson);
            Assert.AreEqual(deserializedLastNSampler.N, expectedLastNSampler.N);
        }
    }
}
