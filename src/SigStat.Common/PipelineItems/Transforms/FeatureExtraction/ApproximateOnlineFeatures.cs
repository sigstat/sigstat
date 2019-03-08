using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    //TODO: approximate Pressure based on Image and Skeleton

    /// <summary>
    /// init Pressure, Altitude, Azimuth features with default values.
    /// <para>Default Pipeline Output: Features.Pressure, Features.Altitude, Features.Azimuth</para>
    /// </summary>
    public class ApproximateOnlineFeatures : PipelineBase, ITransformation
    {
        [Output("Pressure")]
        FeatureDescriptor<List<double>> OutputPressure { get; set; } = Features.Pressure;

        [Output("Altitude")]
        FeatureDescriptor<List<double>> OutputAltitude { get; set; } = Features.Altitude;

        [Output("Azimuth")]
        FeatureDescriptor<List<double>> OutputAzimuth { get; set; } = Features.Azimuth;

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            int len = signature.GetFeature(Features.X).Count;
            List<double> defaultValues = new List<double>();
            for (int i = 0; i < len; i++)
            {
                defaultValues.Add(0.5);
            }
            signature.SetFeature(OutputPressure, defaultValues);
            signature.SetFeature(OutputAltitude, defaultValues);
            signature.SetFeature(OutputAzimuth, defaultValues);

            //TODO: ez csak tmp. Valamit kitalalni

        }
    }
}
