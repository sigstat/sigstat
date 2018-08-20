using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Markers
{
    /// <summary>
    /// Starts a timer to measure completion time of following transforms.
    /// <para>Default Pipeline Output: (<see cref="DateTime"/>) DefaultTimer</para>
    /// </summary>
    public class TimeMarkerStart : PipelineBase, ITransformation
    {
        /// <summary>Starts a timer to measure completion time of following transforms.</summary>
        public TimeMarkerStart()
        {
            this.Output(FeatureDescriptor<DateTime>.Descriptor("DefaultTimer"));
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            signature.SetFeature(OutputFeatures[0], DateTime.Now);
        }
    }

    /// <summary>
    /// Stops a timer to measure completion time of previous transforms.
    /// <para>Default Pipeline Output: (<see cref="DateTime"/>) DefaultTimer</para>
    /// </summary>
    public class TimeMarkerStop : PipelineBase, ITransformation
    {
        /// <summary>Stops a timer to measure completion time of previous transforms.</summary>
        public TimeMarkerStop()
        {
            this.Output(FeatureDescriptor<DateTime>.Descriptor("DefaultTimer"));
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            signature.SetFeature(OutputFeatures[0], DateTime.Now - signature.GetFeature<DateTime>(OutputFeatures[0]));
        }
    }

}
