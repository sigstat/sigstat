using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class HSCPThinning : PipelineBase, ITransformation
    {

        public HSCPThinning()
        {
            this.Output(FeatureDescriptor<bool[,]>.Descriptor("HSCPThinningResult"));
        }

        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature<bool[,]>(InputFeatures[0]);
            Progress = 50;
            int stepCnt = 0;
            HSCPThinningStep algo = new HSCPThinningStep();
            while (algo.ResultChanged != false)
            {
                b = algo.Scan(b);
                stepCnt++;
            }
            signature.SetFeature(OutputFeatures[0], b);
            Progress = 100;
            Log(LogLevel.Info, $"HSCP thinning steps applied {stepCnt} times.");
        }
        
    }
}
