using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public class NormalizeRotation : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; } = Features.X;

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; } = Features.Y;

        [Input]
        public FeatureDescriptor<List<double>> InputT { get; set; } = Features.T;

        [Output]
        public FeatureDescriptor<List<double>> OutputX { get; set; } = Features.X;

        [Output]
        public FeatureDescriptor<List<double>> OutputY { get; set; } = Features.Y;


        public void Transform(Signature signature)
        {
            var linePoints = GenerateLinearBestFit(signature, out double a, out double b);

            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));

            var time = signature.GetFeature(InputT).ToList();
            double cosa = (time.Max() - time.Min()) / (linePoints.Max() - linePoints.Min());
            double sina = (linePoints.Max() - linePoints.Min()) / (time.Max() - time.Min());

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

            int numPoints = yValues.Count;
            double meanT = tValues.Average();
            double meanY = yValues.Average();

            double sumTSquared = tValues.Sum(t => t * t);

            double sumTY = 0;
            for (int i = 0; i < numPoints; i++)
            {
                sumTY += tValues[i] * yValues[i];
            }

            a = (sumTY / numPoints - meanT * meanY) / (sumTSquared / numPoints - meanT * meanT);
            b = (a * meanT - meanY);

            double a1 = a;
            double b1 = b;

            var newYValues = new List<double>(numPoints);
            for (int i = 0; i < numPoints; i++)
            {
                newYValues.Add(a1 * tValues[i] - b1);
            }

            return newYValues;

        }
    }
}
