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
    public class NormalizeRotation2 : PipelineBase, ITransformation // based on : https://reader.elsevier.com/reader/sd/pii/S0031320316304368?token=F0AB3A2D0C3ECA816161E0F285FA74326474B9839000DEA23AC51CBF51AB99B85874E9F6179D4CE24FE673623A8D655C
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

            double xCentroid = xValues.Average();
            double yCentroid = yValues.Average();

            double sumXY = xValues.Select((x, i) => x * yValues[i]).Sum();
            double sumXSquare = xValues.Select(x => x * x).Sum();
            double sumYSquare = yValues.Select(y => y * y).Sum();

            // Moments of inertia referred to the reference centroid
            double Ixy_centroid = sumXY - (n * xCentroid * yCentroid);
            double Ix_centroid = sumYSquare - (n * yCentroid * yCentroid);
            double Iy_centroid = sumXSquare - (n * xCentroid * xCentroid);

            double alpha = 0.5 * Math.Atan2(2 * Ixy_centroid, Iy_centroid - Ix_centroid);
            double cosAlpha = Math.Cos(alpha);
            double sinAlpha = Math.Sin(alpha);

            var rotatedXValues = xValues.Select((x, i) => x * cosAlpha - yValues[i] * sinAlpha).ToList();
            var rotatedYValues = yValues.Select((y, i) => xValues[i] * sinAlpha + y * cosAlpha).ToList();

            signature.SetFeature(OutputX, rotatedXValues);
            signature.SetFeature(OutputY, rotatedYValues);
        }
    }
}
