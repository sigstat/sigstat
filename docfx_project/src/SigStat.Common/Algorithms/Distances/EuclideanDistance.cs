// Source code parts taken from Accord Math Library, to reduce dependencies.
// See:
// Accord Math Library
// The Accord.NET Framework
// http://accord-framework.net
// Copyright © César Souza, 2009-2014
// cesarsouza at gmail.com
// https://github.com/primaryobjects/Accord.NET/blob/master/Sources/Accord.Math/Distance.cs

namespace SigStat.Common.Algorithms.Distances
{
    /// <inheritdoc/>
    public class EuclideanDistance : IDistance<double[]>
    {

        /// <summary>
        ///   Gets the Euclidean distance between two points.
        /// </summary>
        /// 
        /// <param name="x">A point in space.</param>
        /// <param name="y">A point in space.</param>
        /// 
        /// <returns>The Euclidean distance between x and y.</returns>
        public double Calculate(double[] x, double[] y)
        {
            double d = 0.0, u;

            for (int i = 0; i < x.Length; i++)
            {
                u = x[i] - y[i];
                d += u * u;
            }

            return System.Math.Sqrt(d);

        }
    }
}
