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
            TestHelper.AssertJson(signer, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedSigner = new Signer();
            expectedSigner.ID = "Test";
            var signerJson = SerializationHelper.JsonSerialize(expectedSigner);
            var deserializedSigner = SerializationHelper.Deserialize<Signer>(signerJson);
            TestHelper.AssertJson(expectedSigner, deserializedSigner);
        }
    }
}
