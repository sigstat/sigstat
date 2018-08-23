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
        private new void Add(ITransformation newitem) { }//TODO erre jobbat kitalalni, pl. pipeline ososztaly

        /// <summary> Initializes a new instance of the <see cref="CentroidTranslate"/> class.</summary>
        public CentroidTranslate()
        {
            Items = new List<ITransformation>()
            {
                new CentroidExtraction
                {
                    Features.X,
                    Features.Y
                },
                new Multiply(-1.0),
                new Translate(FeatureDescriptor<List<double>>.Descriptor("Centroid")).Input(Features.X, Features.Y)
            };

            this.Output(Features.X, Features.Y);

        }

    }
}
