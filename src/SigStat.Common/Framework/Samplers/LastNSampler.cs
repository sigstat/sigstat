using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Universal sampler for training on the last N genuine signatures.
    /// </summary>
    public class LastNSampler : Sampler
    {
        public LastNSampler(int n = 10) : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Reverse().Take(n).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Reverse().Skip(n).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }
}
