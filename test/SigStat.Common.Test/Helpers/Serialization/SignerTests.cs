using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Helpers;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class SignerTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            var signer = new Signer();
            signer.ID = "Test";
            var json = SerializationHelper.JsonSerialize(signer);
            var expectedJson = "{\r\n  \"ID\": \"Test\",\r\n  \"Signatures\": []\r\n}";
            Assert.AreEqual(expectedJson,json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedSigner = new Signer();
            expectedSigner.ID = "Test";
            var signerJson = "{\r\n  \"ID\": \"Test\",\r\n  \"Signatures\": []\r\n}";
            var deserializedSigner = SerializationHelper.Deserialize<Signer>(signerJson);
            Assert.AreEqual(deserializedSigner.ID, expectedSigner.ID);
        }
    }
}
