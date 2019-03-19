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
    public static class DtwPy
    {
        /// <summary>
        /// Calculates the distance between two time sequences
        /// </summary>
        /// <typeparam name="P">the type of data points</typeparam>
        /// <param name="sequence1">time sequence 1</param>
        /// <param name="sequence2">time sequence 2</param>
        /// <param name="distance">a function to calculate the distance between two points</param>
        /// <returns></returns>
        public static double Dtw<P>(IEnumerable<P> sequence1, IEnumerable<P> sequence2, Func<P, P, double> distance)
        {
            // TODO: optimalizálás
            // - nem kellene a teljes NxM-es mátrixot létrehozni, kisebb memóriaigénnyel is menne
            // - inputot közvetlenül tömb/lista formátumban el lehetne fogadni, így nem kéne legyártani az indexelhető változatot belőle


            var vector1 = sequence1.ToArray();
            var vector2 = sequence2.ToArray();
            //N, M = A.shape[0], B.shape[0]	 #we assume that the sequences have the same number of x and y coordinates
            int n = vector1.Length;
            int m = vector2.Length;
            //DTW = float('inf') * np.ones((N + 1, M + 1))
            var dtw = new double[n + 1, m + 1];
            dtw.SetValues(double.MaxValue);
            //DTW[0, 0] = 0
            dtw[0, 0] = 0;

            for (int i = 1; i < n + 1; i++)
            {
                for (int j =1; j < m + 1; j++)
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
            return dtw[n-1, m-1];
        }


        /// <summary>
        /// Calculates the euclidean distance of two vectors
        /// </summary>
        /// <param name="vector1">vector1</param>
        /// <param name="vector2">vector2</param>
        /// <returns></returns>
        /// <remarks>The two vectors must have the same length</remarks>
        public static double EuclideanDistance(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException();
            }

            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                double dist = vector1[i] - vector2[i];
                sum += dist*dist;
            }
            return Math.Sqrt(sum);
        }
    }
}
