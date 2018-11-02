using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Iteratively thins the input binary raster with the <see cref="OnePixelThinningStep"/> algorithm.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) OnePixelThinningResult </para>
    /// </summary>
    public class OnePixelThinning : PipelineBase, ITransformation
    {
        /// <summary> Initializes a new instance of the <see cref="OnePixelThinning"/> class. </summary>
        public OnePixelThinning()
        {
            this.Output(FeatureDescriptor.Get<bool[,]>("OnePixelThinningResult"));
        }

        /// <inheritdoc/>
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
