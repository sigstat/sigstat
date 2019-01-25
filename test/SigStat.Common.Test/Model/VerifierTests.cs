using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System.Collections.Generic;
using System.IO;

namespace SigStat.Common.Test.Model
{
    [TestClass]
    public class VerifierTests
    {
       [TestMethod]
       public void TestConstructors()
        {
            var v1 = new Verifier();
            Assert.IsNotNull(v1);
            Assert.IsNotNull(v1.Classifier);
            Assert.IsNotNull(v1.Pipeline);

            var v2 = new Verifier(v1);
            Assert.AreSame(v2.Pipeline, v1.Pipeline);
            Assert.AreSame(v2.Classifier, v1.Classifier);
        }

        [TestMethod]
        public void TestTrain()
        {
            //TODO: The sample code is not working yet.
        }

        [TestMethod]
        public void TestTest()
        {
            //TODO: The sample code is not working yet.
        }

        [TestMethod]
        public void TestBasicVerifier()
        {
            var v = Verifier.BasicVerifier;
            Assert.IsNotNull(v);
        }
    }
}
