using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class CogShiftTransform : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Input]
        public FeatureDescriptor<Point> InputCog { get; set; }

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; }

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; }

        public void Transform(Signature signature)
        {
            var xs = signature.GetFeature<List<double>>(InputX);
            var ys = signature.GetFeature<List<double>>(InputY);
            var cog = signature.GetFeature<Point>(InputCog);
            double dx = (double)(-cog.X);
            double dy = (double)(-cog.Y);

            int n = xs.Count;
            if (n != xs.Count || n != ys.Count)
            {
                throw new Exception();
            }
            for (int i = 0; i < n; i++)
            {
                xs[i] = xs[i] + dx;
                ys[i] = ys[i] + dy;
            }

            signature.SetFeature<List<double>>(OutputX, xs);
            signature.SetFeature<List<double>>(OutputY, ys);
        }
    }
}
