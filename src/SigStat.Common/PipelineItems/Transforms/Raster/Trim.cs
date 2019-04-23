using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Trims unnecessary empty space from a binary raster.
    /// <para>Pipeline Input type: bool[,]</para>
    /// <para>Default Pipeline Output: (bool[,]) Trimmed</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Trim : PipelineBase, ITransformation
    {

        [Input]
        
        public FeatureDescriptor<bool[,]> Input { get; set; }

        [Output("Trimmed")]
        public FeatureDescriptor<bool[,]> Output { get; set; }

        private readonly int framewidth;

        /// <param name="framewidth">Leave a border around the trimmed area. framewidth > 0</param>
        public Trim(int framewidth)
        {
            this.framewidth = framewidth;
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            bool[,] input = signature.GetFeature(Input);
            int w = input.GetLength(0);
            int h = input.GetLength(1);

            if (framewidth < 0)
            {
                this.LogWarning("Negative frame width {framewidth}, this will result in data loss.", framewidth);
            }
            if (framewidth > w / 2 || framewidth > h / 2)
            {
                this.LogWarning("Too large frame width {framewidth}, this will result in empty raster.", framewidth);
            }

            int x0 = 0;
            int x1 = w;
            int y0 = 0;
            int y1 = h;

            //left
            for (int x = 0; x < w && (x0 == 0); x++)
            {
                for (int y = 0; y < h && (x0 == 0); y++)
                {
                    if (input[x, y])
                    {
                        x0 = Math.Max(0, x - framewidth);
                    }
                }
            }
            //right
            for (int x = w - 1; x >= 0 && (x1 == w); x--)
            {
                for (int y = 0; y < h && (x1 == w); y++)
                {
                    if (input[x, y])
                    {
                        x1 = Math.Min(w, x + framewidth);
                    }
                }
            }
            //top
            for (int y = 0; y < h && (y0 == 0); y++)
            {
                for (int x = 0; x < w && (y0 == 0); x++)
                {
                    if (input[x, y])
                    {
                        y0 = Math.Max(0, y - framewidth);
                    }
                }
            }
            //bottom
            for (int y = h - 1; y >= 0 && (y1 == h); y--)
            {
                for (int x = 0; x < w && (y1 == h); x++)
                {
                    if (input[x, y])
                    {
                        y1 = Math.Min(h, y + framewidth);
                    }
                }
            }

            bool[,] o = new bool[x1 - x0, y1 - y0];
            for (int x = 0; x < o.GetLength(0); x++)
            {
                for (int y = 0; y < o.GetLength(1); y++)
                {
                    o[x, y] = input[x0 + x, y0 + y];
                }
            }

            signature.SetFeature(Output, o);
            this.LogInformation($"Trimming done. New resolution: {x1-x0}x{y1-y0} px");
        }

    }
}
