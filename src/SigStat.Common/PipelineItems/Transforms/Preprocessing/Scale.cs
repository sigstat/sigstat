using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Maps values of a feature to a specific range.
    /// <para>InputFeature: feature to be scaled.</para>
    /// <para>OutputFeature: output feature for scaled InputFeature></para>
    /// </summary>
    /// <remarks> This is a specific case of the <see cref="Map"/> transform. </remarks>
    public class Scale:PipelineBase, ITransformation
    {
        [Input]
        //public FeatureDescriptor InputFeature { get; set; }
        FeatureDescriptor InputFeature;

        /// <summary>
        /// <para>NewMinValue: lower bound of the interval, in which the input feature will be scaled</para> 
        /// </summary>
        public double NewMinValue { get; set; } = 0;

        /// <summary>
        /// <para>NewMaxValue: upper bound of the interval, in which the input feature will be scaled</para>
        /// </summary>
        public double NewMaxValue { get; set; } = 1;

        [Output("ScaledFeature")] // jó lenne a névhez meghivatkozni az input nevét
        //public FeatureDescriptor OutputFeature { get; set; }
        FeatureDescriptor OutputFeature;


        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            if (InputFeature == null || OutputFeature == null)
                throw new NullReferenceException("Input or output feature is null");

            List<double> values = new List<double>(signature.GetFeature<List<double>>(InputFeature).ToList());

            //find actual min and max values
            var oldMinValue = values.Min();
            var oldMaxValue = values.Max();

            //scale values between min and max
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = NewMinValue +
                    ((values[i] - oldMinValue) * (NewMaxValue - NewMinValue) / (oldMaxValue - oldMinValue));

            }

            signature.SetFeature(OutputFeature, values);
        }
    }
}
