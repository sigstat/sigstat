using SigStat.Common;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Transformation
{
    /// <summary>
    /// Maps values of a feature to 0.0 - 1.0 range.
    /// <para>Pipeline Input type: List{double}</para>
    /// <para>Default Pipeline Output: (List{double}) UniformScalingResult</para>
    /// </summary>
    /// <remarks> This is a specific case of the <see cref="Map"/> transform. </remarks>
    public class Scale : PipelineBase, ITransformation
    {

        public FeatureDescriptor InputFeature { get; set; }
        public double NewMinValue { get; set; } = 0;
        public double NewMaxValue { get; set; } = 1;
        public FeatureDescriptor OutputFeature { get; set; }

        public Scale() { }

        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        /// <param name="inputFeature">Feature to be scaled.</param>
        /// <param name="newMinValue">Lower bound of the interval, in which the input feature will be scaled</param>
        /// <param name="newMaxValue">Upper bound of the interval, in which the input feature will be scaled</param>
        /// <param name="outputFeature">Output feature for scaled <paramref name="inputFeature"/></param>
        public Scale(FeatureDescriptor inputFeature, double newMinValue, double newMaxValue, FeatureDescriptor outputFeature = null)
        {
            InputFeature = inputFeature;
            NewMinValue = newMinValue;
            NewMaxValue = newMaxValue;
            OutputFeature = outputFeature ?? InputFeature;
        }


        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
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
