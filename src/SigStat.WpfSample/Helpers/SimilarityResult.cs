using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public class SimilarityResult
    {
        public Signature TestSignature { get; }
        public double AvgDistFromReferences { get; }

        public SimilarityResult(Signature testSignature, double avgDistFromReferences)
        {
            TestSignature = testSignature;
            AvgDistFromReferences = avgDistFromReferences;
        }
    }
}
