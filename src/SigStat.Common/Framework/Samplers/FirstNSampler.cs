using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Universal sampler for training on the first N genuine signatures.
    /// </summary>
    public class FirstNSampler : Sampler
    {
        public FirstNSampler(int n = 10) : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(n).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(n).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }
}
