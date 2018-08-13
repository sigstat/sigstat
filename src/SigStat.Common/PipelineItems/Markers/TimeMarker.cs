using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    public class TimeMarkerStart : PipelineBase, ITransformation
    {
        public TimeMarkerStart()
        {
            this.Output(FeatureDescriptor<DateTime>.Descriptor("DefaultTimer"));
        }

        public void Transform(Signature signature)
        {
            signature.SetFeature(OutputFeatures[0], DateTime.Now);
        }
    }

    public class TimeMarkerStop : PipelineBase, ITransformation
    {
        public TimeMarkerStop()
        {
            this.Output(FeatureDescriptor<DateTime>.Descriptor("DefaultTimer"));
        }

        public void Transform(Signature signature)
        {
            signature.SetFeature(OutputFeatures[0], DateTime.Now - signature.GetFeature<DateTime>(OutputFeatures[0]));
        }
    }

}
