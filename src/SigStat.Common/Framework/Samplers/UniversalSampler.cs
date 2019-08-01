using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Framework.Samplers
{
    /// <summary>
    /// Selects a given number of signatures for training and testing
    /// </summary>
    public class UniversalSampler : Sampler
    {
        /// <summary>
        /// Count of signatures to use for training
        /// </summary>
        public int TrainingCount { get; set; }
        /// <summary>
        /// Count of signatures to use for testing
        /// </summary>
        public int TestCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trainingCount">Count of signatures to use for training</param>
        /// <param name="testCount">Count of signatures to use for testing</param>
        public UniversalSampler(int trainingCount, int testCount) : base(null,null,null)
        {
            TrainingCount = trainingCount;
            TestCount = testCount;
            TrainingFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Take(TrainingCount).ToList();
            GenuineTestFilter = sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(TrainingCount).Take(TestCount).ToList();
            ForgeryTestFilter = sl => sl.Where(s => s.Origin == Origin.Forged).Take(TestCount).ToList();
        }
    }
}
