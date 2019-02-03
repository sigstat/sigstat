using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace SigStat.Common
{
    /// <summary>
    ///
    /// </summary>
    public class Baseline
    {
        /// <summary>
        /// Starting point of the baseline
        /// </summary>
        public PointF Start { get; set; }
        /// <summary>
        /// Endpoint of the baseline
        /// </summary>
        public PointF End { get; set; }

        /// <summary>
        /// Initializes a Baseline instance
        /// </summary>
        public Baseline()
        {
        }

        /// <summary>
        /// Initializes a Baseline instance with the given startpoint and endpoint
        /// </summary>
        /// <param name="x1">x coordinate for the start point</param>
        /// <param name="y1">y coordinate for the start point</param>
        /// <param name="x2">x coordinate for the endpoint</param>
        /// <param name="y2">y coordinate for the endpoint</param>
        public Baseline(int x1, int y1, int x2, int y2)
        {
            Start = new PointF(x1, y1);
            End = new PointF(x2, y2);
        }

        /// <summary>
        /// Returns a string representation of the baseline
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Baseline {Start}-{End}";
        }
    }
}
