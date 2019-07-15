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
        public int N { get; set; }
        public LastNSampler(int n = 10) : base(null, null, null)
        {
            N = n;
            references = sl => sl.Where(s => s.Origin == Origin.Genuine).Reverse().Take(N).ToList();
            genuineTests = sl => sl.Where(s => s.Origin == Origin.Genuine).Reverse().Skip(N).ToList();
            forgeryTests = sl => sl.Where(s => s.Origin == Origin.Forged).ToList();
        }
    }
}
