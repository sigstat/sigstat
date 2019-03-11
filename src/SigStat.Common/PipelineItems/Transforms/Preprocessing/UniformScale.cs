using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Maps values of a feature to a specific range and another proportional.
    /// <para>BaseDimension: feature modelled the base dimension of the scaling. </para>
    /// <para>ProportionalDimension: feature modelled the dimension scaled proportionally to the base dimension. </para>
    /// <para>BaseDimensionOutput: output feature for scaled BaseDimension></para>
    /// <para>ProportionalDimensionOutput: output feature for scaled ProportionalDimension></para>
    /// </summary>
    /// <remarks> This is a specific case of the <see cref="Map"/> transform. </remarks>
    public class UniformScale : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> BaseDimension { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> ProportionalDimension { get; set; }

        /// <summary>
        /// Lower bound of the interval, in which the base dimension will be scaled
        /// </summary>
        public double NewMinBaseValue { get; set; } = 0;

        /// <summary>
        /// Upper bound of the interval, in which the base dimension will be scaled
        /// </summary>
        public double NewMaxBaseValue { get; set; } = 1;

        /// <summary>
        /// Lower bound of the interval, in which the proportional dimension will be scaled
        /// </summary>
        public double NewMinProportionalValue { get; set; } = 0;

        [Output("UniformScaledBaseDimension")]
        public FeatureDescriptor<List<double>> BaseDimensionOutput { get; set; }

        [Output("UniformScaledProportionalDimension")]
        public FeatureDescriptor<List<double>> ProportionalDimensionOutput { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            if (BaseDimension == null || BaseDimensionOutput == null)
            {
                throw new NullReferenceException("Input or output of the base dimension is null");
            }

            if (ProportionalDimension == null || ProportionalDimensionOutput == null)
            {
                throw new NullReferenceException("Input or output of the proportional dimension is null");
            }


            List<double> baseValues = new List<double>(signature.GetFeature(BaseDimension).ToList());
            List<double> propValues = new List<double>(signature.GetFeature(ProportionalDimension).ToList());

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
