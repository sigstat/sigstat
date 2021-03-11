using Newtonsoft.Json;
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
    /// <para>OutputFeature: output feature for scaled InputFeature</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class RelativeScale:PipelineBase, ITransformation
    {
        /// <summary>
        /// Gets or sets the input feature.
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputFeature { get; set; }
        /// <summary>
        /// Gets or sets the reference feature.
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> ReferenceFeature { get; set; }


        /// <summary>
        /// Gets or sets the output feature.
        /// </summary>
        [Output("ScaledFeature")]
        public FeatureDescriptor<List<double>> OutputFeature { get; set; }


        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            if (InputFeature == null || OutputFeature == null)
                throw new InvalidOperationException("Input or output feature is null");

            List<double> refValues = new List<double>(signature.GetFeature<List<double>>(ReferenceFeature).ToList());
            double maxValue = refValues.Max() - refValues.Min();

            List<double> values = new List<double>(signature.GetFeature<List<double>>(InputFeature).ToList());

            //find actual min and max values
            var oldMinValue = values.Min();
            var oldMaxValue = values.Max();

            //scale values between min and max
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = 
                    ((values[i] - oldMinValue) * (maxValue) / (oldMaxValue - oldMinValue));

            }

            signature.SetFeature(OutputFeature, values);
        }
    }
}
