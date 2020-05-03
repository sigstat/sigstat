using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Algorithms.Distances
{
    /// <summary>
    /// Calculates the distance between two vector sequences using Dynamic Time Warping
    /// </summary>
    /// <remarks>Based on: Abdullah Mueen, Eamonn J. Keogh: Extracting Optimal
    /// Performance from Dynamic Time Warping.KDD 2016: 2129-2130</remarks>
    public class DtwDistance : IDistance<double[][]>
    {
        /// <summary>
        /// The local distance function to use, when calculating the distance between two sueqence-points. Default is EuclideanDistance
        /// </summary>
        public IDistance<double[]> LocalDistance { get; set; } = new EuclideanDistance();


        /// <inheritdoc/>
        public double Calculate(double[][] p1, double[][] p2)
        {
            return DtwImplementations.OptimizedDtw(p1, p2, LocalDistance.Calculate);

        }
    }
}
