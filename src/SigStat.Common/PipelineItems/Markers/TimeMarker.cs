using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    public class TimeMarkerStart : ITransformation
    {
        private readonly FeatureDescriptor<DateTime> time_fd;

        public TimeMarkerStart(FeatureDescriptor<DateTime> time_fd)
        {
            this.time_fd = time_fd;
        }

        public void Transform(Signature signature)
        {
            signature[time_fd] = DateTime.Now;
        }
    }

    public class TimeMarkerStop : ITransformation
    {
        private readonly FeatureDescriptor<DateTime> time_fd;

        public TimeMarkerStop(FeatureDescriptor<DateTime> time_fd)
        {
            this.time_fd = time_fd;
        }

        public void Transform(Signature signature)
        {
            signature[time_fd] = DateTime.Now - signature.GetFeature(time_fd);
        }
    }

}
