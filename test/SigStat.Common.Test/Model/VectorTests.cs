//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Text;
//using static SigStat.Common.Vector;

//namespace SigStat.Common.Test.Model
//{
//    [TestClass]
//    public class VectorTests
//    {
//        [TestClass]
//        public class VectorPointTests
//        {
//            [TestMethod]
//            public void TestConstructors()
//            {
//                Vector v = new Vector();
//                VectorPoint Vp = new VectorPoint(v, VectorPointKind.Start);
//            }

//            [TestMethod]
//            public void TestGetterSetters()
//            {
//                Vector v = new Vector();
//                VectorPoint Vp = new VectorPoint(v, VectorPointKind.Start);
//                Vp.X = 2;
//                Vp.Y = 2;
//                Assert.AreEqual(2, Vp.X);
//                Assert.AreEqual(2, Vp.Y);

//                Vector v2 = new Vector();
//                VectorPoint Vp2 = new VectorPoint(v, VectorPointKind.End);
//                Vp2.X = 2;
//                Vp2.Y = 2;
//                Assert.AreEqual(2, Vp2.X);
//                Assert.AreEqual(2, Vp2.Y);
//            }

//            [TestMethod]
//            public void TestToPoint()
//            {
//                Vector v = new Vector(1,2,3,4);
//                VectorPoint Vp = new VectorPoint(v, VectorPointKind.Start);
//                Point expectedP = new Point(1, 2);
//                Assert.AreEqual(expectedP, Vp.ToPoint());
//            }
//        }

//        [TestMethod]
//        public void TestConstructors()
//        {
//            Vector vector = new Vector();
//            Assert.AreEqual(0, vector.X);
//            Assert.AreEqual(0, vector.Y);
//            Assert.AreEqual(0, vector.Vx);
//            Assert.AreEqual(0, vector.Vy);

//            Vector vector2 = new Vector(3, 2, 4, 5);
//            Assert.AreEqual(3, vector2.X);
//            Assert.AreEqual(2, vector2.Y);
//            Assert.AreEqual(4, vector2.Vx);
//            Assert.AreEqual(5, vector2.Vy);

//            Vector vector3 = new Vector(4, 5);
//            Assert.AreEqual(0, vector3.X);
//            Assert.AreEqual(0, vector3.Y);
//            Assert.AreEqual(4, vector3.Vx);
//            Assert.AreEqual(5, vector3.Vy);

//            Point p1 = new Point(3, 2);
//            Point p2 = new Point(4, 5);
//            Vector vector4 = new Vector(p1, p2);
//            Assert.AreEqual(p1.X, vector4.X);
//            Assert.AreEqual(p1.Y, vector4.Y);
//            Assert.AreEqual(p2.X - p1.X, vector4.Vx);
//            Assert.AreEqual(p2.Y - p1.Y, vector4.Vy);
//        }

//        [TestMethod]
//        public void TestGetterSetters()
//        {
//            Vector vector = new Vector();
//            vector.X = 2;
//            vector.Y = 2;
//            Assert.AreEqual(2, vector.X);
//            Assert.AreEqual(2, vector.Y);

//            vector.Vx = 1;
//            vector.Vy = 1;

//            Assert.AreEqual(1, vector.Vx);
//            Assert.AreEqual(1, vector.Vy);

//            vector.X2 = 3;
//            vector.Y2 = 3;

//            Assert.AreEqual(3, vector.X2);
//            Assert.AreEqual(3, vector.Y2);
//        }

//        [TestMethod]
//        public void TestPoints()
//        {
//            Vector vector = new Vector();
//            vector.X = 5;
//            vector.Y = 5;

//            Point expectedStart = new Point(5, 5);
//            Assert.AreEqual(expectedStart, vector.Start);

//            vector.X2 = 3;
//            vector.Y2 = 3;
//            Point expectedEnd = new Point(3, 3);
//            Assert.AreEqual(expectedEnd, vector.End);
//        }

//        [TestMethod]
//        public void TestToString()
//        {
//            Vector vector = new Vector(1, 2, 3, 4);
//            Assert.AreEqual("Location (1,2), Direction (3,4)", vector.ToString());
//        }

//        [TestMethod]
//        public void TestEquals()
//        {
//            Vector vector = new Vector();
//            Vector vector2 = new Vector(3, 2, 4, 5);
//            Vector vector3 = new Vector(3, 2, 4, 5);
//            Vector vector4 = new Vector(3, 2, 2, 5);
//            Vector vector5 = new Vector(3, 3, 2, 5);
//            Vector vector6 = new Vector(3, 2, 4, 6);
//            int testint = 0;

//            Assert.IsFalse(vector2.Equals(vector));
//            Assert.IsTrue(vector2.Equals(vector3));
//            Assert.IsFalse(vector2.Equals(vector4));
//            Assert.IsFalse(vector2.Equals(null));
//            Assert.IsFalse(vector2.Equals(vector5));
//            Assert.IsFalse(vector2.Equals(vector6));
//            Assert.IsFalse(vector2.Equals(testint));
//        }

