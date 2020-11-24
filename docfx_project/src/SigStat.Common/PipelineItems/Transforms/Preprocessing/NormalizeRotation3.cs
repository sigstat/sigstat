using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Performs rotation normalization on the online signature
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.ITransformation" />
    [JsonObject(MemberSerialization.OptOut)]
    public class NormalizeRotation3 : PipelineBase, ITransformation // based on: https://ieeexplore.ieee.org/stamp/stamp.jsp?tp=&arnumber=6408186
    {
        /// <summary>
        /// Gets or sets the input feature representing the X coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the input feature representing the Y coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; } = Features.Y;

        /// <summary>
        /// Gets or sets the output feature representing the X coordinates of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the output feature representing the Y coordinates of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputY { get; set; } = Features.Y;


        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));

            if (xValues.Count != yValues.Count)
                throw new ArgumentException($"The length of {nameof(InputX)} and {nameof(InputY)} are not the same.");

            int n = xValues.Count;

            double xMean = xValues.Average();
            double yMean = yValues.Average();

            double xVariance = xValues.Select(x => (x - xMean) * (x - xMean)).Sum() / (n - 1);
            double yVariance = yValues.Select(y => (y - yMean) * (y - yMean)).Sum() / (n - 1);
            double xyCovariance = xValues.Select((x, i) => (x - xMean) * (yValues[i] * yMean)).Sum() / (n - 1);

            double variancesSquareDiff = (yVariance * yVariance) - (xVariance * xVariance) ;

            double numerator = variancesSquareDiff + Math.Sqrt(variancesSquareDiff * variancesSquareDiff + 4 * xyCovariance * xyCovariance);
            double denominator = 2 * xyCovariance;

            double alpha = Math.Atan2(numerator, denominator);
            double cosAlpha = Math.Cos(alpha);
            double sinAlpha = Math.Sin(alpha);

            var rotatedXValues = xValues.Select((x, i) => x * cosAlpha - yValues[i] * sinAlpha).ToList();
            var rotatedYValues = yValues.Select((y, i) => xValues[i] * sinAlpha + y * cosAlpha).ToList();

            
            signature.SetFeature(OutputX, rotatedXValues);
            signature.SetFeature(OutputY, rotatedYValues);
        }
    }
}
