using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Selects the first N signatures for training
    /// </summary>
    public class LastNSampler : Sampler
    {
        /// <summary>
        /// Count of signatures used for training
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="n">Count of signatures used for training</param>
        public LastNSampler(int n = 10) : base(null, null, null)
        {
            N = n;
            TrainingFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Reverse().Take(N).Reverse().ToList();
            GenuineTestFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Reverse().Skip(N).Reverse().ToList();
            ForgeryTestFilter = sl => sl.Where(s => s.Origin == Origin.Forged).ToList();
        }
    }
}
