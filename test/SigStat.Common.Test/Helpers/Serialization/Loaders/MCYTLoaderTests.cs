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
            var expectedJson = "{\r\n  \"DatabasePath\": \"Databases\\\\Online\\\\MCYT\\\\Task2.zip\",\r\n  \"StandardFeatures\": true\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedMcytLoader = new MCYTLoader(@"Databases\Online\MCYT\Task2.zip".GetPath(), true);
            var mcytLoaderJson = "{\r\n  \"DatabasePath\": \"Databases\\\\Online\\\\MCYT\\\\Task2.zip\",\r\n  \"StandardFeatures\": true\r\n}";
            var deserializedLoader = SerializationHelper.Deserialize<Svc2004Loader>(mcytLoaderJson);
            Assert.AreEqual(deserializedLoader.DatabasePath, expectedMcytLoader.DatabasePath);
            Assert.IsTrue(deserializedLoader.StandardFeatures);
        }
    }
}
