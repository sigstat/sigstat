using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    public class LogMarker : PipelineBase, ITransformation
    {
        private readonly LogLevel level;

        public LogMarker(LogLevel level)
        {
            this.level = level;
            //this.Input(FeatureDescriptor<DateTime>.Descriptor("DefaultTimer"));
        }

        public void Transform(Signature signature)
        {
            Log(level, $"Signature{signature.ID}.{InputFeatures[0].Name} = {signature[InputFeatures[0]].ToString()}");
        }
    }
}
