//using Alairas.WpfTemalabor.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Alairas.WpfTemalabor.DTW
//{
//    //TODO: r megadása, külső használata
//    public class Dtw
//    {
//        public SignatureData TestSignature { get; }
//        public SignatureData ReferenceSignature { get; }
//        public double[,] CostMatrix { get; set; }
//        public List<Point> WarpingPath { get; set; }
//        public List<Point> WarpingPathModified { get; set; }
//        public int WindowSize { get; set; } = 11;

//        int spacingParameterValue = Configuration.DefaultSpacingParameterValue;
//        private int usedLengthOfTestSignature;
//        private int usedLengthOfRefSignature;
//        private bool isCostMatrixFilled;
//        private bool isWarpingPathDefined;
//        private bool isWarpingPathModifiedFound;

//        Features featureFilter = Configuration.DefaultFeatureFilter;

//        public Dtw(SignatureData testSignature, SignatureData referenceSignature)
//        {
//            TestSignature = testSignature;
//            ReferenceSignature = referenceSignature;

//            //because the derived features are shorter than original feautures
//            // '-r' --> first order differences vector length
//            // '-1' --> second order differences vector length
//            usedLengthOfTestSignature = TestSignature.NumOfPoints - spacingParameterValue - 1;
//            usedLengthOfRefSignature = ReferenceSignature.NumOfPoints - spacingParameterValue - 1;

//            CostMatrix = new double[usedLengthOfTestSignature, usedLengthOfRefSignature];
//            isCostMatrixFilled = false;
//            isWarpingPathDefined = false;

//            featureFilter = Configuration.DefaultFeatureFilter;

//            DeriveFeaturesWithSpacingParameter();
//        }

//        public Dtw(SignatureData ts, SignatureData rs, Features ff) : this(ts, rs)
//        {
//            featureFilter = ff;
//        }


//        public double CalculateDtwScore()
//        {
//            if (!isCostMatrixFilled)
//            {
//                FillCostMatrix();
//            }
//            if (!isWarpingPathDefined)
//            {
//                FindWarpingPath();
//            }

//            int lwp = WarpingPath.Count;

//            return CostMatrix[usedLengthOfTestSignature - 1, usedLengthOfRefSignature - 1] / lwp;
//        }

//        public double CalculateWarpingPathScore()
//        {
//            return CalculateNormalizedAverageDistortion() + CalculateNormalizedAverageDisplacement();
//        }

//        public double CalculateNormalizedAverageDistortion()
//        {
//            if (!isWarpingPathDefined)
//                FindWarpingPath();
//            if (!isWarpingPathModifiedFound)
//                FindWarpingPathModified(WindowSize);
//            //throw new Exception("Modified warping path must be found before. Call FindWarpingPathModified with a chosen window size!");


//            double averageDistortion = 0;

//            for (int i = 0; i < WarpingPath.Count; i++)
//            {
//                int testIndex = WarpingPath[i].X;
//                int referenceIndexInWPath = WarpingPath[i].Y;
//                int referenceIndexInWPathModified = WarpingPathModified[i].Y;

//                double distInWarpingPath = Analyzer.GetManhattanDistance(
//                    TestSignature.GetFeatures(testIndex, featureFilter),
//                    ReferenceSignature.GetFeatures(referenceIndexInWPath, featureFilter));

//                double distInWarpingPathModified = Analyzer.GetManhattanDistance(
//                    TestSignature.GetFeatures(testIndex, featureFilter),
//                    ReferenceSignature.GetFeatures(referenceIndexInWPathModified, featureFilter));

//                double normalizationFactor = GetMaxDistanceBetweenTestIndexAndReference(testIndex);

//                averageDistortion += (Math.Abs(distInWarpingPath - distInWarpingPathModified) / normalizationFactor);
//            }

//            averageDistortion /= WarpingPath.Count;

//            return averageDistortion;
//        }

