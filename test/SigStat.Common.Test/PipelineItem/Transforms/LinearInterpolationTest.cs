using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.PipelineItem.Transforms
{
    [TestClass]
    public class LinearInterpolationTest
    {
        LinearInterpolation li = new LinearInterpolation();
        LinearInterpolation li2 = new LinearInterpolation();

        [TestMethod]
        public void TestGetValueExceptionHandling()
        {
            try
            {
                double resultException = li.GetValue(2.0);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("TimeValues is not initialized", ex.Message);
            }

            try
            {
                li.TimeValues = new List<double> { 1.0, 2.0, 3.0 };
                double resultException = li.GetValue(2.0);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("FeatureValues is not initialized", ex.Message);
            }
        }

        [TestMethod]
        public void TestGetValue()
        {
            List<double> timeValues = new List<double>() { 2.0, 3.0, 4.0};
            List<double> featueValues = new List<double>() { 3.0,4.0,5.0};
            
            LinearInterpolation li = new LinearInterpolation();
            li.TimeValues = timeValues;
            li.FeatureValues = featueValues;

            double result = li.GetValue(2.0);
            double expected = 3.0;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetValueDoesNotContains()
        {
            List<double> timeValues = new List<double>() { 2.0,4.0,6.0,8.0,10.0,12.0 };
            
            List<double> featueValues = new List<double>() { 2.0, 4.0, 6.0, 8.0, 10.0, 12.0 };
            
            //timeValues does not contains timestamps
            li2.TimeValues = timeValues;
            li2.FeatureValues = featueValues;

            double result2 = li2.GetValue(3.0);
            double expected2 = 3.0;
            
            Assert.AreEqual(expected2, result2);

        }
    }
}
