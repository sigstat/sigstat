using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;

namespace SigStat.Common.Test.Helpers.Serialization.Loaders
{
    [TestClass]
    public class Svc2004LoaderTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var svc2004Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
            var json = SerializationHelper.JsonSerialize(svc2004Loader);
            var expectedJson = "{\r\n  \"DatabasePath\": \"Databases\\\\Online\\\\SVC2004\\\\Task2.zip\",\r\n  \"StandardFeatures\": true\r\n}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedSvcLoader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
            var svcLoaderJson = "{\r\n  \"DatabasePath\": \"Databases\\\\Online\\\\SVC2004\\\\Task2.zip\",\r\n  \"StandardFeatures\": true\r\n}";
            var deserializedLoader = SerializationHelper.Deserialize<Svc2004Loader>(svcLoaderJson);
            Assert.AreEqual(deserializedLoader.DatabasePath, expectedSvcLoader.DatabasePath);
            Assert.IsTrue(deserializedLoader.StandardFeatures);
        }
    }
}
