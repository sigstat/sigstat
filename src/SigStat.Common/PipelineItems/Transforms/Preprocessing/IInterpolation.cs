using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public interface IInterpolation
    {
        List<double> FeatureValues { get; set; }
        List<double> TimeValues { get; set; }

        double GetValue(double timestamp);
    }
}
