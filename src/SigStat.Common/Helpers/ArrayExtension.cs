using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common
{

    /// <summary>
    /// Helper methods for processing arrays
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Enumerates all values in a two dimensional array
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">The array to enumerate</param>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>(this T[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    yield return array[i, j];
                }
        }
      
        /// <summary>
        /// Sets all values in a two dimensional array to <paramref name="value"/>
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">Array</param>
        /// <param name="value">New value for the array elements</param>
        /// <returns>A reference to <paramref name="array"/> (allows chaining)</returns>
        public static T[,] SetValues<T>(this T[,] array, T value)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = value;
                }
            }
            return array;
        }

        /// <summary>
        /// Calculates the sum of the values in the given sub-array
        /// </summary>
        /// <param name="array">A two dimensional array with double values</param>
        /// <param name="x1">First index of the starting point for the region to summarize</param>
        /// <param name="y1">Second index of the starting point for the region to summarize</param>
        /// <param name="x2">First index of the endpoint for the region to summarize</param>
        /// <param name="y2">Second index of the endpoint for the region to summarize</param>
        /// <returns></returns>
        internal static double Sum(this double[,] array, int x1, int y1, int x2, int y2)
        {
            double sum = 0;
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    sum += array[x, y];
                }
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of column values in a two dimensional array
        /// </summary>
        /// <param name="array">A two dimensional array with double values</param>
        /// <param name="column">The column, to sum</param>
        /// <returns></returns>
        internal static double SumCol(this double[,] array, int column)
        {
            int height = array.GetLength(1);
            return Sum(array, column, 0, column, height - 1);
        }

        /// <summary>
        /// Returns the sum of row values in a two dimensional array
        /// </summary>
        /// <param name="array">A two dimensional array with double values</param>
        /// <param name="row">The row, to sum</param>
        internal static double SumRow(this double[,] array, int row)
        {
            int width = array.GetLength(0);
            return Sum(array, 0, row, width - 1, row);
        }


        /// <summary>
        /// Calculates the center of gravity, assuming that each cell contains
        /// a weight value
        /// </summary>
        /// <param name="weightMartix"></param>
        /// <returns></returns>
        public static (int x, int y) GetCog(this double[,] weightMartix)
        {
            //TODO: Implementation may be simplified
            // Search for k
            // k =  SUM(Fn*kn)/SUM(Fn)

            int width = weightMartix.GetLength(0);
            int height = weightMartix.GetLength(1);
            int resultX = width / 2;
            int resultY = height / 2; // tihs will bechanged

            // let's calculate ky
            double sumFk = 0;
            double sumF = 0;
            for (int y = 0; y < height; y++)
            {
                double sumTmp = 0;
                for (int x = 0; x < width; x++)
                {
                    sumTmp += weightMartix[x, y];
                }
                sumF += sumTmp;
                sumFk += sumTmp * y;
            }

            if (sumF > 0)
            {
                resultY = Convert.ToInt32(sumFk / sumF);
            }


            // let's calculate ky
            sumFk = 0;
            sumF = 0;
            for (int x = 0; x < width; x++)
            {
                double sumTmp = 0;
                for (int y = 0; y < height; y++)
                {
                    sumTmp += weightMartix[x, y];
                }
                sumF += sumTmp;
                sumFk += sumTmp * x;
            }

            if (sumF > 0)
            {
                resultX = Convert.ToInt32(sumFk / sumF);
            }

            return (resultX, resultY);
        }
    }
}
