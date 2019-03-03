using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Extracts minimum and maximum values of given feature.
    /// <para>Default Pipeline Output: (List{double}) Min, (List{double}) Max </para>
    /// </summary>
    /// <remarks>
    /// Output features are lists, containing only one value each.
    /// </remarks>
    public class Extrema : PipelineBase, ITransformation
    {
        [Input]
        FeatureDescriptor<List<double>> Input;

        [Output("Min")]
        FeatureDescriptor<List<double>> OutputMin;

        [Output("Max")]
        FeatureDescriptor<List<double>> OutputMax;

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature<List<double>>(Input);
            //find min and max values
            double min = values.Min();
            double max = values.Max();

            this.Trace("SigID: {signature.ID} FeatureName: {Input.Name} Min: {min} Max: {max}", signature.ID, Input.Name, min, max);

            signature.SetFeature(OutputMin, new List<double> { min });//proba: minden featureben lehessen több érték, akkor is ha csak 1-et tarolunk
            signature.SetFeature(OutputMax, new List<double> { max });
            Progress = 100;
        }
    }
}
