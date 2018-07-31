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
            Items = new List<ITransformation>()
            {
                new Extrema(Features.T, "TMin", "TMax"),
                new Multiply
                {
                    (FeatureDescriptor<List<double>>.Descriptor("TMin"),-1.0)
                },
                new Addition(FeatureDescriptor<List<double>>.Descriptor("TMin"))
                {
                    Features.T
                },
                new Multiply
                {//ezt ugy is lehetne pl hogy egy TMin_Neg featuret hozzaadunk, de ugyse kell kesobb..
                    (FeatureDescriptor<List<double>>.Descriptor("TMin"),-1.0)
                },
            };
        }

    }
}
