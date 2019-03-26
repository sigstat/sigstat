using SigStat.Common.Algorithms;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Iteratively thins the input binary raster with the <see cref="OnePixelThinningStep"/> algorithm.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) OnePixelThinningResult </para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class OnePixelThinning : PipelineBase, ITransformation
    {
        [Input]
        [JsonProperty]
        public FeatureDescriptor<bool[,]> Input { get; set; }

        [Output("OnePixelThinningResult")]
        public FeatureDescriptor<bool[,]> Output { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var b = signature.GetFeature(Input);
            Progress = 50;
            int stepCnt = 0;
            OnePixelThinningStep algo = new OnePixelThinningStep();
            while (algo.ResultChanged != false)
            {
                b = algo.Scan(b);
                stepCnt++;
            }
            signature.SetFeature(Output, b);
            Progress = 100;
            this.LogInformation( $"One pixel thinning steps applied {stepCnt} times.");
        }
    }
}
