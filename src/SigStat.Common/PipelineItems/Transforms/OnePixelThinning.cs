using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class OnePixelThinning : PipelineBase, ITransformation
    {

        public OnePixelThinning()
        {
            this.Output(FeatureDescriptor<bool[,]>.Descriptor("OnePixelThinningResult"));
        }

        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature<bool[,]>(InputFeatures[0]);
            Progress = 50;
            int stepCnt = 0;
            OnePixelThinningStep algo = new OnePixelThinningStep();
            while (algo.ResultChanged != false)
            {
                b = algo.Scan(b);
                stepCnt++;
            }
            signature.SetFeature(OutputFeatures[0], b);
            Progress = 100;
            Log(LogLevel.Info, $"One pixel thinning steps applied {stepCnt} times.");
        }
    }
}
