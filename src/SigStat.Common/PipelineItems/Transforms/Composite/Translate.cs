using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Sequential pipeline to translate X and Y <see cref="Features"/> by specified vector (constant or feature).
    /// The following Transforms are called: <see cref="AddConst"/> twice, or <see cref="AddVector"/>.
    /// <para>Default Pipeline Input: <see cref="Features.X"/>, <see cref="Features.Y"/></para>
    /// <para>Default Pipeline Output: <see cref="Features.X"/>, <see cref="Features.Y"/></para>
    /// </summary>
    public class Translate : SequentialTransformPipeline
    {
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; } = Features.X;

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; } = Features.Y;

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; } = Features.X;

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; } = Features.Y;


        /// <param name="xAdd">Value to translate <see cref="Features.X"/> by.</param>
        /// <param name="yAdd">Value to translate <see cref="Features.Y"/> by.</param>
        public Translate(double xAdd, double yAdd)
        {
            OutputX = InputX;
            OutputY = InputY;
            Items = new List<ITransformation>
            {
                new AddConst(xAdd) { InputList = InputX },
                new AddConst(yAdd) { InputList = InputY },
            };

        }

        /// <param name="vectorFeature">Feature to translate X and Y by.</param>
        public Translate(FeatureDescriptor<List<double>> vectorFeature)
        {
            Items = new List<ITransformation>
            {
                new AddVector(vectorFeature) { Inputs = { Features.X, Features.Y } }
            };

        }
    }
}
