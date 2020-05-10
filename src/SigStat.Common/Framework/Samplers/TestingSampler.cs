using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Testing sampler for signer dependent sampling frequency verification system
    /// </summary>
    public class TestingSampler: Sampler
    {
        /// <summary>
        /// Count of signatures used for training
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="n">count of signatures used for training</param>
        public TestingSampler(int n = 5) : base(null, null, null)
        {
            N = n;
            TrainingFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Take(N).ToList();
            GenuineTestFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(N).Take(N).ToList();
            ForgeryTestFilter = sl => sl.Where(s => s.Origin == Origin.Forged).ToList();
        }
    }
}
