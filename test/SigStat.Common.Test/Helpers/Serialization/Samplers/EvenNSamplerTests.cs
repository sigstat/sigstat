using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization.Samplers
{
    [TestClass]
    public class EvenNSamplerTests
    {
        [TestMethod]
        public void TestSerialize()
        {
            var evenNSampler = new EvenNSampler {N = 10};
            var json = SerializationHelper.JsonSerialize(evenNSampler);
            var expectedJson = "{\r\n  \"N\": 10\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedEvenNSampler = new EvenNSampler {N = 10};
            var evenNSamplerJson = "{\r\n  \"N\": 10\r\n}";
            var deserializedEvenNSampler = SerializationHelper.Deserialize<EvenNSampler>(evenNSamplerJson);
            Assert.AreEqual(deserializedEvenNSampler.N, expectedEvenNSampler.N);
        }
    }
}
