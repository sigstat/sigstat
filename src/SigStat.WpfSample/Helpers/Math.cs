using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public static class MyMath
    {
        public static T[,]  ToMatrix<T>(this List<T[]> listOfArrays)
        {
            T[,] result = new T[listOfArrays.Count, listOfArrays[0].Length];
            for (int i = 0; i < listOfArrays.Count; i++)
            {
                for (int j = 0; j < listOfArrays[0].Length; j++)
                {
                    result[i, j] = listOfArrays[i][j];
                }
            }
            return result;
                
        }


        public static double Min(double d1, double d2, double d3)
        {
            if (d1 < d2)
            {
                if (d3 < d1)
                    return d3;
                else
                    return d1;
            }
            else
            {
                if (d2 < d3)
                    return d2;
                else
                    return d3;
            }
        }

        //TODO: nem biztos, hogy kelleni fog
        public static double[] MinMaxTransformationNormalization(double[] originalValues)
        {
            double min = originalValues.Min();
            double max = originalValues.Max();
            for (int i = 0; i < originalValues.Length; i++)
            {
                originalValues[i] = (originalValues[i] - min) / (max - min);
            }

            return originalValues;
        }

        //TODO: 'a' paraméter átgondolása, numerikusan stabil megoldásra átírni
        public static double Sigmoid(double x, double a = 1)
        {
            return 1 / (1 + Math.Exp(-x * a));
        }


    }
}
