using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.PipelineItem.Transforms
{
    [TestClass]
    public class UniformScaleTest
    {
        [TestMethod]
        public void TestTransform()
        {
            new UniformScale();
            //public void Transform(Signature signature)
        }
    }
}
