using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using SigStat.Common;
using SigStat.Common.Helpers;
using Newtonsoft.Json;

namespace SigStat.Common
{
    /// <summary>
    /// Extracts basic statistical signature (like <see cref="Features.Bounds"/> or <see cref="Features.Cog"/>) information from an Image
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BasicMetadataExtraction : PipelineBase, ITransformation
    {
        [JsonProperty]
        /// <summary>
        /// Represents theratio of significant pixels that should be trimmed
        /// from each side while calculating <see cref="Features.TrimmedBounds"/>
        /// </summary>
        public static double Trim { get; set; } = 0.05;

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //TODO: Implementation was ported from older code. Needs revision
            var image = signature.GetFeature(Features.Image);
            var bounds = new RectangleF(0, 0, image.Width, image.Height);

            double[,] weightMatrix = new double[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    weightMatrix[x, y] = 255 - (image[x, y].R + image[x, y].G + image[x, y].B) / 3;
                }
            }
            Point cog = new Point();
            (cog.X, cog.Y) = weightMatrix.GetCog();


            double sum;
            double limit;

            int top = -1;
            int left = -1;
            int right = image.Width;
            int bottom = image.Height;

            // top
            sum = 0;
            limit = weightMatrix.Sum(0, 0, image.Width - 1, cog.Y) * Trim;
            do
            {
                sum += weightMatrix.SumRow(++top);
            }
            while (sum < limit);

            // bottom
            sum = 0;
            limit = 
                weightMatrix.Sum(0, cog.Y, image.Width - 1, image.Height - 1) * Trim;
            do
            {
                sum += weightMatrix.SumRow(--bottom);
            }
            while (sum < limit);

            // left
            sum = 0;
            limit = weightMatrix.Sum(0, 0, cog.X, image.Height - 1) * Trim;
            do
            {
                sum += weightMatrix.SumCol(++left);
            }
            while (sum < limit);

            // right
            sum = 0;
            limit = weightMatrix.Sum( cog.X, 0, image.Width - 1, image.Height - 1) * Trim;
            do
            {
                sum += weightMatrix.SumCol(--right);
            }
            while (sum < limit);

            signature.SetFeature(Features.Bounds, bounds);
            signature.SetFeature(Features.Cog, cog);
            signature.SetFeature(Features.TrimmedBounds, new Rectangle(left, top, right - left, bottom - top));

        }

    }
}
