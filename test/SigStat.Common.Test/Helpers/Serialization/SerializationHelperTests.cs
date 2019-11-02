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
            var signature =  JsonAssert.BuildSignature();
            var json = SerializationHelper.JsonSerialize(signature);
            var expectedJson = @"{
              ""features"": {},
              ""ID"": ""Demo"",
              ""Origin"": 1,
              ""Signer"": {
              ""ID"": ""S01"",
              ""Signatures"": []
              }
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var signature = JsonAssert.BuildSignature();
            var signatureJson = SerializationHelper.JsonSerialize(signature);
            var deserializedSignature = SerializationHelper.Deserialize<Signature>(signatureJson);
            JsonAssert.AreEqual(signature, deserializedSignature);
        }

        [TestMethod]
        public void TestSerializationToFile()
        {
            var signature = JsonAssert.BuildSignature();
            var path = "TestSerialization.json";
            SerializationHelper.JsonSerializeToFile(signature,path);
            var json = File.ReadAllText(path);
            var expectedJson = @"{
              ""features"": {},
              ""ID"": ""Demo"",
              ""Origin"": 1,
              ""Signer"": {
              ""ID"": ""S01"",
              ""Signatures"": []
              }
            }";
            JsonAssert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserializationFromFile()
        {
            var expectedSignature = JsonAssert.BuildSignature();
            var path = "TestSerialization.json";
            SerializationHelper.JsonSerializeToFile(expectedSignature, path);
            var deserializedSignature = SerializationHelper.DeserializeFromFile<Signature>(path);
            JsonAssert.AreEqual(expectedSignature,deserializedSignature);
        }
    }
}
