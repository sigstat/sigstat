using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Algorithms.Distances
{
    /// <summary>
    /// An abstract base class for the calculation of the distance of two entities (points, sequences etc.)
    /// </summary>
    /// <typeparam name="P">Entity type</typeparam>
    public interface IDistance<in P>
    {
        /// <summary>
        /// Calculates the distance between the two parameters
        /// </summary>
        /// <param name="p1">Firs parameter</param>
        /// <param name="p2">Second parameter</param>
        /// <returns></returns>
        double Calculate(P p1, P p2);
    }
}
