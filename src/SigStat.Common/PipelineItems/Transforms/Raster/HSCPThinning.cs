using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Iteratively thins the input binary raster with the <see cref="HSCPThinningStep"/> algorithm.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) HSCPThinningResult </para>
    /// </summary>
    public class HSCPThinning : PipelineBase, ITransformation
    {
        /// <summary> Initializes a new instance of the <see cref="HSCPThinning"/> class. </summary>
        public HSCPThinning()
        {
            this.Output(FeatureDescriptor.Get<bool[,]>("HSCPThinningResult"));
        }

        /// <inheritdoc/>
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
