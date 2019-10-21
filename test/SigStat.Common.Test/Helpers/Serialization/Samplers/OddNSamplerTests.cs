using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization.Samplers
{
    [TestClass]
    public class OddNSamplerTests
    {
        [TestMethod]
        public void TestSerialize()
        {
            var oddNSampler = new OddNSampler {N = 10};
            var json = SerializationHelper.JsonSerialize(oddNSampler);
            var expectedJson = "{\r\n  \"N\": 10\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedOddNSampler = new OddNSampler { N = 10 };
            var oddNSamplerJson = "{\r\n  \"N\": 10\r\n}";
            var deserializedOddNSampler = SerializationHelper.Deserialize<OddNSampler>(oddNSamplerJson);
            Assert.AreEqual(deserializedOddNSampler.N, expectedOddNSampler.N);
        }
    }
}
