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
            var expectedJson = $"{{\r\n  \"features\": {{}},\r\n  \"ID\": \"Demo\",\r\n  \"Origin\": 1,\r\n  \"Signer\": {{\r\n    \"ID\": \"S01\",\r\n    \"Signatures\": []\r\n  }}\r\n}}";
            Assert.AreEqual(expectedJson,json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var signatureJson = $"{{\r\n  \"features\": {{}},\r\n  \"ID\": \"Demo\",\r\n  \"Origin\": 1,\r\n  \"Signer\": {{\r\n    \"ID\": \"S01\",\r\n    \"Signatures\": []\r\n  }}\r\n}}";
            var deserializedSignature = SerializationHelper.Deserialize<Signature>(signatureJson);
            var expectedSignature = TestHelper.BuildSignature();
            Assert.AreEqual(expectedSignature.ID, deserializedSignature.ID);
            Assert.AreEqual(expectedSignature.Origin, deserializedSignature.Origin);
            Assert.AreEqual(expectedSignature.Signer.ID, deserializedSignature.Signer.ID);
        }

        [TestMethod]
        public void TestSerializationToFile()
        {
            var signature = TestHelper.BuildSignature();
            var path = "TestSerialization.json";
            SerializationHelper.JsonSerializeToFile(signature,path);
            var json = File.ReadAllText(path);
            var expectedJson = $"{{\r\n  \"features\": {{}},\r\n  \"ID\": \"Demo\",\r\n  \"Origin\": 1,\r\n  \"Signer\": {{\r\n    \"ID\": \"S01\",\r\n    \"Signatures\": []\r\n  }}\r\n}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserializationFromFile()
        {
            var expectedSignature = TestHelper.BuildSignature();
            var path = "TestSerialization.json";
            SerializationHelper.JsonSerializeToFile(expectedSignature, path);
            var deserializedSignature = SerializationHelper.DeserializeFromFile<Signature>(path);
            Assert.AreEqual(expectedSignature.ID, deserializedSignature.ID);
            Assert.AreEqual(expectedSignature.Origin, deserializedSignature.Origin);
            Assert.AreEqual(expectedSignature.Signer.ID, deserializedSignature.Signer.ID);
        }
    }
}
