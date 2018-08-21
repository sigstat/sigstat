using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// Extracts standard <see cref="Features"/> from sorted Components.
    /// <para>Default Pipeline Input: (bool[,]) Components</para>
    /// <para>Default Pipeline Output: X, Y, Button <see cref="Features"/></para>
    /// </summary>
    public class ComponentsToFeatures : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<List<List<PointF>>> componentsFeature;

        /// <summary> Initializes a new instance of the <see cref="ComponentsToFeatures"/> class. </summary>
        public ComponentsToFeatures()
        {
            componentsFeature = FeatureDescriptor<List<List<PointF>>>.Descriptor("Components");
            this.Output(
                Features.X,
                Features.Y,
                Features.Button
                );
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var components = signature.GetFeature(componentsFeature);

            List<double> xs = new List<double>();
            List<double> ys = new List<double>();
            List<bool> pendown = new List<bool>();
            foreach (var c in components)
            {
                foreach (var p in c)
                {
                    xs.Add(p.X);
                    ys.Add(p.Y);
                    if (p == c[0])//direkt nem equals(), hanem ==
                        pendown.Add(false);
                    else
                        pendown.Add(true);
                }
                Progress += (int)(1.0 / components.Count * 100);
            }

            signature.SetFeature(OutputFeatures[0], xs);
            signature.SetFeature(OutputFeatures[1], ys);
            signature.SetFeature(OutputFeatures[2], pendown);
            Progress = 100;
            Log(LogLevel.Info, $"X,Y,Button features extracted. Length: {xs.Count}");

        }
    }
}
