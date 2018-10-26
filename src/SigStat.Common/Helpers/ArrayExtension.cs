using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common
{
    public static class ArrayExtension
    {
        public static void SetRow<T>(this T[,] array, int y, T value)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                array[x, y] = value;
            }
        }


        public static void SetColumn<T>(this T[,] array, int x, T value)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                array[x, y] = value;
            }
        }


        public static int Max(this int[,] array)
        {
            int max = int.MinValue;
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
            {
                if (max < array[i, j]) max = array[i, j];
            }
            return max;
        }

        public static byte Max(this byte[,] array)
        {
            byte max = byte.MinValue;
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (max < array[i, j]) max = array[i, j];
                }
            return max;
        }

        public static double Max(this double[,] array)
        {
            double max = double.MinValue;
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (max < array[i, j])
                    {
                        max = array[i, j];
                    }
                }
            return max;
        }
        public static Tuple<int,int> IndexOf(this int[,] array, int value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == value)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }

            return null;
        }
        public static Tuple<int, int> IndexOf(this double[,] array, double value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == value)
                        return new Tuple<int, int>(i, j);
                }
            return null;
        }
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <summary>
        /// Performs a given action on all items of the array and returns the original array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T[] ForEach<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i]);
            }
            return array;

        }

        public static T[][] CreateNested<T>(int length1, int length2)
        {
            T[][] result = new T[length1][];
            for (int i = 0; i < length1; i++)
            {
                result[i] = new T[length2];
            }

            return result;
        }

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

        public static IEnumerable<T> GetRow<T>(this T[,] array, int rowIndex)
        {
            for (int col = 0; col < array.GetLength(1); col++)
            {
                yield return array[rowIndex, col];
            }
        }

        public static IEnumerable<T> GetColumn<T>(this T[,] array, int colIndex)
        {
            for (int row = 0; row < array.GetLength(0); row++)
            {
                yield return array[row, colIndex];
            }
        }

        public static T[,] GetPart<T>(this T[,] source, int startIndex1, int startIndex2, int length1, int length2)
        {
            var result = new T[length1, length2];
            for (int i1 = 0; i1 < length1; i1++)
            {
                for (int i2 = 0; i2 < length2; i2++)
                {
                    result[i1, i2] = source[startIndex1 + i1, startIndex2 + i2];
                }
            }

            return result;
        }


        static Random random = new Random();
        public static T[] Shuffle<T>(this T[] array)
        {
            T[] retArray = new T[array.Length];
            array.CopyTo(retArray, 0);

            
            for (int i = 0; i < array.Length; i += 1)
            {
                int swapIndex = random.Next(i, array.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }

        public static T[] Clone<T>(this T[] array)
        {
            T[] clone=new T[array.Length];
            array.CopyTo(clone, 0);
            return clone;
        }

    }
}
