using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class OnePixelThinning : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<bool[,]> bFd;

        public OnePixelThinning(FeatureDescriptor<bool[,]> bFd)
        {
            this.bFd = bFd;
        }

        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature(bFd);
            Progress = 50;
            int stepCnt = 0;
            OnePixelThinningStep algo = new OnePixelThinningStep();
            while (algo.ResultChanged != false)
            {
                b = algo.Scan(b);
                stepCnt++;
            }
            signature.SetFeature(FeatureDescriptor<bool[,]>.Descriptor("Skeleton"), b);
            Progress = 100;
            Log(LogLevel.Info, $"One pixel thinning steps applied {stepCnt} times.");
        }
    }
}
