using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    public class LogMarker : PipelineBase, ITransformation
    {
        private readonly LogLevel level;
        private readonly FeatureDescriptor fd;

        public LogMarker(LogLevel level,  FeatureDescriptor fd)
        {
            this.level = level;
            this.fd = fd;
        }

        public void Transform(Signature signature)
        {
            Log(level, $"Signature{signature.ID}.{fd.Name} = {signature[fd].ToString()}");
        }
    }
}
