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
    public class NormalizeRotation : PipelineBase, ITransformation
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
        /// Gets or sets the input feature representing the timestamps of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputT { get; set; } = Features.T;

        /// <summary>
        /// Gets or sets the output feature representing the X coordinates of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the input feature representing the Y coordinates of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputY { get; set; } = Features.Y;

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var linePoints = GenerateLinearBestFit(signature, out double a, out double b);

            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));

            if (xValues.Count != yValues.Count)
                throw new ArgumentException($"The length of {nameof(InputX)} and {nameof(InputY)} are not the same");

            var time = signature.GetFeature(InputT);

            var angle = Math.Atan((linePoints.Max() - linePoints.Min()) / (time.Max() - time.Min()));

            double cosa = Math.Cos(angle);
            double sina = Math.Sin(angle);

            for (int i = 0; i < xValues.Count; i++)
            {
                double x = xValues[i];
                double y = yValues[i];
                xValues[i] = x * cosa - y * sina;
                yValues[i] = x * sina + y * cosa;
            }

            signature.SetFeature(OutputX, xValues);
            signature.SetFeature(OutputY, yValues);
        }


        private List<double> GenerateLinearBestFit(Signature sig, out double a, out double b)
        {
            var tValues = new List<double>(sig.GetFeature(InputT));
            var yValues = new List<double>(sig.GetFeature(InputY));

            if (tValues.Count != yValues.Count)
                throw new ArgumentException($"The length of {nameof(InputT)} and {nameof(InputY)} are not the same");

            int numPoints = yValues.Count;
            double sumT = tValues.Sum();
            double sumY = yValues.Sum();
            double sumTSquared = tValues.Sum(t => t * t);
            double sumTY = tValues.Select((t, i) => t * yValues[i]).Sum();

            a = (sumTY * numPoints - sumT * sumY) / (sumTSquared * numPoints - sumT * sumT);

            double meanT = tValues.Average();
            double meanY = yValues.Average();
            b = (meanY - a * meanT);

            double a1 = a;
            double b1 = b;

            return tValues.Select(t => b1 + a1 * t).ToList();

        }

    }
}
