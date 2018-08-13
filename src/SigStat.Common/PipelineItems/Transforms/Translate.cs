using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Translate : SequentialTransformPipeline
    {
        public Translate(double xAdd, double yAdd)
        {
            Items = new List<ITransformation>()
            {
                new Addition
                {
                    (Features.X, xAdd),
                    (Features.Y, yAdd)
                }
            };

            //Output of last transform is the output of the sequence
            this.Output(Items.Last().OutputFeatures.ToArray());

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

            //Output of last transform is the output of the sequence
            this.Output(Items.Last().OutputFeatures.ToArray());

        }
    }
}
