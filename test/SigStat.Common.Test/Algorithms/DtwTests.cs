using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common;
using SigStat.Common.Algorithms;
using System;
using System.Collections.Generic;

namespace SigStat.Common.Test.Algorithms
{
    [TestClass]
    public class DtwTests
    {

        [TestMethod]
        public void TestComputeTwoDimension()
        {

            double[][] signature1 = new double[3][];    
            
            //Two dimension points. another test required for one dimension points.
            signature1[0] = new double[] { 1, 4 };
            signature1[1] = new double[] { 2, 5 };
            signature1[2] = new double[] { 3, 8 };
            
            double[][] signature2 = new double[3][];
            signature2[0] = new double[] { 1, 5 };
            signature2[1]= new double[] { 2, 6 };
            signature2[2]= new double[] { 3, 8 };
         

            var dtw = new Dtw();
            double cost = dtw.Compute(signature1, signature2);
            double expectedCost = 0;
            Assert.AreEqual(expectedCost, cost);
        }
        [TestMethod]
        public void TestComputeOneDimension()
        {
            double[][] signature1 = new double[3][];

            //One dimension points.
            signature1[0] = new double[] { 4 };
            signature1[1] = new double[] { 5 };
            signature1[2] = new double[] { 8 };

            double[][] signature2 = new double[3][];
            signature2[0] = new double[] { 4 };
            signature2[1] = new double[] { 5 };
            signature2[2] = new double[] { 8 };


            var dtw = new Dtw();
            double cost = dtw.Compute(signature1, signature2);
            double expectedCost = 0;
            Assert.AreEqual(expectedCost, cost);
        }
        [TestMethod]
        public void TestDistanceTwoDimension()
        {
            double[] p1 = new double[2] { 1,2};
            double[] p2 = new double[2] { 1,4};
            double expectedDistance = 2;
            var dtw = new Dtw();
            double distance = dtw.Distance(p1, p2);
            Assert.AreEqual(expectedDistance, distance);
        }

        [TestMethod]
        public void TestDistanceOneDimension()
        {
            double[] p1 = new double[1] {  2 };
            double[] p2 = new double[1] {  4 };
            double expectedDistance = 2;
            var dtw = new Dtw();
            double distance = dtw.Distance(p1, p2);
            Assert.AreEqual(expectedDistance, distance);
        }

        [TestMethod]
        public void TestComputeDifferentLenghtInput() //SameLengthInput already tested in the previous tests.
        {

            double[][] signature1 = new double[3][]; 

            //Two dimension points. another test required for one dimension points.
            signature1[0] = new double[] { 1, 4 };
            signature1[1] = new double[] { 2, 5 };
            signature1[2] = new double[] { 3, 8 };

            double[][] signature2 = new double[4][];
            signature2[0] = new double[] { 1, 6 };
            signature2[1] = new double[] { 2, 7 };
            signature2[2] = new double[] { 3, 10 };
            signature2[3] = new double[] { 4, 12 };

            var dtw = new Dtw();
            double cost = dtw.Compute(signature1, signature2);
            double expectedCost = 0.25;
            double expectedDistance = 2;
            double distance = dtw.Distance(signature1[1], signature2[1]);
            Assert.AreEqual(expectedDistance, distance);
            Assert.AreEqual(expectedCost, cost);
        }

        [TestMethod]
        public void TestSameInput()
        {

            double[][] signature1 = new double[3][]; 

            //Two dimension points. another test required for one dimension points.
            signature1[0] = new double[] { 1, 4 };
            signature1[1] = new double[] { 2, 5 };
            signature1[2] = new double[] { 3, 8 };

            double[][] signature2 = new double[3][];
            signature2[0] = new double[] { 1, 4 };
            signature2[1] = new double[] { 2, 5 };
            signature2[2] = new double[] { 3, 8 };

            var dtw = new Dtw();
            double cost = dtw.Compute(signature1, signature2);
            double expectedCost = 0;
            double expectedDistance = 0;
            double distance = dtw.Distance(signature1[1], signature2[1]);
            Assert.AreEqual(expectedDistance, distance);
            Assert.AreEqual(expectedCost, cost);
        }
    }
}
