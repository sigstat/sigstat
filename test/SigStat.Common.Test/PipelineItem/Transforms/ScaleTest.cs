using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.PipelineItem.Transforms
{
    [TestClass]
    public class ScaleTest
    {
        [TestMethod]
        public void TestTransform()
        {
            Signature signature = new Signature();
            signature.ID = "Demo";
            signature.Origin = Origin.Genuine;
            signature.Signer = new Signer()
            {
                ID = "S05"
            };

            new Scale();

            //Sample, Program.cs
  /*              new Scale()
            {
                InputFeature = SigStat.Common.Features.X,
                    NewMinValue = 100,
                    NewMaxValue = 500,
                    OutputFeature = FeatureDescriptor.Get<List<double>>("ScalingResult")
                }.Transform(signature);

    */
    //            sc.Transform(signature);
            //public void Transform(Signature signature)
        }
    }
}
