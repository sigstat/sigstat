using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Algorithms
{
    /// <summary>
    /// Dynamic Time Warping algorithm
    /// </summary>
    public class DTW
    {
        private double[,] dMat;
        private double[,] wMat;

        private List<(int, int)> forwardPath;

        private Func<double[], double[], double> dist_method;

        public DTW()
        {
            dist_method = Accord.Math.Distance.Euclidean;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sequence1"></param>
        /// <param name="Sequence2"></param>
        /// <param name="DistanceMethod">Accord.Math.Distance.*</param>
        public DTW(Func<double[], double[], double> DistanceMethod)
        {
            dist_method = DistanceMethod;
        }

        /// <summary>
        /// Generate shortest path between the two sequences.
        /// </summary>
        /// <returns>List of index pairs, cost</returns>
        public (List<(int, int)>, double warpingDistance) Compute(double[][] s1, double[][] s2)
        {
            int n = s1.Length;
            int m = s2.Length;
            double dAcc = 0.0;
            dMat = new double[n, m];//distance matrix
            wMat = new double[n, m];//warp matrix

            int i = 0;
            int j = 0;
            //tavolsagok
            for (i = 0; i < n; i++)
                for (j = 0; j < m; j++)
                    dMat[i, j] = Distance(s1[i], s2[j]);

            //sarok
            wMat[0, 0] = dMat[0, 0];
            //két oldal
            for (i = 1; i < n; i++)
                wMat[i, 0] = dMat[i, 0] + wMat[i - 1, 0];
            for (j = 1; j < m; j++)
                wMat[0, j] = dMat[0, j] + wMat[0, j - 1];
            //tobbi resz
            for (i = 1; i < n; i++)
                for (j = 1; j < m; j++)
                {
                    dAcc = Math.Min(Math.Min(wMat[i - 1, j], wMat[i - 1, j - 1]), wMat[i, j - 1]);
                    dAcc += dMat[i, j];
                    wMat[i, j] = dAcc;
                }

            //legrovidebb ut megtalalas:
            int[,] warpingPath = new int[n + m, 2];//lepesek
            int K = 0;//K. lepes
            i = n - 1;//vegerol indulunk visszafele
            j = m - 1;
            warpingPath[0, 0] = i;//cel
            warpingPath[0, 1] = j;
            K++;
            while (i > 0 || j > 0)
            {
                if (i == 0)
                    j--;//bal szele
                else if (j == 0)
                    i--;//also szele
                else
                {
                    List<double> a = new List<double> { wMat[i - 1, j], wMat[i, j - 1], wMat[i - 1, j - 1] };
                    int minIndex = a.IndexOf(Math.Min(Math.Min(a[0], a[1]), a[2]));
                    //ez meg nem optimalis: ha tobb minimum van, akkor jobb, ha a diagonalis iranyt valasztjuk
                    if (a[minIndex] == a[2])
                        minIndex = 2;
                    if (minIndex == 0)
                        i -= 1;
                    else if (minIndex == 1)
                        j -= 1;
                    else if (minIndex == 2)
                    {
                        i -= 1;
                        j -= 1;
                    }
                }
                warpingPath[K, 0] = i;
                warpingPath[K, 1] = j;
                K++;
            }

            //Array.Reverse(warpingPath);//ez csak jagged[][] nel mukodik
            forwardPath = new List<(int, int)>(K);
            for (i = K - 1; i >= 0; i--)
                forwardPath.Add((warpingPath[i, 0], warpingPath[i, 1]));


            double cost = 0;

            double warpingDistance = dAcc / K;//gabor megoldasa, ez manhattan

            double[] bests1 = new double[K];
            double[] bests2 = new double[K];
            for (int istep = 0; istep < K; istep++)
            {
                (int s1i, int s2i) = forwardPath[istep];
                bests1[istep] = s1[s1i][0];
                bests2[istep] = s2[s2i][0];
            }
            cost = Distance(bests1, bests2);

            cost /= K; //ettol EER jobb lesz de AER rosszabb

            return (forwardPath, cost);
        }

        /// <summary>
        /// Mask of the shortest path
        /// </summary>
        /// <returns></returns>
        /*public bool[,] genPathArray()
        {
            if (forwardPath == null)
                Compute();

            bool[,] img = new bool[s1.Length, s2.Length];
            foreach ((int x,int y) step in forwardPath)
            {
                img[step.x, step.y] = true;
            }
            return img;
        }*/

        /// <summary>
        /// Calculate distance between two points.
        /// Distance method can be set in ctor.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private double Distance(double[] p1, double[] p2)
        {
            if (p1.Length == 1)//nem ter vissza az Accord, ha a pontok 1 dimenziosak, ezert ezt kulon kezeljuk
                return Math.Abs(p2[0] - p1[0]);

            double d = Accord.Math.Distance.GetDistance(dist_method).Distance(p1, p2);
            if (double.IsNaN(d))
                d = 0;//ez nehany metrikanal kell, pl Canberra 0,0 ban

            return d;
        }

    }
}
