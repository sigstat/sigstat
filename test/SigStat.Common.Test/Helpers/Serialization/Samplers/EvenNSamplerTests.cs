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
            TestHelper.AssertJson(evenNSampler, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedEvenNSampler = new EvenNSampler {N = 10};
            var evenNSamplerJson = SerializationHelper.JsonSerialize(expectedEvenNSampler);
            var deserializedEvenNSampler = SerializationHelper.Deserialize<EvenNSampler>(evenNSamplerJson);
            TestHelper.AssertJson(expectedEvenNSampler, deserializedEvenNSampler);
        }
    }
}
