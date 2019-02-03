using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Extracts standard <see cref="Features"/> from sorted Components.
    /// <para>Default Pipeline Input: (List{List{PointF}}) Components</para>
    /// <para>Default Pipeline Output: X, Y, Button <see cref="Features"/></para>
    /// </summary>
    public class ComponentsToFeatures : PipelineBase, ITransformation
    {
        private readonly FeatureDescriptor<List<List<PointF>>> componentsFeature;

        /// <summary> Initializes a new instance of the <see cref="ComponentsToFeatures"/> class. </summary>
        public ComponentsToFeatures()
        {
            componentsFeature = FeatureDescriptor.Get<List<List<PointF>>>("Components");
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
                    if (p == c[0])  //direkt nem equals(), hanem ==
                    {
                        pendown.Add(false);
                    }
                    else
                    {
                        pendown.Add(true);
                    }
                }
            }

            signature.SetFeature(OutputFeatures[0], xs);
            signature.SetFeature(OutputFeatures[1], ys);
            signature.SetFeature(OutputFeatures[2], pendown);
            Logger.LogInformation($"X,Y,Button features extracted. Length: {xs.Count}");

        }
    }
}
