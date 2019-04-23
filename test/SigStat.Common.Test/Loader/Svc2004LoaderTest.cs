using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigStat.Common.Test.Loader
{
    [TestClass]
    public class Svc2004LoaderTest
    {
        [TestMethod]
        public void TestLoadSignatureFromFile()
        {

            Svc2004Loader loader = new Svc2004Loader(@"Databases\Test\SVC2004\Test.zip", true);
            var signers = new List<Signer>(loader.EnumerateSigners());

            Assert.AreEqual(1, signers.Count);
            var signer = signers[0];
            Assert.AreEqual("01", signer);

            Assert.AreEqual(2, signer.Signatures.Count);
            Assert.AreEqual("01", signer.Signatures[0].ID);

            Assert.AreEqual(signer, signer.Signatures[0].Signer, "The loaded signer object and the signer instance referenced by the signer are not the same");

            Assert.AreEqual(Origin.Genuine, signer.Signatures[0].Origin);
            Assert.AreEqual(Origin.Forged, signer.Signatures[1].Origin);

            var signature = signer.Signatures[0];

            foreach (var expectedDescriptor in Svc2004.All)
            {
                Assert.IsTrue(signature.HasFeature(expectedDescriptor), $"{expectedDescriptor.Name} was not found in signature");
            }

            foreach (var descriptor in signature.GetFeatureDescriptors())
            {
                var featureValues = (List<int>)signature[descriptor];
                Assert.AreEqual(84, featureValues.Count);
            }
        }
        [TestMethod]
        public void TestLoadSignatureFromStream()
        {
            //   public static void LoadSignature(Signature signature, Stream stream, bool standardFeatures)
        }

        [TestMethod]
        public void TestParseSignature()
        {
            //  private static void ParseSignature(Signature signature, string[] linesArray, bool standardFeatures)
        }


    }
}
