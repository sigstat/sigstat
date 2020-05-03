using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Algorithms.Distances;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SigStat.Common.Test.Algorithms
{
    [TestClass]
    public class DtwExperimentsTests
    {
        int[] s1 = new[] { 1, 4, 1, 1, 1, 1, 4 };
        int[] s2 = new[] { 4, 1, 1, 1, 4 };

        [TestMethod]
        public void Test1DSmall()
        {
            var dtw1 = DtwImplementations.ExactDtw(s1, s2, EuclideanDistance);
            var dtw2 = DtwImplementations.ExactDtwWikipedia(s1, s2, EuclideanDistance);
            var dtw3 = DtwImplementations.OptimizedDtw(s1, s2, EuclideanDistance);



            Assert.AreEqual(dtw1, dtw2);
            Assert.AreEqual(dtw1, dtw3);
        }
        [TestMethod]
        public void Test1DRandom()
        {
            Random random = new Random(1234);
            var s1 = Enumerable.Range(0, 10000).Select(i => random.Next(1000)).ToList();
            var s2 = Enumerable.Range(0, 10000).Select(i => random.Next(1000)).ToList();
            var sw = Stopwatch.StartNew();
            var dtw1 = DtwImplementations.ExactDtw(s1, s2, EuclideanDistance);
            var t1 = sw.Elapsed;
            sw.Restart();
            var dtw2 = DtwImplementations.ExactDtwWikipedia(s1, s2, EuclideanDistance);
            var t2 = sw.Elapsed;
            sw.Restart();
           // var dtw3 = DtwImplementations.OptimizedDtw(s1, s2, EuclideanDistance);
            var t3 = sw.Elapsed;
            sw.Restart(); 
            var dtw4 = DtwPy.Dtw(s1, s2, EuclideanDistance);
            var t4 = sw.Elapsed;


            Assert.AreEqual(dtw1, dtw2);
            //Assert.AreEqual(dtw1, dtw3);
            Assert.AreEqual(dtw1, dtw4);
        }


        [TestMethod]
        public void Test3DRandom()
        {
            var dist = new EuclideanDistance();
            Random random = new Random(1234);
            var s1 = Enumerable.Range(0, 10000).Select(i =>new double[] { random.Next(1000), random.Next(1000), random.Next(1000) }).ToArray();
            var s2 = Enumerable.Range(0, 9000).Select(i =>new double[] { random.Next(1000), random.Next(1000), random.Next(1000) }).ToArray();
            var sw = Stopwatch.StartNew();
            var dtw1 = DtwImplementations.ExactDtw(s1, s2, dist.Calculate);
            var t1 = sw.Elapsed;
            sw.Restart();
            var dtw2 = DtwImplementations.ExactDtwWikipedia(s1, s2, dist.Calculate);
            var t2 = sw.Elapsed;
            sw.Restart();
            var dtw3 = DtwImplementations.OptimizedDtw(s1, s2, dist.Calculate);
            var t3 = sw.Elapsed;
            sw.Restart();
            var dtw4 = DtwPy.Dtw(s1, s2, dist.Calculate);
            var t4 = sw.Elapsed;
            sw.Restart();


            Assert.AreEqual(dtw1, dtw2);
            Assert.AreEqual(dtw1, dtw3);
            Assert.AreEqual(dtw1, dtw4);

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
