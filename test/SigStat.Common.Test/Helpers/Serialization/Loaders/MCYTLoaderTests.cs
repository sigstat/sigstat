using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;

namespace SigStat.Common.Test.Helpers.Serialization.Loaders
{
    [TestClass]
    public class MCYTLoaderTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var mcytLoader = new MCYTLoader(@"Databases\Online\MCYT\Task2.zip".GetPath(), true);
            var json = SerializationHelper.JsonSerialize(mcytLoader);
            TestHelper.AssertJson(mcytLoader, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedMcytLoader = new MCYTLoader(@"Databases\Online\MCYT\Task2.zip".GetPath(), true);
            var mcytLoaderJson = SerializationHelper.JsonSerialize(expectedMcytLoader);
            var deserializedLoader = SerializationHelper.Deserialize<Svc2004Loader>(mcytLoaderJson);
            TestHelper.AssertJson(expectedMcytLoader, deserializedLoader);
        }
    }
}
