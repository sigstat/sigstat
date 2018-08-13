using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Find minimum and maximum values of given feature, add them as new features
    /// </summary>
    public class Extrema : PipelineBase, ITransformation
    {

        public Extrema()
        {
            this.Output(
                FeatureDescriptor<List<double>>.Descriptor("Min"),
                FeatureDescriptor<List<double>>.Descriptor("Max")
            );
        }

        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature<List<double>>(InputFeatures[0]);
            //find min and max values
            double min = values.Min();
            double max = values.Max();

            Log(LogLevel.Debug, $"SigID: {signature.ID} FeatureName: {InputFeatures[0].Name} Min: {min} Max: {max}");

            signature.SetFeature(OutputFeatures[0], new List<double> { min });//proba: minden featureben lehessen több érték, akkor is ha csak 1-et tarolunk
            signature.SetFeature(OutputFeatures[1], new List<double> { max });
            Progress = 100;
        }
    }
}
