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
            //  Signature s2 = new Signature();
            //      s2=signature;
            //  var tfs = new SequentialTransformPipeline
            //  {
            //      new ParallelTransformPipeline
            //      {

            //new Normalize() { Input = Features.X, Output = Features.X },
            //          new Normalize() { Input = Features.Y, Output = Features.Y },
            //      },
            //      new RealisticImageGenerator(1280, 720)
            //  };
            //  tfs.Logger = new SimpleConsoleLogger();
            //  tfs.Transform(s2);

            // // ImageSaver.Save(s2, $"{s2.Signer.ID }_{s2.ID }_Brfore_GeneratedOnlineImage.png");
            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));

            int numPoints = yValues.Count;
            double meanX = xValues.Average();
            double meanY = yValues.Average();

            double sumXSquared = xValues.Sum(x => x * x);
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

            double xmin = xValues.Min();
            double ymin = yValues.Min();

            //for (int i = 0; i < numPoints; i++)
            //{

            //    xValues[i] = xValues[i] - xmin;
            //    yValues[i] = yValues[i] - ymin;
            //}

            signature.SetFeature(OutputX, xValues);
            signature.SetFeature(OutputY, yValues);
            //    Signature s3 = new Signature();
            //    s3 = signature;
            //    tfs = new SequentialTransformPipeline
            //    {
            //        new ParallelTransformPipeline
            //        {

            //  new Normalize() { Input = Features.X, Output = Features.X },
            //            new Normalize() { Input = Features.Y, Output = Features.Y },
            //        },
            //        new RealisticImageGenerator(1280, 720)
            //    };
            //    tfs.Logger = new SimpleConsoleLogger();
            //    tfs.Transform(s3);

            //    //ImageSaver.Save(s3, $"{s3.Signer.ID }_{s3.ID }_After_GeneratedOnlineImage.png");
        }

        


        }
    }
