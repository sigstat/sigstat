using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Algorithms;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.Algorithms
{
    [TestClass]
    public class DtwExperimentsTests
    {
        int[] s1 = new[] { 1, 4, 1, 1, 1, 1, 4 };
        int[] s2 = new[] { 4, 1, 1, 1, 1, 4, 1 };

        [TestMethod]
        public void TestComputeTwoDimension()
        {
            var dtw1 = DtwExperiments.ExactDtw(s1, s2, EuclideanDistance);
            var dtw2 = DtwExperiments.ExactDtwWikipedia(s1, s2, EuclideanDistance);
            var dtw3 = DtwExperiments.OptimizedDtw(s1, s2, EuclideanDistance);


            Assert.AreEqual(dtw1, dtw2);
            Assert.AreEqual(dtw1, dtw3);

        }


        public static double EuclideanDistance(int d1, int d2)
        {
            return Math.Abs(d1 - d2);
        }


        public static double GenericEuclideanDistance(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException();
            }

            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                double dist = vector1[i] - vector2[i];
                sum += dist * dist;
            }
            return Math.Sqrt(sum);
        }

    }
}
