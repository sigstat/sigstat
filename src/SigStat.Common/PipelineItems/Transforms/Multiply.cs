using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    //TODO: ez miert fogad el tobb Inputot is, csak az elsot transzformalja

    /// <summary>
    /// Multiplies the values of a feature with a given constant.
    /// <para>Pipeline Input type: List{double}</para>
    /// <para>Default Pipeline Output: (List{double}) Input</para>
    /// </summary>
    public class Multiply : PipelineBase, IEnumerable, ITransformation
    {
        private readonly double byConst;

        /// <summary> Initializes a new instance of the <see cref="Multiply"/> class with specified settings. </summary>
        /// <param name="byConst">The value to multiply the input feature by.</param>
        public Multiply(double byConst)
        {
            this.byConst = byConst;
        }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return InputFeatures.GetEnumerator();
        }
        /// <inheritdoc/>
        public void Add(FeatureDescriptor newItem)
        {
            InputFeatures.Add(newItem);
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //default output is the input
            if (OutputFeatures == null)
                OutputFeatures = InputFeatures;

            if (InputFeatures[0].IsCollection)
            {
                var values = signature.GetFeature<List<double>>(InputFeatures[0]);
                for (int i = 0; i < values.Count; i++)
                    values[i] = values[i] * byConst;
                signature.SetFeature(OutputFeatures[0], values);
            }
            else
            {
                var values = signature.GetFeature<double>(InputFeatures[0]);
                values = values * byConst;
                signature.SetFeature(OutputFeatures[0], values);
            }

            Progress = 100;
        }
    }
}
