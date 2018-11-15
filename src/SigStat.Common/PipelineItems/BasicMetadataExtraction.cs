using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using SigStat.Common;
using SigStat.Common.Helpers;

namespace Alairas.Common
{
    public class BasicMetadataExtraction : PipelineBase, ITransformation
    {
        public static double Trim
        {
            get { return 0.05; }
        }
        public void Transform(Signature signature)
        {
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
            var cog = Matrix.GetCog(weightMatrix);


            double sum;
            double limit;

            int top = -1;
            int left = -1;
            int right = image.Width;
            int bottom = image.Height;

            // top
            sum = 0;
            limit = Matrix.GetSum(weightMatrix, 0, 0, image.Width - 1, cog.Y) * Trim;
            do
            {
                sum += Matrix.GetSumRow(weightMatrix, ++top);
            }
            while (sum < limit);

            // bottom
            sum = 0;
            limit = Matrix.GetSum(
                weightMatrix, 0, cog.Y, image.Width - 1, image.Height - 1) * Trim;
            do
            {
                sum += Matrix.GetSumRow(weightMatrix, --bottom);
            }
            while (sum < limit);

            // left
            sum = 0;
            limit = Matrix.GetSum(weightMatrix, 0, 0, cog.X, image.Height - 1) * Trim;
            do
            {
                sum += Matrix.GetSumCol(weightMatrix, ++left);
            }
            while (sum < limit);

            // right
            sum = 0;
            limit = Matrix.GetSum(weightMatrix, cog.X, 0, image.Width - 1, image.Height - 1) * Trim;
            do
            {
                sum += Matrix.GetSumCol(weightMatrix, --right);
            }
            while (sum < limit);

            signature.SetFeature(Features.Bounds, bounds);
            signature.SetFeature(Features.Cog, cog);
            signature.SetFeature(Features.TrimmedBounds, new Rectangle(left, top, right - left, bottom - top));

        }

        public void Run(Signature input)
        {
            Transform(input);
        }

    }
}
