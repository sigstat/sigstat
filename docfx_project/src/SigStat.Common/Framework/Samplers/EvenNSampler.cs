using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Selects the first N signatures with even index for training
    /// </summary>
    public class EvenNSampler : Sampler
    {
        /// <summary>
        /// Count of signatures used for training
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="n">count of signatures used for training</param>
        public EvenNSampler(int n = 10) : base(null, null, null)
        {
            N = n;
            TrainingFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Where((s, i) => i % 2 == 0).Take(N).ToList();
            GenuineTestFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Except(TrainingFilter(sl)).ToList();
            ForgeryTestFilter = sl => sl.Where(s => s.Origin == Origin.Forged).ToList();
        }
    }
}
