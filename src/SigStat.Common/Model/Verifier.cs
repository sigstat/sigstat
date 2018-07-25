using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model
{
    public class Verifier
    {
        public ProcessingPipeline Pipeline { get; set; }

        public Verifier()
        {
            Pipeline = new ProcessingPipeline();
        }
    }
}
