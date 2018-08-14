using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Custom transzformáció T (time) featureon, az alábbi pipeline elemekből:
    /// Extrema, Multiply, Addition.
    /// Eredmeny: 0. idopontbol indulnak az adatok
    /// </summary>
    public class TimeReset : SequentialTransformPipeline
    {
        private new void Add(ITransformation newitem) { }//TODO erre jobbat kitalalni, pl. pipeline ososztaly

        public TimeReset()
        {
            var negMin = FeatureDescriptor<List<double>>.Descriptor("NegMin");
            Items = new List<ITransformation>()
            {
                new Extrema().Input(Features.T),//find minimum
                new Multiply(-1.0).Output(negMin, null),//negate
                new AddVector(negMin).Input(Features.T),//add the negated value
            };

            this.Output(Features.T);
        }

    }
}
