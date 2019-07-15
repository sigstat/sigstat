using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Universal sampler for training on n odd indexed genuine signatures.
    /// </summary>ss
    public class OddNSampler : Sampler
    {
        public OddNSampler(int n = 10) : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where((s, i) => i % 2 != 0).Take(n).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where((s, i) => i % 2 == 0).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }
}
