using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class Binarization : PipelineBase, ITransformation
    {

        public enum ForegroundType
        {
            Dark,
            Bright
        }

        private double binThreshold = -1;//0-1 //-1: iterative
        private readonly ForegroundType foregroundType = ForegroundType.Dark;

        /// <summary>
        /// threshold automatikusan lesz kiszamolva, sotet eloter, vilagos hatter
        /// </summary>
        public Binarization()
        {
            this.Output(FeatureDescriptor<bool[,]>.Descriptor("Binarized"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foregroundType"></param>
        /// <param name="binThreshold">0-1</param>
        public Binarization(ForegroundType foregroundType, double binThreshold)
        {
            this.foregroundType = foregroundType;
            this.binThreshold = binThreshold;
        }

        public void Transform(Signature signature)
        {
            Image<Rgba32> image = signature.GetFeature<Image<Rgba32>>(InputFeatures[0]);
            int w = image.Size().Width;
            int h = image.Size().Height;

            if (binThreshold < 0)//find threshold if not specified
                binThreshold = IterativeThreshold(image, 0.008);

            //binarize
            bool[,] b = new bool[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                    b[i, j] = (Level(image[i, j]) > binThreshold);
                Progress += (int)((1.0 / w)*100);
            }

            Log(LogLevel.Info, "Binarization done.");
            signature.SetFeature(OutputFeatures[0], b);
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
                background /= bCnt;//avg
                foreground /= fCnt;//avg
                prevThreshold = nextThreshold;
                nextThreshold = ((background + foreground) / 2.0);
            }

            Log(LogLevel.Debug, $"Binarization threshold: {nextThreshold}");
            return nextThreshold;
        }

        /// <summary>
        /// 0 - 1 kozotti ertek
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private double Level(Rgba32 c)
        {
            double r = c.R / 255.0;
            double g = c.G / 255.0;
            double b = c.B / 255.0;
            double level;
            //if (t == ForegroundType.Blue)//kek sulyozas ( es hatter feher)
            //    level = b - (r + g) / 2.0;//ez nem nagyon mukodik
            //else
            if (foregroundType == ForegroundType.Dark)
                level = 1.0 - (r + g + b) / 3.0;
            else //if (t == ForegroundType.Bright)
                level = (r + g + b) / 3.0;
            return level;
        }
    }
}
