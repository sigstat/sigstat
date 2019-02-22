using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
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
    public class Multiply : PipelineBase, /*IEnumerable,*/ ITransformation
    {
        private readonly double byConst;

        [Input]
        public FeatureDescriptor<double> InputValue;

        [Input]
        public FeatureDescriptor<List<double>> InputList;

        [Output("MultiplyResult")]
        public FeatureDescriptor Output;

        /// <summary> Initializes a new instance of the <see cref="Multiply"/> class with specified settings. </summary>
        /// <param name="byConst">The value to multiply the input feature by.</param>
        public Multiply(double byConst)
        {
            this.byConst = byConst;
        }

        /*/// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return InputList.GetEnumerator();
        }
        /// <inheritdoc/>
        public void Add(FeatureDescriptor newItem)
        {
            InputList.Add(newItem);
        }*/

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //default output is the input
            if (Output == null)
            {
                if(InputValue!=null)
                    Output = InputValue;
                if(InputList!=null)
                    Output = InputList;
            }

            if (InputList!=null)
            {
                var values = signature.GetFeature(InputList);
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] = values[i] * byConst;
                }
                signature.SetFeature(Output, values);
            }
            else
            {
                var values = signature.GetFeature(InputValue);
                values = values * byConst;
                signature.SetFeature(Output, values);
            }

            Progress = 100;
        }
    }
}
