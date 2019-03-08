using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Generates a binary raster version of the input image with the iterative threshold method.
    /// <para>Pipeline Input type: Image{Rgba32}</para>
    /// <para>Default Pipeline Output: (bool[,]) Binarized</para>
    /// </summary>
    public class Binarization : PipelineBase, ITransformation
    {

        /// <summary> Represents the type of the input image. </summary>
        public enum ForegroundType
        {
            /// <summary> (default) Foreground is darker than background. (eg. ink on paper) </summary>
            Dark,
            /// <summary> Foreground is brighter than background. (for non-signature images) </summary>
            Bright
        }

        private double? binThreshold = null;//0-1 //null: iterative
        private readonly ForegroundType foregroundType = ForegroundType.Dark;

        /// <summary> Initializes a new instance of the <see cref="Binarization"/> class with default settings: Iterative threshold and <see cref="ForegroundType.Dark"/>. </summary>
        public Binarization() :this(ForegroundType.Dark, null)
        {
        }
        /// <summary> Initializes a new instance of the <see cref="Binarization"/> class with specified settings. </summary>
        /// <param name="foregroundType"></param>
        /// <param name="binThreshold">Use this threshold value instead of iteratively calculating it. Range from 0 to 1</param>
        public Binarization(ForegroundType foregroundType, double? binThreshold)
        {
            this.foregroundType = foregroundType;
            this.binThreshold = binThreshold;
            //this.Output(FeatureDescriptor.Get<bool[,]>("Binarized"));
        }

        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Output("Binarized")]
        public FeatureDescriptor<bool[,]> OutputMask { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            Image<Rgba32> image = signature.GetFeature(InputImage);
            int w = image.Size().Width;
            int h = image.Size().Height;

            if (binThreshold != null && (binThreshold < 0 || binThreshold > 1))
            {
                this.LogWarning("Binarization Threshold is set to an invalid value: {binThreshold}. The valid range is from 0.0 to 1.0", binThreshold);
            }

            if (binThreshold == null)   //find threshold if not specified
            {
                binThreshold = IterativeThreshold(image, 0.008);
            }

            //binarize
            bool[,] b = new bool[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    b[i, h - j - 1] = (Level(image[i, j]) > binThreshold);
                }
                Progress += (int)((1.0 / w)*100);
            }
            this.LogInformation( "Binarization done.");
            signature.SetFeature(OutputMask, b);
            Progress = 100;
        }

        /// <summary>
        /// http://accord-framework.net/docs/html/T_Accord_Imaging_Filters_IterativeThreshold.htm
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxError">pl 0.008</param>
        /// <returns></returns>
        private double IterativeThreshold(Image<Rgba32> image, double maxError)
        {
            int w = image.Size().Width;
            int h = image.Size().Height;
            double nextThreshold = (foregroundType == ForegroundType.Dark) ? .01 : .99;//startingThreshold: rosszabbik iranybol kozelitunk
            double prevThreshold = 0.0;
            
            while (Math.Abs(nextThreshold - prevThreshold) > maxError)
            {
                double background = 0;
                int bCnt = 0;
                double foreground = 0;
                int fCnt = 0;
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        double level = Level(image[i, j]);
                        if (level < nextThreshold)
                        {
                            background += level;
                            bCnt++;
                        }
                        else
                        {
                            foreground += level;
                            fCnt++;
                        }
                    }
                }
                background /= bCnt;//avg
                foreground /= fCnt;//avg
                prevThreshold = nextThreshold;
                nextThreshold = ((background + foreground) / 2.0);
            }

            this.LogTrace("Binarization threshold: {nextThreshold}", nextThreshold);
            return nextThreshold;
        }

        /// <summary>
        /// Extracts the brightness of the input color. Ranges from 0.0 to 1.0
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private double Level(Rgba32 c)
        {
            double r = c.R / 255.0;
            double g = c.G / 255.0;
            double b = c.B / 255.0;
            double level;            
            if (foregroundType == ForegroundType.Dark)
            {
                level = 1.0 - (r + g + b) / 3.0;
            }
            else
            {
                level = (r + g + b) / 3.0;
            }
            return level;
        }
    }
}
