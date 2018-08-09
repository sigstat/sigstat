using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class ComponentsToFeatures : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<List<List<PointF>>> componentsFeature;

        public ComponentsToFeatures()
        {
            componentsFeature = FeatureDescriptor<List<List<PointF>>>.Descriptor("Components");
        }

        public void Transform(Signature signature)
        {
            var components = signature.GetFeature(componentsFeature);

            List<double> xs = new List<double>();
            List<double> ys = new List<double>();
            List<int> pendown = new List<int>();//TODO: ez miert nem bool
            foreach (var c in components)
            {
                foreach (var p in c)
                {
                    xs.Add(p.X);
                    ys.Add(p.Y);
                    if (p == c[0])//direkt nem equals(), hanem ==
                        pendown.Add(0);
                    else
                        pendown.Add(1);
                }
                Progress += (int)(1.0 / components.Count * 100);
            }

            signature.SetFeature(Features.X, xs);
            signature.SetFeature(Features.Y, ys);
            signature.SetFeature(Features.Button, pendown);
            Progress = 100;
            Log(LogLevel.Info, $"X,Y,Button features extracted. Length: {xs.Count}");

        }
    }
}
