using SigStat.Common.Loaders;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using SigStat.Common.Transforms;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
{
    public static class SignatureHelper
    {
        /// <summary>
        /// Save online signature as file
        /// </summary>
        /// <param name="sig"></param>
        /// <param name="fileName"></param>
        public static void SaveImage(Signature sig, string fileName)
        {
            var tfs = new SequentialTransformPipeline
              {
                  new ParallelTransformPipeline
                  {
                      new Normalize() { Input = Features.X, Output = Features.X },
                      new Normalize() { Input = Features.Y, Output = Features.Y },
                  },
                  new RealisticImageGenerator(1280, 720)
              };
            tfs.Transform(sig);

            ImageSaver.Save(sig, fileName);
        }
        /// <summary>
        /// Return the signature length using Eculidan distance
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static double GetSignatureLength(Signature signature)
        {
            var x = signature.GetFeature(Features.X);
            var y = signature.GetFeature(Features.Y);
            double l = 0;
            for (int i = 0; i < x.Count - 1; i++)
            {
                l = l + (Math.Sqrt(((x[i + 1] - x[i]) * (x[i + 1] - x[i])) + ((y[i + 1] - y[i]) * (y[i + 1] - y[i]))));
            }
            return l;
        }

        internal static void CalculateStandardStatistics(this Signature signature)
        {
            var x = signature.GetFeature(Features.X);
            var y = signature.GetFeature(Features.Y);
            signature[Features.Size] = new SizeF((float)(x.Max() - x.Min()), (float)(y.Max() - y.Min()));
        }
    }
}
