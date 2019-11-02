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
            var expectedJson = @"{
              ""DatabasePath"": ""Databases\\Online\\SVC2004\\Task2.zip"",
              ""StandardFeatures"": true
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var expectedSvcLoader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true);
            var svcLoaderJson = SerializationHelper.JsonSerialize(expectedSvcLoader);
            var deserializedLoader = SerializationHelper.Deserialize<Svc2004Loader>(svcLoaderJson);
            JsonAssert.AreEqual(expectedSvcLoader, deserializedLoader);
        }
    }
}
