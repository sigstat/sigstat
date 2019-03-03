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
    public class UniformScale : PipelineBase, ITransformation
    { 
        public FeatureDescriptor BaseDimension { get; set; }
        public FeatureDescriptor ProportionalDimension { get; set; }
        public double NewMinBaseValue { get; set; } = 0;
        public double NewMaxBaseValue { get; set; } = 1;
        public double NewMinProportionalValue { get; set; } = 0;
        public FeatureDescriptor BaseDimensionOutput { get; set; }
        public FeatureDescriptor ProportionalDimensionOutput { get; set; }

        public UniformScale() { }

        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        /// <param name="baseDimension">Feature modelled the base dimension of the scaling.</param>
        /// <param name="proposalDimension">Feature modelled the dimension scaled proportionally to the base dimension.</param>
        /// <param name="newMinBaseValue">Lower bound of the interval, in which the base dimension will be scaled</param>
        /// <param name="newMaxBaseValue">Upper bound of the interval, in which the base dimension will be scaled</param>
        /// <param name="newMinProportionalValue">Lower bound of the interval, in which the proportional dimension will be scaled</param>
        /// <param name="baseDimensionOutput">Output feature for scaled <paramref name="baseDimension"/></param>
        /// <param name="proportionalDimensionOutput">Output feature for scaled <paramref name="proportionalDimension"/></param>
        public UniformScale(FeatureDescriptor baseDimension, FeatureDescriptor proportionalDimension, 
            double newMinBaseValue, double newMaxBaseValue, double newMinProportionalValue,
            FeatureDescriptor baseDimensionOutput = null, FeatureDescriptor proportionalDimensionOutput = null)
        {
            BaseDimension = baseDimension;
            ProportionalDimension = proportionalDimension;
            NewMinBaseValue = newMinBaseValue;
            NewMaxBaseValue = newMaxBaseValue;
            NewMinProportionalValue = newMinProportionalValue;
            BaseDimensionOutput = baseDimensionOutput ?? BaseDimension;
            ProportionalDimensionOutput = proportionalDimensionOutput ?? ProportionalDimension;
        }


        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            List<double> baseValues = new List<double>(signature.GetFeature<List<double>>(BaseDimension).ToList());
            List<double> propValues = new List<double>(signature.GetFeature<List<double>>(ProportionalDimension).ToList());

            //find actual min and max values
            var oldMinBaseValue = baseValues.Min();
            var oldMaxBaseValue = baseValues.Max();
            var oldMinPropValue = propValues.Min();
            var oldMaxPropValue = propValues.Max();

            // newMaxProp = newMinProp + (newBaseIntervalLength / oldBaseIntervalLength) * oldPropIntervalLength
            var newMaxPropValue = NewMinProportionalValue + 
                ((NewMaxBaseValue - NewMinBaseValue) / (oldMaxBaseValue - oldMinBaseValue)) * (oldMaxPropValue - oldMinPropValue);

            //scale values between min and max
            for (int i = 0; i < baseValues.Count; i++)
            {
                baseValues[i] = NewMinBaseValue + 
                    ((baseValues[i] - oldMinBaseValue) * (NewMaxBaseValue - NewMinBaseValue) / (oldMaxBaseValue - oldMinBaseValue));
                propValues[i] = NewMinProportionalValue + 
                    ((propValues[i] - oldMinPropValue) * (newMaxPropValue - NewMinProportionalValue) / (oldMaxPropValue - oldMinPropValue));
            }

            signature.SetFeature(BaseDimensionOutput, baseValues);
            signature.SetFeature(ProportionalDimensionOutput, propValues);
        }

    }
}
