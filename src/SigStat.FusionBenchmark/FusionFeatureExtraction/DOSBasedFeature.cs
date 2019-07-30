using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using Newtonsoft.Json;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    class DOSBasedFeature: PipelineBase, ITransformation
    {
        public static double M { get; set; }

        public static double W { get; set; }

        static readonly private double invalidValue = -10.0;

        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        [Output("CurvatureProfile")]
        public FeatureDescriptor<List<double>> OutputCurvature { get; set; }

        List<double> xs;
        List<double> ys;
        List<bool> bs;

        public DOSBasedFeature(double m = 2.0, double w = 10.0)
        {
            M = m;
            W = w;
        }

        public void Transform(Signature signature)
        {
            this.LogInformation("DOSBasedFeature transform started.");
            xs = signature.GetFeature<List<double>>(InputX);
            ys = signature.GetFeature<List<double>>(InputY);
            bs = signature.GetFeature<List<bool>>(InputButton);
            var ts = new List<double>();
            var firstSegment = new Section(new PointD(), new PointD());
            var secondSegment = new Section(new PointD(), new PointD());
            var pauseSegment = new Section(new PointD(), new PointD());

            for (int i = 0; i < bs.Count; i++)
            {
                if (bs[i])
                {
                    int j = i;
                    j = FindSection(firstSegment, j, W);
                    j = FindSection(pauseSegment, j, M);
                    j = FindSection(secondSegment, j, W);
                    if (j < xs.Count && firstSegment.Length() < 3 * W &&
                        secondSegment.Length() < 3 * W && pauseSegment.Length() < 3 * W)
                        ts.Add(CalculateDiffAngle(firstSegment, secondSegment));
                }
                else
                {
                    ts.Add(invalidValue);
                }
            }
            signature.SetFeature<List<double>>(OutputCurvature, ts);
            this.LogInformation("DOSBasedFeature transform finished.");
        }

        /*public static double CalculateAngle(Section section)
        {
            double dx = section.End.X - section.Start.X;
            double dy = section.End.Y - section.Start.Y;
            double res = Math.Atan2(dy, dx);
            return res;
        }*/

        private static void NormalizeSection(Section section)
        {
            section.End.SetXY(section.End.X - section.Start.X, section.End.Y - section.Start.Y);
            section.Start.SetXY(0.0, 0.0);
            double length = section.Length();
            section.End.SetXY(section.End.X / length, section.End.Y / length);
        }
        

        public static double CalculateDiffAngle(Section first, Section second)
        {
            NormalizeSection(first);
            NormalizeSection(second);
            double distance = Math.Sqrt((first.End.Y - second.End.Y) * (first.End.Y - second.End.Y)
                                        + (first.End.X - second.End.X) * (first.End.X - second.End.X));
            double res = 2 * Math.Asin(distance / 2);
            return res;
        } 

        private int FindSection(Section section, int idx, double thres)
        {
            if (idx >= xs.Count)
                return idx;
            section.Start.SetXY(xs[idx], ys[idx]);
            section.End.SetXY(xs[idx], ys[idx]);
            int j;
            for (j = idx + 1; (j < xs.Count && section.Length() < thres); j++)
            {
                section.End.SetXY(xs[j], ys[j]);
            }
            return j;
        }

    }
}