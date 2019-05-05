using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.PipelineItem.Transforms
{
    [TestClass]
    public class CubicInterpolationTest
    {
        [TestMethod]
        public void TestGetValue()
        {
            List<double> timeValues = new List<double>();
            timeValues.Add(2.0);
            timeValues.Add(3.0);
            timeValues.Add(4.0);
            List<double> featueValues = new List<double>();
            featueValues.Add(3.0);
            featueValues.Add(4.0);
            featueValues.Add(5.0);
            
            CubicInterpolation ci = new CubicInterpolation();
            ci.TimeValues = timeValues;
            ci.FeatureValues = featueValues;

            double result = ci.GetValue(2.0);
            double expected = 3.0;

            Assert.AreEqual(expected, result);
        }
        
    }
}
