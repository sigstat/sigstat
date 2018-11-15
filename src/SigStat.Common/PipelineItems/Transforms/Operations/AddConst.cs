using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Adds a constant value to a feature. Works with collection features too.
    /// <para>Default Pipeline Output: Pipeline Input</para>
    /// </summary>
    public class AddConst : PipelineBase, ITransformation
    {
        private readonly double addValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConst"/> class with specified settings.
        /// </summary>
        /// <param name="value">The value to be added to the input feature.</param>
        public AddConst(double value)
        {
            this.addValue = value;
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //default output is the input
            if (OutputFeatures == null || OutputFeatures.Count == 0)
            {
                OutputFeatures = new List<FeatureDescriptor> { InputFeatures[0] };
            }


            if (InputFeatures[0].IsCollection)//we must treat this separately
            {
                List<double> values = signature.GetFeature<List<double>>(InputFeatures[0]);
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] = values[i] + addValue;
                    Progress += 100 / values.Count;
                }
                signature.SetFeature(OutputFeatures[0], values);
            }
            else
            {
                double value = signature.GetFeature<double>(InputFeatures[0]);
                value = value + addValue;
                signature.SetFeature(OutputFeatures[0], value);
            }

            Progress = 100;
        }
    }
}
