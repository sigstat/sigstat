using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    public class LogMarker : ITransformation
    {
        private readonly Action<string> logger;
        private readonly FeatureDescriptor fd;

        public LogMarker(Action<string> logger, FeatureDescriptor fd)
        {
            this.logger = logger;
            this.fd = fd;
        }

        public void Transform(Signature signature)
        {
            logger(signature[fd].ToString());//TODO ez meg nem jo
        }
    }
}
