using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Extracts standard <see cref="Features"/> from sorted Components.
    /// <para>Default Pipeline Input: (List{List{PointF}}) Components</para>
    /// <para>Default Pipeline Output: X, Y, Button <see cref="Features"/></para>
    /// </summary>
    public class ComponentsToFeatures : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<List<PointF>>> InputComponents;

        [Output("X")]
        public FeatureDescriptor<List<double>> X;

        [Output("Y")]
        public FeatureDescriptor<List<double>> Y;

        [Output("Button")]
        public FeatureDescriptor<List<bool>> Button;

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var components = signature.GetFeature(InputComponents);

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

            signature.SetFeature(X, xs);
            signature.SetFeature(Y, ys);
            signature.SetFeature(Button, pendown);
            this.Log($"X,Y,Button features extracted. Length: {xs.Count}");

        }
    }
}
