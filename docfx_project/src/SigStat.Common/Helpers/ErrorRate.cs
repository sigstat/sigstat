using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents the ErrorRates achieved in a benchmark
    /// </summary>
    public struct ErrorRate
    {
        /// <summary>
        /// False Acceptance Rate
        /// </summary>
        public double Far;
        /// <summary>
        /// False Rejection Rate
        /// </summary>
        public double Frr;
        /// <summary>
        /// Average Error Rate (calculated from Far and Frr)
        /// </summary>
        public double Aer { get { return (Far + Frr) / 2; } }
    }
}
