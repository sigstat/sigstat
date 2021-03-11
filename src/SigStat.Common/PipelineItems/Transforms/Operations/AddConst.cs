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
    [JsonObject(MemberSerialization.OptOut)]
    public class AddConst : PipelineBase, ITransformation
    {
        /// <summary>
        /// Input values for trasformation
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> Input { get; set; }

        /// <summary>
        /// Output feature to store results
        /// </summary>
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
            if (Input!=null)
            {
                List<double> values = signature.GetFeature(Input);
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] = values[i] + addValue;
                    Progress += 100 / values.Count;
                }
                signature.SetFeature(Output, values);
            }
           

            Progress = 100;
        }
    }
}
