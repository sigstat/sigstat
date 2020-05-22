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
            var expectedJson = @"{
              ""TrainingCount"": 5,
              ""TestCount"": 20
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedUniversalSampler = new UniversalSampler(5,20);
            var universalNSamplerJson = SerializationHelper.JsonSerialize(expectedUniversalSampler);
            var deserializedUniversalNSampler = SerializationHelper.Deserialize<UniversalSampler>(universalNSamplerJson);
            JsonAssert.AreEqual(expectedUniversalSampler, deserializedUniversalNSampler);
        }
    }
}
