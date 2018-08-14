using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// input: X Y
    /// output: X Y
    /// </summary>
    public class Translate : SequentialTransformPipeline
    {
        public Translate(double xAdd, double yAdd)
        {
            Items = new List<ITransformation>()
            {
                new AddConst(xAdd).Input(Features.X),
                new AddConst(yAdd).Input(Features.Y),
            };

            this.Output(Features.X, Features.Y);

        }

        public Translate(FeatureDescriptor<List<double>> vectorFeature)
        {
            Items = new List<ITransformation>()
            {
                new AddVector(vectorFeature).Input(Features.X, Features.Y)
            };

            this.Output(Features.X, Features.Y);

        }
    }
}
