using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// HSCP thinning algorithm
    /// http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
    /// 
    /// TODO: ez parhuzamosithato
    /// </summary>
    public class HSCPThinning : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<bool[,]> bFd;

        public HSCPThinning(FeatureDescriptor<bool[,]> bFd)
        {
            this.bFd = bFd;
        }

        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature(bFd);
            Progress = 50;
            int stepCnt = 0;
            HSCPThinningStep algo = new HSCPThinningStep();
            while (algo.ResultChanged != false)
            {
                b = algo.Scan(b);
                stepCnt++;
            }
            signature.SetFeature(FeatureDescriptor<bool[,]>.Descriptor("Skeleton"), b);
            Progress = 100;
            Log(LogLevel.Info, $"HSCP thinning steps applied {stepCnt} times.");
        }
        
    }
}
