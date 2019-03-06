using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public abstract class InterpolationBase : IInterpolation
    {
        public List<double> FeatureValues { get; set; }
        public List<double> TimeValues { get; set; }

        public abstract double GetValue(double timestamp);
    }
}
