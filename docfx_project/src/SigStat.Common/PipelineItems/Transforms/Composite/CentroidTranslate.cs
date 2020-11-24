using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Sequential pipeline to translate X and Y <see cref="Features"/> to Centroid.
    /// The following Transforms are called: <see cref="CentroidExtraction"/>, <see cref="Multiply"/>(-1), <see cref="Translate"/>
    /// <para>Default Pipeline Input: <see cref="Features.X"/>, <see cref="Features.Y"/></para>
    /// <para>Default Pipeline Output: (List{double}) Centroid</para>
    /// </summary>
    /// <remarks>This is a special case of <see cref="Translate"/></remarks>

    [JsonObject(MemberSerialization.OptOut)]
    public class CentroidTranslate : SequentialTransformPipeline
    {

        /// <summary>
        /// Gets or sets the input feature representing the X coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the input feature representing the Y coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; } = Features.Y;

        /// <summary>
        /// Gets or sets the output feature representing the X coordinates of an online signature
        /// </summary>
        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the output feature representing the X coordinates of an online signature
        /// </summary>
        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; } = Features.Y;

        /// <summary> Initializes a new instance of the <see cref="CentroidTranslate"/> class.</summary>
        public CentroidTranslate()
        {
            var C = FeatureDescriptor<List<double>>.Get("Centroid");//TODO: Register()
            Items = new List<ITransformation>
            {
                new CentroidExtraction { Inputs = new List<FeatureDescriptor<List<double>>>(){InputX, InputY }, OutputCentroid=C },
                new Multiply(-1.0),
                new Translate(C) { OutputX = OutputX, OutputY = OutputY }
            };
        }

    }
}
