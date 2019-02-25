using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Sequential pipeline to translate X and Y <see cref="Features"/> to Centroid.
    /// The following Transforms are called: <see cref="CentroidExtraction"/>, <see cref="Multiply"/>(-1), <see cref="Translate"/>
    /// <para>Default Pipeline Input: <see cref="Features.X"/>, <see cref="Features.Y"/></para>
    /// <para>Default Pipeline Output: (List{double}) Centroid</para>
    /// </summary>
    /// <remarks>This is a special case of <see cref="Translate"/></remarks>
    public class CentroidTranslate : SequentialTransformPipeline
    {

        [Input]
        public FeatureDescriptor<List<double>> InputX = Features.X;

        [Input]
        public FeatureDescriptor<List<double>> InputY = Features.Y;

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX = Features.X;

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY = Features.Y;

        /// <summary> Initializes a new instance of the <see cref="CentroidTranslate"/> class.</summary>
        public CentroidTranslate()
        {
            var C = FeatureDescriptor<List<double>>.Get("Centroid");//TODO: Register()
            Items = new List<ITransformation>
            {
                new CentroidExtraction { Inputs = {InputX, InputY }, OutputCentroid=C },
                new Multiply(-1.0),
                new Translate(C) { OutputX = OutputX, OutputY = OutputY }
            };
        }

    }
}
