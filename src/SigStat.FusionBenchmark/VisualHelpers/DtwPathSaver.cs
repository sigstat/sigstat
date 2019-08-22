using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    class DtwPathSaver: PipelineBase
    { 
        [Input]
        public List<FeatureDescriptor> Features { get; set; }

        public void Save(Signature sig1, Signature sig2)
        {
            

        }
    }
}
