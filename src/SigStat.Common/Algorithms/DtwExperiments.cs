using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SigStat.Common;
namespace SigStat.Common.Algorithms
{
    /// <summary>
    /// A simple implementation of the DTW algorithm.
    /// </summary>
    public static class DtwExperiments
    {





        /// Calculate Dynamic Time Wrapping distance
        /// A,B: data and query, respectively
        /// cb : cummulative bound used for early abandoning
        /// r  : size of Sakoe-Chiba warpping band
        //public static double Dtw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance, int warpingWindowLentgh)

        /// <summary>
        /// Complex, optimized DTW calculation
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="sequence1"></param>
        /// <param name="sequence2"></param>
        /// <param name="distance"></param>
        /// <param name="m"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <remarks>Bases on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
        /// Performance from Dynamic Time Warping.KDD 2016: 2129-2130</remarks>
        public static double MyDtw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance, int m, int r)
        {
            var s1 = sequence1.ToArray();
            var s2 = sequence2.ToArray();

            double[] cost_tmp;
            int i, j, k;
            double x, y, z, min_cost;

            /// Instead of using matrix of size O(m^2) or O(mr), we will reuse two array of size O(r).
            var cost = new double[2 * r + 1];
            for (k = 0; k < cost.Length; k++) cost[k] = double.PositiveInfinity;

            var cost_prev = new double[2 * r + 1];
            for (k = 0; k < cost_prev.Length; k++) cost_prev[k] = double.PositiveInfinity;

            for (i = 0; i < m; i++)
            {
                k = Math.Max(0, r - i);
                min_cost = double.PositiveInfinity;

                for (j = Math.Max(0, i - r); j <= Math.Min(m - 1, i + r); j++, k++)
                {
                    // Initialize all row and column
                    if ((i == 0) && (j == 0))
                    {
                        cost[k] = distance(s1[0], s2[0]);
                        min_cost = cost[k];
                        continue;
                    }

                    if ((j - 1 < 0) || (k - 1 < 0)) y = double.PositiveInfinity;
                    else y = cost[k - 1];
                    if ((i - 1 < 0) || (k + 1 > 2 * r)) x = double.PositiveInfinity;
                    else x = cost_prev[k + 1];
                    if ((i - 1 < 0) || (j - 1 < 0)) z = double.PositiveInfinity;
                    else z = cost_prev[k];

                    // Classic DTW calculation
                    cost[k] = Math.Min(Math.Min(x, y), z) + distance(s1[i], s2[j]);

                    // Find minimum cost in row for early abandoning (possibly to use column instead of row).
                    if (cost[k] < min_cost)
                    {
                        min_cost = cost[k];
                    }
                }

                //// We can abandon early if the current cummulative distace with lower bound together are larger than bsf
                //if (i + r < m - 1 && min_cost + cb[i + r + 1] >= bsf)
                //{
                //    return min_cost + cb[i + r + 1];
                //}

                /// Move current array to previous array.
                cost_tmp = cost;
                cost = cost_prev;
                cost_prev = cost_tmp;
            }
            k--;

            // the DTW distance is in the last cell in the matrix of size O(m^2) or at the middle of our array.
            double final_dtw = cost_prev[k];
            return final_dtw;
        }


        /// <summary>
        /// Calculates the distance between two time sequences
        /// </summary>
        /// <typeparam name="P">the type of data points</typeparam>
        /// <param name="sequence1">time sequence 1</param>
        /// <param name="sequence2">time sequence 2</param>
        /// <param name="distance">a function to calculate the distance between two points</param>
        /// <returns></returns>
        public static double Dtw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance, int warpingWindowLentgh)
        {
            // TODO: optimalizálás
            // - nem kellene a teljes NxM-es mátrixot létrehozni, kisebb memóriaigénnyel is menne
            // - inputot közvetlenül tömb/lista formátumban el lehetne fogadni, így nem kéne legyártani az indexelhető változatot belőle


            var vector1 = sequence1.ToArray();
            var vector2 = sequence2.ToArray();
            //N, M = A.shape[0], B.shape[0]	 #we assume that the sequences have the same number of x and y coordinates
            int n = vector1.Length;
            int m = vector2.Length;
            int w = Math.Max(warpingWindowLentgh, Math.Abs(n - m));
            //DTW = float('inf') * np.ones((N + 1, M + 1))
            var dtw = new double[n + 1, m + 1];
            dtw.SetValues(double.MaxValue);
            //DTW[0, 0] = 0
            dtw[0, 0] = 0;

            for (int i = 1; i < n + 1; i++)
            {
                for (int j = Math.Max(1, i - w); j < Math.Min(m + 1, i + w); j++)
                {
                    //cost = _distance(A[i - 1], B[j - 1], mode)
                    var cost = distance(vector1[i - 1], vector2[j - 1]);
                    dtw[i, j] = cost + MathHelper.Min(
                        dtw[i - 1, j], // Insert 
                        dtw[i, j - 1], // Delete
                        dtw[i - 1, j - 1]); // Match
                }
            }
            //   for i in range(1, N + 1):
            //      for j in range(1, M + 1):
            //            cost = _distance(A[i - 1], B[j - 1], mode)
            //   DTW[i, j] = cost + min(DTW[i - 1, j],    # insertion
            //DTW[i, j - 1],    # deletion
            //                           DTW[i - 1, j - 1])    # match

            //return DTW[N, M]

            //TODO: could dtw[n,m] be the correct syntax here?
            return dtw[n - 1, m - 1];
        }


        public static double ExactDTw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance)
        {
            //Input: s1 and s2 are time series of length n and m
            //Output: DTW distance d between x and y
            //D(1:n + 1, 1:m + 1) = inf;
            //D(1, 1) = 0;
            //for i = 2 : n + 1 %for each row
            //  for j = 2 : m + 1 %for each column
            //    cost = (x(i - 1) - y(j - 1)) ^ 2;
            //    D(i, j) = cost + min( [D(i - 1, j), D(i, j - 1), D(i - 1, j - 1)]) ;
            //d = sqrt(D(n + 1, m + 1));

            // TODO: sqrt?
            var s1 = sequence1.ToArray();
            var s2 = sequence2.ToArray();
            var n = s1.Length;
            var m = s2.Length;


            var D = new double[n + 1, m + 1];
            D.SetValues(double.PositiveInfinity);
            D[1, 1] = 0;
            for (int i = 2; i <= n+1; i++)
            {
                for (int j = 2; j <= m + 1; m++)
                {
                    var cost = distance(s1[i - 1], s2[j - 1]);
                    D[i, j] = cost + Min(D[i - 1, j], D[i, j - 1], D[i - 1, j - 1]);
                }

            }
            return D[n + 1, m + 1];

        }

        private static double Min(double d1, double d2, double d3)
        {
            if (d3 > d2)
            {
                if (d2 > d1)
                    return d1;
                else
                    return d2;
            }
            else
            {
                if (d3 > d1)
                    return d1;
                else
                    return d3;
            }
        }


    }
}
