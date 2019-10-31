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
            JsonAssert.AreEqual(oddNSampler, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedOddNSampler = new OddNSampler { N = 10 };
            var oddNSamplerJson = SerializationHelper.JsonSerialize(expectedOddNSampler);
            var deserializedOddNSampler = SerializationHelper.Deserialize<OddNSampler>(oddNSamplerJson);
            JsonAssert.AreEqual(expectedOddNSampler, deserializedOddNSampler);
        }
    }
}
