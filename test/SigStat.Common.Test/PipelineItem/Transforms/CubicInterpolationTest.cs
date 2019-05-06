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
        CubicInterpolation ci = new CubicInterpolation();
        CubicInterpolation ci2 = new CubicInterpolation();

        [TestMethod]
        public void TestGetValueExceptionHandling()
        {
            try
            {
                double resultException = ci.GetValue(2.0);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("List of timestamps is null", ex.Message);
            }

            try
            {
                ci.TimeValues = new List<double> { 1.0, 2.0, 3.0 };
                double resultException = ci.GetValue(2.0);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("List of feature values is null", ex.Message);
            }
        }

        [TestMethod]
        public void TestGetValueContains()
        {
            List<double> timeValues = new List<double>();
            timeValues.Add(2.0);
            timeValues.Add(3.0);
            timeValues.Add(4.0);
            List<double> featueValues = new List<double>();
            featueValues.Add(3.0);
            featueValues.Add(4.0);
            featueValues.Add(5.0);


            //timeValues contains timestamps
            ci.TimeValues = timeValues;
            ci.FeatureValues = featueValues;

            double result1 = ci.GetValue(2.0);
            double expected1 = 3.0;
            Assert.AreEqual(expected1, result1);



        }

        [TestMethod]
        public void TestGetValueDoesNotContains()
        {
            List<double> timeValues = new List<double>();
            timeValues.Add(2.0);
            timeValues.Add(4.0);
            timeValues.Add(6.0);
            timeValues.Add(8.0);
            timeValues.Add(10.0);
            timeValues.Add(12.0);
            List<double> featueValues = new List<double>();
            featueValues.Add(2.0);
            featueValues.Add(4.0);
            featueValues.Add(6.0);
            featueValues.Add(8.0);
            featueValues.Add(10.0);
            featueValues.Add(12.0);

            //timeValues does not contains timestamps
            ci2.TimeValues = timeValues;
            ci2.FeatureValues = featueValues;

            double result2 = ci2.GetValue(3.0);
            double expected2 = 3.0;
            Assert.AreEqual(expected2, result2);

        }
    }
}
