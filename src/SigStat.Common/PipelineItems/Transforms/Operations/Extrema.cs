using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

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
        /// <summary> Initializes a new instance of the <see cref="Extrema"/> class. </summary>
        public Extrema()
        {
            this.Output(
                FeatureDescriptor.Get<List<double>>("Min"),
                FeatureDescriptor.Get<List<double>>("Max")
            );
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature<List<double>>(InputFeatures[0]);
            //find min and max values
            double min = values.Min();
            double max = values.Max();

            Logger.LogTrace($"SigID: {signature.ID} FeatureName: {InputFeatures[0].Name} Min: {min} Max: {max}");

            signature.SetFeature(OutputFeatures[0], new List<double> { min });//proba: minden featureben lehessen több érték, akkor is ha csak 1-et tarolunk
            signature.SetFeature(OutputFeatures[1], new List<double> { max });
            Progress = 100;
        }
    }
}
