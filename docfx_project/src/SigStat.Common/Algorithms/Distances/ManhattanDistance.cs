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
    public class ManhattanDistance : IDistance<double[]>
    {

        /// <summary>
        ///   Gets the Manhattan distance between two points.
        /// </summary>
        /// 
        /// <param name="x">A point in space.</param>
        /// <param name="y">A point in space.</param>
        /// 
        /// <returns>The Manhattan distance between x and y.</returns>
        public double Calculate(double[] x, double[] y)
        {
            double sum = 0.0;
            for (int i = 0; i < x.Length; i++)
                sum += System.Math.Abs(x[i] - y[i]);
            return sum;

        }
    }
}
