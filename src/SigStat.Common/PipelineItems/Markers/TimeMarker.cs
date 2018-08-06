using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    public class TimeMarkerStart : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<DateTime> timeFd;

        public TimeMarkerStart(FeatureDescriptor<DateTime> timeFd)
        {
            this.timeFd = timeFd;
        }

        public void Transform(Signature signature)
        {
            signature[timeFd] = DateTime.Now;
        }
    }

    public class TimeMarkerStop : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<DateTime> timeFd;
        
        public TimeMarkerStop(FeatureDescriptor<DateTime> timeFd)
        {
            this.timeFd = timeFd;
        }

        public void Transform(Signature signature)
        {
            signature[timeFd] = DateTime.Now - signature.GetFeature(timeFd);
        }
    }

}
