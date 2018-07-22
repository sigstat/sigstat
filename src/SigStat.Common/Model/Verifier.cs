using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model
{
    class Verifier
    {
        public ProcessingPipeline Pipeline { get; private set; }

        public Verifier()
        {
            Pipeline = new ProcessingPipeline();
        }
    }
}
