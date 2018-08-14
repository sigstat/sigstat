using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// pl 1. Centroid.xy, 2. X, 3. Y .Akkor
    /// X osszes elemehez C.x-t adunk,
    /// Y osszes elemehez C.y-t adunk
    /// </summary>
    public class AddVector : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<List<double>> vectorFeature;

        /// <summary>
        /// Initialize with a vector feature.
        /// Don't forget to add as many Inputs as the vector's dimension.
        /// </summary>
        /// <param name="vectorFeature"></param>
        public AddVector(FeatureDescriptor<List<double>> vectorFeature)
        {
            this.vectorFeature = vectorFeature;
        }

        public void Transform(Signature signature)
        {
            //default output is the input
            if (OutputFeatures == null)
                OutputFeatures = InputFeatures;

            var vector = signature.GetFeature(vectorFeature);

            int dim = vector.Count;
            if ((InputFeatures.Count) != dim)
                Log(LogLevel.Error, "Dimension mismatch");

            for (int iF = 0; iF < dim; iF++)
            {
                var listFeature = signature.GetFeature<List<double>>(InputFeatures[iF]);
                for (int i = 0; i < listFeature.Count; i++)
                    listFeature[i] += vector[iF];
                signature.SetFeature(OutputFeatures[iF], listFeature);
                Progress += 100 / dim;
            }

            Progress = 100;
        }

    }
}
