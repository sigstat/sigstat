using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Custom transzformáció X és Y featureon, az alábbi pipeline elemekből:
    /// CentroidExtraction, Multiply -1, Translate
    /// </summary>
    public class CentroidTranslate : SequentialTransformPipeline
    {
        private new void Add(ITransformation newitem) { }//TODO erre jobbat kitalalni, pl. pipeline ososztaly

        public CentroidTranslate()
        {
            Items = new List<ITransformation>()
            {
                new CentroidExtraction
                {
                    Features.X,
                    Features.Y
                },
                new Multiply
                {
                    (FeatureDescriptor<List<double>>.GetDescriptor("Centroid"), -1.0)//TODO ez tul hosszu
                },
                new Translate(FeatureDescriptor<List<double>>.GetDescriptor("Centroid"))
            };
        }

    }
}