//        public double CalculateNormalizedAverageDisplacement()
//        {
//            if (!isWarpingPathDefined)
//                FindWarpingPath();
//            if (!isWarpingPathModifiedFound)
//                FindWarpingPathModified(WindowSize);
//            //throw new Exception("Modified warping path must be found before. Call FindWarpingPathModified with a chosen window size!");

//            double averageDisplacement = 0;
//            double normalizationFactor = usedLengthOfRefSignature;

//            for (int i = 0; i < WarpingPath.Count; i++)
//            {
//                averageDisplacement += (Math.Abs(WarpingPath[i].Y - WarpingPathModified[i].Y) / normalizationFactor);
//            }

//            averageDisplacement /= WarpingPath.Count;

//            return averageDisplacement;
//        }

//        public void FindWarpingPath()
//        {
//            if (isWarpingPathDefined)
//            {
//                return;
//            }

//            WarpingPath = new List<Point>(Math.Max(usedLengthOfTestSignature, usedLengthOfRefSignature));
//            int i = usedLengthOfTestSignature - 1;
//            int j = usedLengthOfRefSignature - 1;

//            while (i > 0 && j > 0)
//            {
//                if (i == 0) { j = j - 1; }
//                else if (j == 0) { i = i - 1; }
//                else
//                {
//                    double minDist = Analyzer.Min( CostMatrix[i, j - 1], CostMatrix[i - 1, j - 1], CostMatrix[i - 1, j]);

//                    if (CostMatrix[i - 1, j] == minDist) { i = i - 1; }
//                    else if (CostMatrix[i, j - 1] == minDist) { j = j - 1; }
//                    else { i = i - 1; j = j - 1; }

//                    WarpingPath.Add(new Point(i, j));
//                }
//            }


//            isWarpingPathDefined = true;
//        }

//        public void FillCostMatrix()
//        {

//            for (int i = 0; i < usedLengthOfTestSignature; i++)
//            {
//                for (int j = 0; j < usedLengthOfRefSignature; j++)
//                {
//                    if (i == 0 && j == 0)
//                    {
//                        //init [0,0]
//                        CostMatrix[0, 0] = Analyzer.GetManhattanDistance( 
//                            TestSignature.GetFeatures(i, featureFilter),
//                            ReferenceSignature.GetFeatures(j, featureFilter));
//                    }
//                    else if (i == 0 && j > 0)
//                    {
//                        CostMatrix[i, j] = Analyzer.GetManhattanDistance(
//                            TestSignature.GetFeatures(i, featureFilter),
//                            ReferenceSignature.GetFeatures(j, featureFilter))
//                            + CostMatrix[i, j - 1];

//                    }
//                    else if (j == 0 && i > 0)
//                    {
//                        CostMatrix[i, j] = Analyzer.GetManhattanDistance(
//                            TestSignature.GetFeatures(i, featureFilter),
//                            ReferenceSignature.GetFeatures(j, featureFilter))
//                           + CostMatrix[i - 1, j];
//                    }
//                    else
//                    {
//                        double preDist =  Analyzer.Min(CostMatrix[i, j - 1], CostMatrix[i - 1, j - 1], CostMatrix[i - 1, j]);
//                        CostMatrix[i, j] = Analyzer.GetManhattanDistance(
//                            TestSignature.GetFeatures(i, featureFilter),
//                            ReferenceSignature.GetFeatures(j, featureFilter))
//                            + preDist;
//                    }
//                }
//            }

//            isCostMatrixFilled = true;
//        }

//        public void FindWarpingPathModified(int window)
//        {
//            if (window < 0)
//                throw new Exception("Window size has to be non-negative");

//            if (isWarpingPathModifiedFound) return;

//            if (!isWarpingPathDefined)
//                FindWarpingPath();

//            WarpingPathModified = new List<Point>();

