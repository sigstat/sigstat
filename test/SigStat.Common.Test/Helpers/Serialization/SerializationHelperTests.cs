using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class SerializationHelperTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var signature =  TestHelper.BuildSignature();
            var json = SerializationHelper.JsonSerialize(signature);
            TestHelper.AssertJson(signature, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var signature = TestHelper.BuildSignature();
            var signatureJson = SerializationHelper.JsonSerialize(signature);
            var deserializedSignature = SerializationHelper.Deserialize<Signature>(signatureJson);
            TestHelper.AssertJson(signature, deserializedSignature);
        }

        [TestMethod]
        public void TestSerializationToFile()
        {
            var signature = TestHelper.BuildSignature();
            var path = "TestSerialization.json";
            SerializationHelper.JsonSerializeToFile(signature,path);
            var json = File.ReadAllText(path);
            TestHelper.AssertJson(signature, json);
        }

        [TestMethod]
        public void TestDeserializationFromFile()
        {
            var expectedSignature = TestHelper.BuildSignature();
            var path = "TestSerialization.json";
            SerializationHelper.JsonSerializeToFile(expectedSignature, path);
            var deserializedSignature = SerializationHelper.DeserializeFromFile<Signature>(path);
            TestHelper.AssertJson(expectedSignature,deserializedSignature);
        }
    }
}
