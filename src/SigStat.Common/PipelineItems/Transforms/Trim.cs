using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// Trims unnecessary empty space from a binary raster.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) Trimmed</para>
    /// </summary>
    public class Trim : PipelineBase, ITransformation
    {
        private readonly int framewidth;

        /// <param name="framewidth">Leave a border around the trimmed area. framewidth > 0</param>
        public Trim(int framewidth)
        {
            this.framewidth = framewidth;
            this.Output(FeatureDescriptor<bool[,]>.Descriptor("Trimmed"));
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            bool[,] input = signature.GetFeature<bool[,]>(InputFeatures[0]);
            int w = input.GetLength(0);
            int h = input.GetLength(1);

            if (framewidth < 0)
                Log(LogLevel.Warn, $"Negative frame width {framewidth}, this will result in data loss.");
            if(framewidth > w/2 || framewidth > h/2)
                Log(LogLevel.Warn, $"Too large frame width {framewidth}, this will result in empty raster.");

            int x0 = 0;
            int x1 = w;
            int y0 = 0;
            int y1 = h;

            //left
            for (int x = 0; x < w && (x0 == 0); x++)
                for (int y = 0; y < h && (x0 == 0); y++)
                    if (input[x, y])
                        x0 = Math.Max(0, x - framewidth);
            Progress = 25;
            //right
            for (int x = w - 1; x >= 0 && (x1 == w); x--)
                for (int y = 0; y < h && (x1 == w); y++)
                    if (input[x, y])
                        x1 = Math.Min(w, x + framewidth);
            Progress = 50;
            //top
            for (int y = 0; y < h && (y0==0); y++)
                for (int x = 0; x < w && (y0==0); x++)
                    if (input[x, y])
                        y0 = Math.Max(0, y - framewidth);
            Progress = 75;
            //bottom
            for (int y = h - 1; y >= 0 && (y1==h); y--)
                for (int x = 0; x < w && (y1==h); x++)
                    if (input[x, y])
                        y1 = Math.Min(h, y + framewidth);

            bool[,] o = new bool[x1 - x0, y1 - y0];
            for (int x = 0; x < o.GetLength(0); x++)
                for (int y = 0; y < o.GetLength(1); y++)
                    o[x, y] = input[x0 + x, y0 + y];

            signature.SetFeature(OutputFeatures[0], o);
            Progress = 100;
            Log(LogLevel.Info, $"Trimming done. New resolution: {x1-x0}x{y1-y0} px");
        }

    }
}
