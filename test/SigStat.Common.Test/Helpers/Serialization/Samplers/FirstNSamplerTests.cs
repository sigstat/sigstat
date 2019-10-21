using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization.Samplers
{
    [TestClass]
    public class FirstNSamplerTests
    {
        [TestMethod]
        public void TestSerialize()
        {
            var firstNSampler = new FirstNSampler {N = 10};
            var json = SerializationHelper.JsonSerialize(firstNSampler);
            var expectedJson = "{\r\n  \"N\": 10\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedFirstNSampler = new FirstNSampler {N = 10};
            var firstNSamplerJson = "{\r\n  \"N\": 10\r\n}";
            var deserializedFirstNSampler = JsonConvert.DeserializeObject<FirstNSampler>(firstNSamplerJson);
            Assert.AreEqual(deserializedFirstNSampler.N, expectedFirstNSampler.N);
        }
    }
}
