using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;


namespace SigStat.Common.Test.PipelineItem.Transforms
{
    [TestClass]
    public class TranslateTest
    {
        [TestMethod]

        public void TestTranslatePreproc()
        {
            OriginType ot1 = OriginType.CenterOfGravity;
            OriginType ot2 = OriginType.Maximum;
            OriginType ot3 = OriginType.Minimum;
            OriginType ot4 = OriginType.Predefined;
            TranslatePreproc tp1 = new TranslatePreproc(ot1);
            TranslatePreproc tp2 = new TranslatePreproc(ot2);
            TranslatePreproc tp3 = new TranslatePreproc(ot3);
            TranslatePreproc tp4 = new TranslatePreproc(ot4);
            Assert.AreEqual(tp1.GoalOrigin, ot1);
            Assert.AreEqual(tp2.GoalOrigin, ot2);
            Assert.AreEqual(tp3.GoalOrigin, ot3);
            Assert.AreEqual(tp4.GoalOrigin, ot4);
        }

        [TestMethod]
        public void TestTransform()
        {
            var signature = TestHelper.BuildSignature()
                .SetFeature(Features.X, new List<double> { 1, 2, 3 })
                .SetFeature(Features.Y, new List<double> { 1, 2, 3 });

            TranslatePreproc tp1 = new TranslatePreproc(OriginType.CenterOfGravity);
            
            try
            {
                tp1.Transform(signature);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Input or output feature is null", ex.Message);
            }

            TranslatePreproc tp2 = new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X };
      
            tp2.Transform(signature);

        }

        [TestMethod]
        public void TestCOGTransform()
        {
            //private void COGTransform(Signature sig)

        }
        [TestMethod]
        public void TestExtremaTransform()
        {

            //private void ExtremaTransform(Signature sig, bool isMax)
        }
        [TestMethod]
        public void TestTranslateToPredefinedOrigin()
        {
            //private void TranslateToPredefinedOrigin(Signature sig, double newOrigin)
        }
    }
}
