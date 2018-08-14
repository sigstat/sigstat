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

        public Translate(FeatureDescriptor<List<double>> byFeature)
        {
            Items = new List<ITransformation>()
            {
                new Addition(byFeature)//emiatt kell az Additionnak több Featuret egyszerre is kezelnie: itt a konstruktorban nem kérhetjük le még a byFeature értékét, mert jó eséllyel nem létezik még
                {
                    Features.X,
                    Features.Y
                }
            };

            this.Output(Features.X, Features.Y);

        }
    }
}
