using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public interface IInterpolation
    {
        double GetValue(double timestamp);
    }
}
