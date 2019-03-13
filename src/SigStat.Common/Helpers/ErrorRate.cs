using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public struct ErrorRate
    {
        public double Far;
        public double Frr;
        public double Aer { get { return (Far + Frr) / 2; } }
    }
}
