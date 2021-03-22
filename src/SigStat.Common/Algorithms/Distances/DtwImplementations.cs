using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SigStat.Common;
using System.Runtime.CompilerServices;

namespace SigStat.Common.Algorithms
{
    /// <summary>
    /// A simple implementation of the DTW algorithm.
    /// </summary>
    public static class DtwImplementations
    {





        /// <summary>
        /// Complex, optimized DTW calculation (Abdullah Mueen, Eamonn J. Keogh)
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
        public static double OptimizedDtw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance, int m = 0, int r = 0)
        {
            var s1 = sequence1.ToArray();
            var s2 = sequence2.ToArray();
            if (m == 0)
                m = s1.Length;
            if (r == 0)
                r = s2.Length;

            double[] cost_tmp;
            int i, j, k;
            double x, y, z, min_cost;

            // Instead of using matrix of size O(m^2) or O(mr), we will reuse two array of size O(r).
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
                    cost[k] = Min(x, y, z) + distance(s1[i], s2[j]);

                    // Find minimum cost in row for early abandoning (possibly to use column instead of row).
                    if (cost[k] < min_cost)
                    {
                        min_cost = cost[k];
                    }
                }

         

                // Move current array to previous array.
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
        /// Exact DTW implementation (Abdullah Mueen, Eamonn J. Keogh)
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="sequence1">The sequence1.</param>
        /// <param name="sequence2">The sequence2.</param>
        /// <param name="distance">The distance.</param>
        /// <returns></returns>
        /// <remarks>Bases on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
        /// Performance from Dynamic Time Warping.KDD 2016: 2129-2130</remarks>
        public static double ExactDtw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance)
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
            var s1 = (new [] { default(P) }).Concat(sequence1).ToArray();
            var s2 = (new [] { default(P) }).Concat(sequence2).ToArray();
            var n = s1.Length - 1;
            var m = s2.Length - 1;


            var D = new double[n + 2, m + 2];
            D.SetValues(double.PositiveInfinity);
            D[1, 1] = 0;
            for (int i = 2; i <= n + 1; i++)
            {
                for (int j = 2; j <= m + 1; j++)
                {
                    var cost = distance(s1[i - 1], s2[j - 1]);
                    D[i, j] = cost + Min(D[i - 1, j], D[i, j - 1], D[i - 1, j - 1]);
                }

            }
            return D[n + 1, m + 1];

        }

        /// <summary>
        /// Constrained DTW implementation  (Abdullah Mueen, Eamonn J. Keogh)
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="sequence1">The sequence1.</param>
        /// <param name="sequence2">The sequence2.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="w">The w.</param>
        /// <returns></returns>
        /// <remarks>Bases on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
        /// Performance from Dynamic Time Warping.KDD 2016: 2129-2130</remarks>
        public static double ConstrainedDTw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance, int w)
        {
            //D(1:n + 1, 1:m + 1) = inf;
            //D(1, 1) = 0;
            //w = max(w, abs(n - m));
            //for i = 2 : n + 1
            //for j = max(2, i - w) : min(m + 1, i + w)
            //cost = (x(i - 1) - y(j - 1)) ^ 2;
            //D(i, j) = cost + min( [D(i - 1, j), D(i, j - 1), D(i - 1, j - 1)]) ;
            //d = sqrt(D(n + 1, m + 1));

            // TODO: sqrt?
            var s1 = (new [] { default(P) }).Concat(sequence1).ToArray();
            var s2 = (new [] { default(P) }).Concat(sequence2).ToArray();
            var n = s1.Length - 1;
            var m = s2.Length - 1;


            var D = new double[n + 2, m + 2];
            D.SetValues(double.PositiveInfinity);
            D[1, 1] = 0;
            w = Math.Max(w, Math.Abs(n - m));
            for (int i = 2; i <= n + 1; i++)
            {
                for (int j = Math.Max(2, i - w); j <= Math.Min(m + 1, i + w); m++)
                {
                    var cost = distance(s1[i - 1], s2[j - 1]);
                    D[i, j] = cost + Min(D[i - 1, j], D[i, j - 1], D[i - 1, j - 1]);
                }

            }
            return D[n + 1, m + 1];

        }


        /// <summary>
        /// Exact DTW implementation (Wikipedia)
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="sequence1">The sequence1.</param>
        /// <param name="sequence2">The sequence2.</param>
        /// <param name="distance">The distance.</param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Dynamic_time_warping</remarks>
        public static double ExactDtwWikipedia<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance)
        {
            //int DTWDistance(s: array[1..n], t: array[1..m]) {
            //DTW:= array[0..n, 0..m]
            //   for i := 1 to n
            //     for j := 1 to m
            //       DTW[i, j] := infinity
            //   DTW[0, 0] := 0

            //   for i := 1 to n
            //       for j := 1 to m
            //           cost := d(s[i], t[j])
            //           DTW[i, j] := cost + minimum(DTW[i - 1, j],    // insertion
            //                                       DTW[i, j - 1],    // deletion
            //                                       DTW[i - 1, j - 1])    // match
            //                    return DTW[n, m]

            // Indexing starts from 1
            var s1 = (new [] { default(P) }).Concat(sequence1).ToArray();
            var s2 = (new [] { default(P) }).Concat(sequence2).ToArray();
            var n = s1.Length - 1;
            var m = s2.Length - 1;
            var dtw = new double[n+1, m+1];
            dtw.SetValues(Double.PositiveInfinity);
            dtw[0, 0] = 0;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    var cost = distance(s1[i], s2[j]);
                    dtw[i, j] = cost + Min(dtw[i - 1, j], dtw[i, j - 1], dtw[i - 1, j - 1]);
                }
            return dtw[n, m];

        }

        /// <summary>
        /// Constrained DTW implementation  (Wikipedia)
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="sequence1">The sequence1.</param>
        /// <param name="sequence2">The sequence2.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="w">The w.</param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Dynamic_time_warping</remarks>
        public static double ConstrainedDtwWikipedia<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance, int w)
        {
            //int DTWDistance(s: array[1..n], t: array[1..m], w: int) {
            //DTW:= array[0..n, 0..m]
          
            //w:= max(w, abs(n - m)) // adapt window size (*)

            //for i := 0 to n
            //    for j:= 0 to m
            //        DTW[i, j] := infinity
            //DTW[0, 0] := 0
            //for i := 1 to n
            //    for j := max(1, i - w) to min(m, i + w)
            //        DTW[i, j] := 0

            //for i := 1 to n
            //    for j := max(1, i - w) to min(m, i + w)
            //        cost := d(s[i], t[j])
            //        DTW[i, j] := cost + minimum(DTW[i - 1, j],    // insertion
            //                                    DTW[i, j - 1],    // deletion
            //                                    DTW[i - 1, j - 1])    // match

            //return DTW[n, m]
          
            // Indexing starts from 1
            var s1 = (new [] { default(P) }).Concat(sequence1).ToArray();
            var s2 = (new [] { default(P) }).Concat(sequence2).ToArray();
            var n = s1.Length - 1;
            var m = s2.Length - 1;

            w = Math.Max(w, Math.Abs(n - m)); // adapt window size (*)
            var dtw = new double[n+1, m+1];
            dtw.SetValues(Double.PositiveInfinity);
            dtw[0, 0] = 0;


            for (int i = 1; i <= n; i++)
                for (int j = Math.Max(1, i - w); i<= Math.Min(m, i + w);j++)
            {
                    dtw[i, j] = 0;
            }

            //TODO: https://app.codacy.com/gh/sigstat/sigstat/file/54164406687/issues/source?bid=22371483&fileBranchId=22371483#l281
            for (int i = 1; i <= n; i++)
                for (int j = Math.Max(1, i - w); j <= Math.Min(m, i + w); j++)
                {
                    var cost = distance(s1[i], s2[j]);
                    dtw[i, j] = cost + Min(dtw[i - 1, j], dtw[i, j - 1], dtw[i - 1, j - 1]);
                }
            return dtw[n, m];

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Min(double d1, double d2, double d3)
        {
            double d12 = d1 > d2 ? d2 : d1;
            return d12 > d3 ? d3 : d12;
        }


    }
}
