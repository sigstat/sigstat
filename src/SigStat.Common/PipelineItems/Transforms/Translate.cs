using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Translate : SequentialTransformPipeline
    {
        //ezeket a featureoket ezzel toljuk el
        private readonly FeatureDescriptor<List<double>> byFeature;
        private List<FeatureDescriptor<List<double>>> fs = new List<FeatureDescriptor<List<double>>>();

        public Translate(double xadd, double yadd)
        {
            Items = new List<ITransformation>()
            {
                new Addition
                {
                    (Features.X, xadd),
                    (Features.Y, yadd)
                }
            };
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
        }
    }
}
