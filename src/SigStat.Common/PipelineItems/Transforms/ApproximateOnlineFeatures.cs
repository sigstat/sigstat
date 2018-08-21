using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    //TODO: approximate Pressure based on Image and Skeleton

    /// <summary>
    /// init Pressure, Altitude, Azimuth features with default values.
    /// <para>Default Pipeline Output: Features.Pressure, Features.Altitude, Features.Azimuth</para>
    /// </summary>
    public class ApproximateOnlineFeatures : PipelineBase, ITransformation
    {
        /// <summary>Initializes a new instance of the <see cref="ApproximateOnlineFeatures"/> class.</summary>
        public ApproximateOnlineFeatures()
        {
            this.Output(Features.Pressure, Features.Altitude, Features.Azimuth);
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            int len = signature.GetFeature(Features.X).Count;
            List<double> defaultValues = new List<double>();
            for (int i = 0; i < len; i++)
                defaultValues.Add(0.5);
            signature.SetFeature(Features.Pressure, defaultValues);
            signature.SetFeature(Features.Altitude, defaultValues);
            signature.SetFeature(Features.Azimuth, defaultValues);

            //TODO: ez csak tmp. Valamit kitalalni

        }
    }
}
