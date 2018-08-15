using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// init Pressure, Altitude, Azimuth features with default values.
    /// TODO: approximate Pressure based on Image and Skeleton
    /// </summary>
    public class ApproximateOnlineFeatures : PipelineBase, ITransformation
    {
        public ApproximateOnlineFeatures()
        {
            this.Output(Features.Pressure, Features.Altitude, Features.Azimuth);
        }

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
