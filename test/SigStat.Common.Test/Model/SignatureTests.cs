using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common.Test.Model
{
    [TestClass]
    public class SignatureTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            string ID = "testid";
            Origin origin = new Origin();
            Signer signer = new Signer();

            Signature signature = new Signature(ID, origin, signer);

            Assert.AreEqual(ID, signature.ID);
            Assert.AreEqual(origin, signature.Origin);
            Assert.AreEqual(signer, signature.Signer);

            signature = new Signature();
            Assert.IsNotNull(signature);
        }

        [TestMethod]
        public void TestGetterSetter()
        {
            string ID = "testid";
            Origin origin = new Origin();
            Signer signer = new Signer();

            Signature signature = new Signature();

            signature.ID = ID;
            signature.Origin = origin;
            signature.Signer = signer;

            Assert.AreEqual(ID, signature.ID);
            Assert.AreEqual(origin, signature.Origin);
            Assert.AreEqual(signer, signature.Signer);

            string feature = "test feature";
            signature["test"] = feature;

            Assert.AreEqual(signature["test"], feature);

            FeatureDescriptor featureDescriptor = FeatureDescriptor.Get<String>("Desc");

            signature[featureDescriptor] = "desc";
            Assert.AreEqual(signature[featureDescriptor], "desc");

            Assert.AreEqual(signature.GetFeature<String>("Desc"), "desc");
            Assert.AreEqual(signature.GetFeature<String>(featureDescriptor), "desc");

            var featureDescriptors = signature.GetFeatureDescriptors();
            Assert.IsNotNull(featureDescriptors);

            signature.SetFeature<String>(featureDescriptor, "feat");
            Assert.AreEqual(signature.GetFeature<String>("Desc"), "feat");

            signature.SetFeature<String>("Desc", "feat2");
            Assert.AreEqual(signature.GetFeature<String>("Desc"), "feat2");

            FeatureDescriptor featureDescriptor2 = FeatureDescriptor.Get<String>("Desc2");
            signature[featureDescriptor2] = "desc2";

            var fs = new List<FeatureDescriptor>();
            fs.Add(featureDescriptor);
            fs.Add(featureDescriptor2);

            //Assert.IsNotNull(signature.GetAggregateFeature(fs));

            Assert.IsTrue(signature.HasFeature(featureDescriptor));
            Assert.IsTrue(signature.HasFeature("Desc2"));

            Assert.IsNotNull(signature.ToString());

        }

        [TestMethod]
        public void GetAggregateFeatureTest()
        {
            string ID = "testid";
            Origin origin = new Origin();
            Signer signer = new Signer();

            Signature signature = new Signature(ID, origin, signer);

            FeatureDescriptor featureDescriptor = FeatureDescriptor.Get<String>("Desc");
            signature[featureDescriptor] = "desc";

            FeatureDescriptor featureDescriptor2 = FeatureDescriptor.Get<String>("Desc2");
            signature[featureDescriptor2] = "desc2";

            List<FeatureDescriptor> fs = new List<FeatureDescriptor>();
            fs.Add(featureDescriptor);
            fs.Add(featureDescriptor2);

        //    Assert.AreEqual(signature.GetAggregateFeature, featureDescriptor);
            
    }



}
}

