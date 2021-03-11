using Newtonsoft.Json;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
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
    public class OrthognalRotation : PipelineBase, ITransformation
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
         
            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));

            int numPoints = yValues.Count;
            double meanX = xValues.Average();
            double meanY = yValues.Average();

   
            double Sx = 0;
            for (int i = 0; i < numPoints; i++)
            {
                Sx = Sx + ((xValues[i] - meanX) * (xValues[i] - meanX));
            }
            Sx = Sx / (numPoints - 1.0);

            double Sy = 0;
            for (int i = 0; i < numPoints; i++)
            {
                Sy = Sy + ((yValues[i] - meanY) * (yValues[i] - meanY));
            }
            Sy = Sy / (numPoints - 1.0);

            double covXY = 0;
            for (int i = 0; i < numPoints; i++)
            {
                covXY = covXY + ((xValues[i] - meanX) * (yValues[i] - meanY));
            }
            covXY = covXY / (numPoints - 1.0);

            double A = Sy - Sx;
            A = A + (Math.Sqrt(((Sy - Sx) * (Sy - Sx)) + (4 * covXY * covXY)));
            A = A / (2 * covXY);

            double alpha = Math.Tanh(A);


            for (int i = 0; i < numPoints; i++)
            {
                double x = xValues[i];
                double y = yValues[i];
                xValues[i] = (x * (Math.Cos(alpha))) - (y * (Math.Sin(alpha)));
                yValues[i] = (x * (Math.Sin(alpha))) + (y * (Math.Cos(alpha)));
            }

            signature.SetFeature(OutputX, xValues);
            signature.SetFeature(OutputY, yValues);
         
        }

        


        }
    }
