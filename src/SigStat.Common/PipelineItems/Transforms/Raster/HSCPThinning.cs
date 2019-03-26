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
    /// Iteratively thins the input binary raster with the <see cref="HSCPThinningStep"/> algorithm.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) HSCPThinningResult </para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class HSCPThinning : PipelineBase, ITransformation
    {
        [Input]
        [JsonProperty]
        public FeatureDescriptor<bool[,]> Input { get; set; }

        [Output("HSCPThinningResult")]
        public FeatureDescriptor<bool[,]> Output { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature(Input);
            Progress = 50;
            int stepCnt = 0;
            HSCPThinningStep algo = new HSCPThinningStep();
            while (algo.ResultChanged != false)
            {
                b = algo.Scan(b);
                stepCnt++;
            }
            signature.SetFeature(Output, b);
            Progress = 100;
            this.LogInformation( $"HSCP thinning steps applied {stepCnt} times.");
        }
        
    }
}
