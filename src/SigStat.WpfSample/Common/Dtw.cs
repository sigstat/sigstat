using SigStat.Common;
using SigStat.WpfSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    //TODO: r megadása, külső használata, xml commentek
    public class Dtw
    {
        public Signature TestSignature { get; private set; }
        public Signature ReferenceSignature { get; private set; }
        public double[,] CostMatrix { get; set; }
        public List<Point> WarpingPath { get; set; }
        public List<Point> WarpingPathModified { get; set; }
        public int WindowSize { get; set; } = 11;

        int spacingParameterValue = Configuration.DefaultSpacingParameter;
        private bool isCostMatrixFilled;
        private bool isWarpingPathDefined;
        private bool isWarpingPathModifiedFound;

        private List<double[]> testSignatureAggregatedFeatures;
        private List<double[]> refSignatureAggregatedFeatures;

        List<FeatureDescriptor> inputFeatures;

        public Dtw(Signature testSignature, Signature referenceSignature)
        {
            TestSignature = testSignature;
            ReferenceSignature = referenceSignature;

            //verifierben már megtörténik, de ha más is használja, akkor ellenőrizni kell a származtatott dolgok meglétét!
            //DeriveFeaturesWithSpacingParameter(); 

            inputFeatures = Configuration.DefaultInputFeatures;

            testSignatureAggregatedFeatures = testSignature.GetAggregateFeature(inputFeatures);
            refSignatureAggregatedFeatures = referenceSignature.GetAggregateFeature(inputFeatures);

            CostMatrix = new double[testSignatureAggregatedFeatures.Count, refSignatureAggregatedFeatures.Count];
            isCostMatrixFilled = false;
            isWarpingPathDefined = false;

        }

        public Dtw(Signature ts, Signature rs, List<FeatureDescriptor> fs) : this(ts, rs)
        {
            inputFeatures = fs;

            testSignatureAggregatedFeatures = ts.GetAggregateFeature(inputFeatures);
            refSignatureAggregatedFeatures = rs.GetAggregateFeature(inputFeatures);
        }


        public double CalculateDtwScore()
        {
            if (!isCostMatrixFilled)
            {
                FillCostMatrix();
            }
            if (!isWarpingPathDefined)
            {
                FindWarpingPath();
            }

            int lwp = WarpingPath.Count;

            double retValue = CostMatrix[testSignatureAggregatedFeatures.Count - 1, refSignatureAggregatedFeatures.Count - 1] / lwp;
            return retValue;
        }

        public double CalculateWarpingPathScore()
        {
            return CalculateNormalizedAverageDistortion() + CalculateNormalizedAverageDisplacement();
        }


        public double CalculateNormalizedAverageDistortion()
        {
            if (!isWarpingPathDefined)
                FindWarpingPath();
            if (!isWarpingPathModifiedFound)
                FindWarpingPathModified(WindowSize);
            //throw new Exception("Modified warping path must be found before. Call FindWarpingPathModified with a chosen window size!");


            double averageDistortion = 0;

            for (int i = 0; i < WarpingPath.Count; i++)
            {
                int testIndex = WarpingPath[i].X;
                int referenceIndexInWPath = WarpingPath[i].Y;
                int referenceIndexInWPathModified = WarpingPathModified[i].Y;


                double distInWarpingPath = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[testIndex], refSignatureAggregatedFeatures[referenceIndexInWPath]);

                double distInWarpingPathModified = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[testIndex], refSignatureAggregatedFeatures[referenceIndexInWPathModified]);

                double normalizationFactor = GetMaxDistanceBetweenTestIndexAndReference(testIndex);

                averageDistortion += (Math.Abs(distInWarpingPath - distInWarpingPathModified) / normalizationFactor);
            }

            averageDistortion /= WarpingPath.Count;

            return averageDistortion;
        }

        public double CalculateNormalizedAverageDisplacement()
        {
            if (!isWarpingPathDefined)
                FindWarpingPath();
            if (!isWarpingPathModifiedFound)
                FindWarpingPathModified(WindowSize);
            //throw new Exception("Modified warping path must be found before. Call FindWarpingPathModified with a chosen window size!");

            double averageDisplacement = 0;
            double normalizationFactor = refSignatureAggregatedFeatures.Count;

            for (int i = 0; i < WarpingPath.Count; i++)
            {
                averageDisplacement += (Math.Abs(WarpingPath[i].Y - WarpingPathModified[i].Y) / normalizationFactor);
            }

            averageDisplacement /= WarpingPath.Count;

            return averageDisplacement;
        }

        public void FindWarpingPath()
        {
            if (isWarpingPathDefined)
            {
                return;
            }

            WarpingPath = new List<Point>(Math.Max(testSignatureAggregatedFeatures.Count, refSignatureAggregatedFeatures.Count));
            int i = testSignatureAggregatedFeatures.Count - 1;
            int j = refSignatureAggregatedFeatures.Count - 1;

            while (i > 0 && j > 0)
            {
                if (i == 0) { j = j - 1; }
                else if (j == 0) { i = i - 1; }
                else
                {
                    double minDist = MyMath.Min(CostMatrix[i, j - 1], CostMatrix[i - 1, j - 1], CostMatrix[i - 1, j]);

                    if (CostMatrix[i - 1, j] == minDist) { i = i - 1; }
                    else if (CostMatrix[i, j - 1] == minDist) { j = j - 1; }
                    else { i = i - 1; j = j - 1; }

                    WarpingPath.Add(new Point(i, j));
                }
            }


            isWarpingPathDefined = true;
        }

        public void FillCostMatrix()
        {

            for (int i = 0; i < testSignatureAggregatedFeatures.Count; i++)
            {
                for (int j = 0; j < refSignatureAggregatedFeatures.Count; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        //init [0,0]
                        CostMatrix[0, 0] = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[i], refSignatureAggregatedFeatures[j]);
                    }
                    else if (i == 0 && j > 0)
                    {
                        CostMatrix[i, j] = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[i], refSignatureAggregatedFeatures[j]) + CostMatrix[i, j - 1];
                    }
                    else if (j == 0 && i > 0)
                    {
                        CostMatrix[i, j] = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[i], refSignatureAggregatedFeatures[j]) + CostMatrix[i - 1, j];
                    }
                    else
                    {
                        double preDist = MyMath.Min(CostMatrix[i, j - 1], CostMatrix[i - 1, j - 1], CostMatrix[i - 1, j]);
                        CostMatrix[i, j] = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[i], refSignatureAggregatedFeatures[j]) + preDist;
                    }
                }
            }

            isCostMatrixFilled = true;
        }

        public void FindWarpingPathModified(int window)
        {
            if (window < 0)
                throw new Exception("Window size has to be non-negative");

            if (isWarpingPathModifiedFound) return;

            if (!isWarpingPathDefined)
                FindWarpingPath();

            WarpingPathModified = new List<Point>();

            for (int i = 0; i < WarpingPath.Count; i++)
            {
                int testIndex = WarpingPath[i].X;
                int refIndex = FindIndexOfClosestMatchInReferenceWithWindowSize(testIndex, window);
                WarpingPathModified.Add(new Point(testIndex, refIndex));
            }

            isWarpingPathModifiedFound = true;
        }

        private int FindIndexOfClosestMatchInReferenceWithWindowSize(int testIndex, int window)
        {

            if (testIndex < 0 || testIndex >= testSignatureAggregatedFeatures.Count)
                throw new Exception("Out of range index");

            int x;
            double minCenterDist = double.MaxValue;
            double minWindowSegmentDist = double.MaxValue;
            int minIndex = refSignatureAggregatedFeatures.Count;

            for (int i = 0; i < refSignatureAggregatedFeatures.Count; i++)
            {
                double windowSegmentDist = GetWindowSegmentDistance(window, testIndex, i);

                if (windowSegmentDist < minWindowSegmentDist)
                {
                    minWindowSegmentDist = windowSegmentDist;
                    minIndex = i;
                    minCenterDist = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[testIndex], refSignatureAggregatedFeatures[i]);
                }
                else if (windowSegmentDist == minWindowSegmentDist)
                {
                    double centerDist = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[testIndex], refSignatureAggregatedFeatures[i]);
                    if (centerDist < minCenterDist)
                    {
                        minWindowSegmentDist = windowSegmentDist;
                        minIndex = i;
                        minCenterDist = centerDist;
                    }
                }
                else
                    x = 0;
            }

            if (minIndex == refSignatureAggregatedFeatures.Count)
                throw new Exception("itt nem talált pontpárt");

            return minIndex;
        }

        private double GetWindowSegmentDistance(int window, int centerIndexTest, int centerIndexReference)
        {
            double dist = 0;
            for (int i = -window; i <= window; i++)
            {
                //Out of range indices, padding with repitions of the feature vectors of the first or last point
                int actualIndexTest = centerIndexTest + i;
                int actualIndexReference = centerIndexReference + i;
                if (actualIndexTest < 0) { actualIndexTest = 0; }
                if (actualIndexReference < 0) { actualIndexReference = 0; }
                if (actualIndexTest > testSignatureAggregatedFeatures.Count - 1) { actualIndexTest = testSignatureAggregatedFeatures.Count - 1; }
                if (actualIndexReference > refSignatureAggregatedFeatures.Count - 1) { actualIndexReference = refSignatureAggregatedFeatures.Count - 1; }

                dist += Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[actualIndexTest], refSignatureAggregatedFeatures[actualIndexReference]);
            }
            return dist;
        }

        private double GetMaxDistanceBetweenTestIndexAndReference(int testIndex)
        {
            if (testIndex < 0 || testIndex >= testSignatureAggregatedFeatures.Count)
                throw new Exception("Out of range index");

            double maxDist = -1;
            for (int i = 0; i < refSignatureAggregatedFeatures.Count; i++)
            {
                double dist = Accord.Math.Distance.Manhattan(testSignatureAggregatedFeatures[testIndex], refSignatureAggregatedFeatures[i]);

                if (dist > maxDist)
                {
                    maxDist = dist;
                }
            }

            return maxDist;
        }

        private void DeriveFeaturesWithSpacingParameter()
        {
            FeatureExtractor testSignatureExtractor = new FeatureExtractor(TestSignature, spacingParameterValue);
            TestSignature = testSignatureExtractor.GetAllDerivedSVC2004Features();

            FeatureExtractor refSignatureExtractor = new FeatureExtractor(ReferenceSignature, spacingParameterValue);
            ReferenceSignature = refSignatureExtractor.GetAllDerivedSVC2004Features();
        }

    }
}

