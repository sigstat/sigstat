using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    public class UniversalSampler : Sampler
    {
        // TODO: this can use wrong configurations 
        public UniversalSampler(int refCount, int testCount) : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(refCount).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(refCount).Take(testCount).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).Take(testCount).ToList())
        { }
    }
}
