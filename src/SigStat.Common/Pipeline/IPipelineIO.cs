using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Gives ability to get or set (rewire) a pipeline item's default input and output features.
    /// </summary>
    public interface IPipelineIO
    {
        /// <summary>
        /// List of features to be used as input.
        /// </summary>
        List<FeatureDescriptor> InputFeatures { get; set; }
        List<FeatureDescriptor> GetInputFeatures();
        List<(FeatureDescriptor, FieldInfo, AutoSetMode)> GetInputFeatures2();
        /// <summary>
        /// List of features to be used as output.
        /// </summary>
        List<FeatureDescriptor> OutputFeatures { get; set; }
        List<FeatureDescriptor> GetOutputFeatures();
    }
}
