using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
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
    }
}
