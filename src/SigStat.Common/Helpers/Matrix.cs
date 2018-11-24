using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Data;

namespace SigStat.Common
{
    public delegate E ItemEvaluator<E, T>(T item);

    public static class Matrix
    {
        /// <summary>
        /// returns a copy of the array with inverted values
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool[,] Invert(this bool[,] array)
        {
            bool[,] result = new bool[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i, j] = !array[i, j];
                }
            }

            return result;

        }

        public static T[] SetValues<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array;
        }

        public static T[] SetValues<T>(this T[] array, Func<T,T> transformation)
        {
            if (array == null)
            {
                return null;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = transformation(array[i]);
            }
            return array;
        }

        /// <summary>
        /// returns a same sized matrix with each item showing the neighbour count for the given position.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static byte[,] Neighbours<T>(T[,] matrix, T emptyValue)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            byte[,] result = new byte[width, height];
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    byte n = 0;
                    if (x > 0 && y > 0 && !matrix[x - 1, y - 1].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (y > 0 && !matrix[x - 0, y - 1].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (x < width - 1 && y > 0 && !matrix[x + 1, y - 1].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (x > 0 && !matrix[x - 1, y - 0].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (x < width - 1 && !matrix[x + 1, y - 0].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (x > 0 && y < height - 1 && !matrix[x - 1, y + 1].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (y < height - 1 && !matrix[x - 0, y + 1].Equals(emptyValue))
                    {
                        n++;
                    }

                    if (x < width - 1 && y < height - 1 && !matrix[x + 1, y + 1].Equals(emptyValue))
                    {
                        n++;
                    }

                    result[x, y] = n;
                }
            }

            return result;


        }

        public static IEnumerable<Point> GetNeighbourPixels(this Point p)
        {
            yield return new Point(p.X - 0, p.Y - 1);
            yield return new Point(p.X + 1, p.Y - 1);
            yield return new Point(p.X + 1, p.Y + 0);
            yield return new Point(p.X + 1, p.Y + 1);
            yield return new Point(p.X + 0, p.Y + 1);
            yield return new Point(p.X - 1, p.Y + 1);
            yield return new Point(p.X - 1, p.Y - 0);
            yield return new Point(p.X - 1, p.Y - 1);
        }
        public static IEnumerable<Point> GetNeighbours(this Point p, Point start, int offset)
        {
            // "12 órától" indulva legeneráljuk az eredményt.
            Point[] result = new[]
                {
                    new Point(p.X - 0, p.Y - 1),
                    new Point(p.X + 1, p.Y - 1),
                    new Point(p.X + 1, p.Y + 0),
                    new Point(p.X + 1, p.Y + 1),
                    new Point(p.X + 0, p.Y + 1),
                    new Point(p.X - 1, p.Y + 1),
                    new Point(p.X - 1, p.Y - 0),
                    new Point(p.X - 1, p.Y - 1),
                };

            var i = (result.IndexOf(start)+ offset) % 8;
            if (i == -1)
            {
                throw new ArgumentException("Invalid start point");
            }

            if (i == 0)
            {
                return result;
            }
            else
            {
                return result.Skip(i).Concat(result.Take(i));
            }
        }



        public static Point GetCog(double[,] weightMartix)
        {
            // Search for k
            // k =  SUM(Fn*kn)/SUM(Fn)

            int width = weightMartix.GetLength(0);
            int height = weightMartix.GetLength(1);
            Point result = new Point(width / 2, height / 2); // tihs will bechanged

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
                result.Y = Convert.ToInt32(sumFk / sumF);
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
                result.X = Convert.ToInt32(sumFk / sumF);
            }

            return result;
        }

        public static E[,] Evaluate<E, T>(T[,] matrix, ItemEvaluator<E, T> evaluator)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            E[,] result = new E[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[x, y] = evaluator(matrix[x, y]);
                }
            }

            return result;
        }

        internal static double GetSum(double[,] matrix, int x1, int y1, int x2, int y2)
        {
            double sum = 0;
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    sum += matrix[x, y];
                }
            }

            return sum;
        }

        internal static double GetSumCol(double[,] matrix, int col)
        {
            int height = matrix.GetLength(1);
            return GetSum(matrix, col, 0, col, height - 1);
        }

        internal static double GetSumRow(double[,] matrix, int row)
        {
            int width = matrix.GetLength(0);
            return GetSum(matrix, 0, row, width - 1, row);
        }

        /// <summary>
        /// Egy DataRow gyüjteményt átalakít egy kétdimenziós tömbbé. 
        /// Az átalakítás során ignoreColumns oszlopot és ignoreRows sort
        /// figyelmen kívül hagy.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        public static T[,] FromTableRows<T>(IEnumerable<DataRow> rows, int ignoreColumns, int ignoreRows)
        {
            if (rows == null)
            {
                return new T[0, 0];
            }

            List<DataRow> rowList = rows.ToList();
            if (rowList.Count == 0)
            {
                return new T[0, 0];
            }

            int colCount = rowList[0].Table.Columns.Count - ignoreColumns;
            int rowCount = rowList.Count - ignoreRows;

            T[,] result = new T[colCount, rowCount];

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    result[row, col] = (T)rowList[ignoreRows + row][ignoreColumns + col];
                }
            }

            return result;
        }


    }
}
