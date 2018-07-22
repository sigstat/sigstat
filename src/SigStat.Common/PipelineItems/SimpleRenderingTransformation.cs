using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SigStat.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Drawing.Pens;
using SixLabors.Primitives;

namespace Alairas.Common
{
    /// <summary>
    /// Renders an image of the signature based on the available online information (X,Y,Dpi)
    /// </summary>
    public class SimpleRenderingTransformation : ITransformation
    {
        public void Transform(Signature signature)
        {
            // Read required features
            var xt = signature.GetFeature(Features.X);
            var yt = signature.GetFeature(Features.Y);
            var dpi = signature.GetFeature(Features.Dpi);

            // Calculate coordinates at 600 dpi
            var points = Enumerable
                .Range(0, xt.Count)
                .Select(i => new PointF(xt[i]*600/dpi, yt[i] * 600 / dpi))
                .ToList();

            // Set margins to 20 pixels
            var minX = points.Min(p => p.X)-10;
            var minY = points.Min(p => p.Y)-10;

            points.ForEach(p => p.X -= minX);
            points.ForEach(p => p.Y -= minY);

            var width = (int)points.Max(p => p.X) + 10;
            var height = (int)points.Max(p => p.Y) + 10;

            Image<Rgba32> image = new Image<Rgba32>(width, height);

            var pen = Pens.Solid(Rgba32.Blue, 10);
            image.Mutate(ctx =>
            {
                //TODO: respect up and down strokes
                //TODO: refine rendering
                ctx.DrawLines(pen, points.ToArray());
            });

            signature.SetFeature(Features.Image, image);
            
        }
    }
}

