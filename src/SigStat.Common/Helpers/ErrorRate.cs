using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents the ErrorRates achieved in a benchmark
    /// </summary>
    public struct ErrorRate : IEquatable<ErrorRate>
    {
        /// <summary>
        /// False Acceptance Rate
        /// </summary>
        public double Far { get; set; }
        /// <summary>
        /// False Rejection Rate
        /// </summary>
        public double Frr { get; set; }
        /// <summary>
        /// Average Error Rate (calculated from Far and Frr)
        /// </summary>
        public double Aer { get { return (Far + Frr) / 2; } }


        /// <summary>
        /// Checks for equality of double values 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ErrorRate other)
        {
            return
                Math.Abs(Far - other.Far).EqualsZero()
                && Math.Abs(Frr - other.Frr).EqualsZero()
                && Math.Abs(Aer - other.Aer).EqualsZero();
        }
    }
}
