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
            JsonAssert.AreEqual(lastNSampler, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedLastNSampler = new LastNSampler { N = 10 };
            var lastNSamplerJson = SerializationHelper.JsonSerialize(expectedLastNSampler);
            var deserializedLastNSampler = SerializationHelper.Deserialize<LastNSampler>(lastNSamplerJson);
            JsonAssert.AreEqual(expectedLastNSampler, deserializedLastNSampler);
        }
    }
}
