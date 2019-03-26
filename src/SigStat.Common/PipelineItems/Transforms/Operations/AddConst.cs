using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Adds a constant value to a feature. Works with collection features too.
    /// <para>Default Pipeline Output: Pipeline Input</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AddConst : PipelineBase, ITransformation
    {
        [Input]
        [JsonProperty]
        public FeatureDescriptor<List<double>> InputList { get; set; }

        //[Input(AutoSetMode = AutoSetMode.Never)]
        //public FeatureDescriptor<double> InputValue;

        [Output]
        public FeatureDescriptor<List<double>> Output { get; set; }

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
            if (InputList!=null)
            {
                List<double> values = signature.GetFeature(InputList);
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] = values[i] + addValue;
                    Progress += 100 / values.Count;
                }
                signature.SetFeature(Output, values);
            }
            /*else
            {
                double value = signature.GetFeature(InputValue);
                value = value + addValue;
                signature.SetFeature(Output, value);
            }*/

            Progress = 100;
        }
    }
}