//        [TestMethod]
//        public void TestVectorOperators()
//        {
//            Vector vector = new Vector(3, 2, 4, 5);
//            Vector vector2 = new Vector(3, 2, 4, 5);
//            Vector vector3 = new Vector(3, 2, 2, 5);
//            Vector vector4 = null;

//            Assert.IsTrue(vector == vector);
//            Assert.IsTrue(vector4 == null);
//            Assert.IsTrue(vector == vector2);
//            Assert.IsFalse(vector == vector3);

//            Assert.IsFalse(vector != vector);
//            Assert.IsFalse(vector4 != null);
//            Assert.IsFalse(vector != vector2);
//            Assert.IsTrue(vector != vector3);
//        }

//        [TestMethod]
//        public void TestPointOperators()
//        {
//            Point p1 = new Point(3, 3);
//            Vector v = new Vector(1, 1, 2, 3);

//            Point expectedSum = new Point(5, 6);
//            Assert.AreEqual(expectedSum, p1 + v);

//            Point expectedDifference = new Point(1, 0);
//            Assert.AreEqual(expectedDifference, p1 - v);
//        }
//        [TestMethod]
//        public void TestGetOtherProperties()
//        {
//            Vector v = new Vector(1, 2, 3, 4);
//            Vector v2 = new Vector(1, 1, 5, 0);
//            Vector v3 = new Vector(1, 1, 0, 5);
//            Vector v4 = new Vector(1, 1, 0, -5);

//            Assert.IsInstanceOfType(v.GetHashCode(), typeof(int));
//            Assert.AreEqual(5, v.GetLength());
//            Assert.AreEqual(Math.Atan2(3, 4), v.Angle);
//            Assert.AreEqual(5, v.Length);
//            Assert.AreEqual((double)4 / (double)3, v.M);
//            Assert.AreEqual((double)2 - (double)4 / (double)3 * 1, v.B);

//            Vector expectedNormal = new Vector(1, 2, 4, -3);
//            Assert.AreEqual(expectedNormal, v.GetNormal());

//            Vector expectedNormalLenght = new Vector(1, 2, 8, -6);
//            Assert.AreEqual(expectedNormalLenght, v.GetNormal(10));

//            v.Length = 10;
//            Vector expectedV = new Vector(1, 2, 6, 8);
//            Assert.IsTrue(expectedV == v);

//            v2.Length = 10;
//            expectedV = new Vector(1, 1, 10, 0);
//            Assert.IsTrue(expectedV == v2);

//            v3.Length = 10;
//            expectedV = new Vector(1, 1, 0, 10);
//            Assert.IsTrue(expectedV == v3);

//            v4.Length = 10;
//            expectedV = new Vector(1, 1, 0, -10);
//            Assert.IsTrue(expectedV == v4);

//        }

//        [TestMethod]
//        public void TestMathlabString()
//        {
//            Vector v = new Vector(1, 2, 3, 4);
//            Assert.AreEqual("[1,2,3,4]", v.ToMatlabString());
//        }

//        [TestMethod]
//        public void TestBoundCog()
//        {
//            Vector v = new Vector(1, 2, 3, 4);

//            Rectangle expectedR = new Rectangle(1, 2, 3, 4);
//            Assert.AreEqual(expectedR, v.Bounds);
//            Assert.AreEqual(expectedR, v.BoundingRectangle);

//            Point expectedCOG = new Point(2, 4);
//            Assert.AreEqual(expectedCOG, v.COG);
//            Assert.AreEqual(expectedCOG, v.Location);
//        }

//        [TestMethod]
//        public void TestAdd()
//        {
//            Vector v = new Vector(1, 2, 3, 4);
//            v.Add(new Point(2, 2));
//            v.Add(new Point(5, 5));
//            Vector expectedV = new Vector(2, 2, 3, 3);
//            Assert.AreEqual(expectedV, v);
//            Assert.ThrowsException<InvalidOperationException>(() => v.Add(new Point(2, 3)));
//        }

//        [TestMethod]
//        public void TestEnumerator()
//        {
//            //TODO:Ezt nem sikerült megcsinálni
//           /* Vector v = new Vector(1, 2, 3, 4);
//            VectorPoint expectedS = new VectorPoint(new Vector(1, 2, 3, 4), VectorPointKind.Start);
//            VectorPoint expectedE = new VectorPoint(new Vector(1, 2, 3, 4), VectorPointKind.End);
//            Assert.AreEqual(expectedS, v.GetEnumerator());*/
            
//        }

//        [TestMethod]
//        public void TestClone()
//        {
//            Vector v = new Vector(1, 2, 3, 4);
//            Vector v2 = v.Clone();

//            Assert.AreEqual(v2, v);

//        }
//    }
//}
