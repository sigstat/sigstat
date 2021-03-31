using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    /// <summary>
    /// Common mathematical functions used by the SigStat framework
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Returns the smallest of the three double parameters
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <returns></returns>
        public static double Min(double d1, double d2, double d3)
        {
            if (d1 < d2)
            {
                if (d1 < d3)
                {
                    return d1;
                }
                else
                {
                    return d3;
                }
            }
            else
            {
                if (d3 < d2)
                {
                    return d3;
                }
                else
                {
                    return d2;
                }
            }
        }

        /// <summary>
        /// Calculates the median of the given data series
        /// </summary>
        /// <param name="values">The data series</param>
        /// <returns></returns>
        public static double Median(this IEnumerable<double> values)
        {
            var valueList = new List<double>(values);
            valueList.Sort();
            return valueList[valueList.Count / 2];
        }
        /// <summary>
        /// return standard diviation of a feature values
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double StdDiviation(this IEnumerable<double> values)
        {
            double mean = values.Average();
            double sum = 0;
            foreach (double value in values)
            {
                sum = sum + ((value - mean) * (value - mean));
            }
            sum = sum / values.Count();

            return Math.Sqrt(sum);
        }
        
        /// <summary>
        /// Return true if the argument falls into the [-double.Epsilon,double.Epsilon] range
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool EqualsZero(this double d)
        {
            return double.Epsilon >= d && d >= -double.Epsilon;
        }

    }

   
}