//            for (int i = 0; i < WarpingPath.Count; i++)
//            {
//                int testIndex = WarpingPath[i].X;
//                int refIndex = FindIndexOfClosestMatchInReferenceWithWindowSize(testIndex, window);
//                WarpingPathModified.Add(new Point(testIndex, refIndex));
//            }

//            isWarpingPathModifiedFound = true;
//        }

//        private int FindIndexOfClosestMatchInReferenceWithWindowSize(int testIndex, int window)
//        {

//            if (testIndex < 0 || testIndex >= usedLengthOfTestSignature)
//                throw new Exception("Out of range index");

//            int x;
//            double minCenterDist = double.MaxValue;
//            double minWindowSegmentDist = double.MaxValue;
//            int minIndex = usedLengthOfRefSignature;

//            for (int i = 0; i < usedLengthOfRefSignature; i++)
//            {
//                double windowSegmentDist = GetWindowSegmentDistance(window, testIndex, i);

//                if (windowSegmentDist < minWindowSegmentDist)
//                {
//                    minWindowSegmentDist = windowSegmentDist;
//                    minIndex = i;
//                    minCenterDist = Analyzer.GetManhattanDistance(
//                        TestSignature.GetFeatures(testIndex, featureFilter),
//                        ReferenceSignature.GetFeatures(i, featureFilter));
//                }
//                else if (windowSegmentDist == minWindowSegmentDist)
//                {
//                    double centerDist = Analyzer.GetManhattanDistance(
//                        TestSignature.GetFeatures(testIndex, featureFilter),
//                        ReferenceSignature.GetFeatures(i, featureFilter));
//                    if (centerDist < minCenterDist)
//                    {
//                        minWindowSegmentDist = windowSegmentDist;
//                        minIndex = i;
//                        minCenterDist = centerDist;
//                    }
//                }
//                else
//                    x = 0;
//            }

//            if (minIndex == usedLengthOfRefSignature)
//                throw new Exception("itt nem talált pontpárt");

//            return minIndex;
//        }

//        private double GetWindowSegmentDistance(int window, int centerIndexTest, int centerIndexReference)
//        {
//            double dist = 0;
//            for (int i = -window; i <= window; i++)
//            {
//                //Out of range indices, padding with repitions of the feature vectors of the first or last point
//                int actualIndexTest = centerIndexTest + i;
//                int actualIndexReference = centerIndexReference + i;
//                if (actualIndexTest < 0) { actualIndexTest = 0; }
//                if (actualIndexReference < 0) { actualIndexReference = 0; }
//                if (actualIndexTest > usedLengthOfTestSignature - 1) { actualIndexTest = usedLengthOfTestSignature - 1; }
//                if (actualIndexReference > usedLengthOfRefSignature - 1) { actualIndexReference = usedLengthOfRefSignature - 1; }

//                dist += Analyzer.GetManhattanDistance(
//                    TestSignature.GetFeatures(actualIndexTest, featureFilter),
//                    ReferenceSignature.GetFeatures(actualIndexReference, featureFilter));
//            }
//            return dist;
//        }

//        private double GetMaxDistanceBetweenTestIndexAndReference(int testIndex)
//        {
//            if (testIndex < 0 || testIndex >= usedLengthOfTestSignature)
//                throw new Exception("Out of range index");

//            double maxDist = -1;
//            for (int i = 0; i < usedLengthOfRefSignature; i++)
//            {
//                double dist = Analyzer.GetManhattanDistance(
//                    TestSignature.GetFeatures(testIndex, featureFilter),
//                    ReferenceSignature.GetFeatures(i, featureFilter));

//                if (dist > maxDist)
//                {
//                    maxDist = dist;
//                }
//            }

//            return maxDist;
//        }

//        private void DeriveFeaturesWithSpacingParameter()
//        {
//            TestSignature.DeriveFeatures(spacingParameterValue);
//            ReferenceSignature.DeriveFeatures(spacingParameterValue);
//        }

//    }
//}
