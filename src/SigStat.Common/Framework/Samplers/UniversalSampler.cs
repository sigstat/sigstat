using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    public class UniversalSampler : Sampler
    {
        // TODO: this can use wrong configurations 
        public int RefCount { get; set; }
        public int TestCount { get; set; }
        public UniversalSampler(int refCount, int testCount) : base(null,null,null)
        {
            RefCount = refCount;
            TestCount = testCount;
            references = sl => sl.Where(s => s.Origin == Origin.Genuine).Take(RefCount).ToList();
            genuineTests = sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(RefCount).Take(TestCount).ToList();
            forgeryTests = sl => sl.Where(s => s.Origin == Origin.Forged).Take(TestCount).ToList();
        }
    }
}
