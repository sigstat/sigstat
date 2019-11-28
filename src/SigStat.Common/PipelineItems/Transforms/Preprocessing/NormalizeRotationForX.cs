using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SigStat.Common.Loaders;
using SigStat.Common.Transforms;
using SigStat.Common.Helpers;


namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Performs rotation normalization on the online signature
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.ITransformation" />
    [JsonObject(MemberSerialization.OptOut)]
    public class NormalizeRotationForX : PipelineBase, ITransformation
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

            var time = signature.GetFeature(InputT);

            var angle = CalculateAngleBetweenLines(
                time.Min(), time.Max(), linePoints.Min(), linePoints.Max(),
                time.Min(), time.Max(), linePoints.Min(), linePoints.Min());

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
            var xValues = new List<double>(sig.GetFeature(InputX));

            int numPoints = xValues.Count;
            double meanT = tValues.Average();
            double meanX = xValues.Average();

            double sumTSquared = tValues.Sum(t => t * t);

            double sumTX = 0;
            for (int i = 0; i < numPoints; i++)
            {
                sumTX += tValues[i] * xValues[i];
            }

            a = (sumTX / numPoints - meanT * meanX) / (sumTSquared / numPoints - meanT * meanT);
            b = (a * meanT - meanX);

            double a1 = a;
            double b1 = b;

            var newXValues = new List<double>(numPoints);
            for (int i = 0; i < numPoints; i++)
            {
                newXValues.Add(a1 * tValues[i] - b1);
            }

            return newXValues;

        }

        private double CalculateAngleBetweenLines(
            double p1x0, double p1x1, double p1y0, double p1y1,
            double p2x0, double p2x1, double p2y0, double p2y1)
        {
            //Calculate the angles
            var thetaP1 = Math.Atan2(p1y0 - p1y1, p1x0 - p1x1);
            var thetaP2 = Math.Atan2(p2y0 - p2y1, p2x0 - p2x1);

            //Calculate the angle between the lines
            var diff = Math.Abs(thetaP1 - thetaP2);

            return diff;
        }
    }
}
