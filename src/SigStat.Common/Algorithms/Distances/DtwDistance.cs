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
        public IDistance<double[]> LocalDistance { get; set; }


        /// <summary>
        /// Initializes a new instance of the DtwDistance class with default settings
        /// </summary>
        /// <param name="localDistance">The distance function used to calculate the distance between two individual points of the squences. Set the parameter to 'null' to use the default <see cref="EuclideanDistance"/></param>
        public DtwDistance(IDistance<double[]> localDistance = null)
        {
            this.LocalDistance = localDistance ?? new EuclideanDistance();
        }

        /// <inheritdoc/>
        public double Calculate(double[][] p1, double[][] p2)
        {
            return DtwImplementations.ExactDtwWikipedia(p1, p2, LocalDistance.Calculate);

        }
    }
}
