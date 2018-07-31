using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Find minimum and maximum values of given feature, add them as new features
    /// </summary>
    public class Extrema : ITransformation
    {
        private readonly FeatureDescriptor<List<double>> f;
        private readonly FeatureDescriptor<List<double>> minfd;
        private readonly FeatureDescriptor<List<double>> maxfd;

        public Extrema(FeatureDescriptor<List<double>> f, string minFeatureName, string maxFeatureName)
        {
            this.f = f;
            //itt letre kell hozni azt a feature descriptort, amit ki fog szamolni. 
            //Kulonben a kesobbi pipeline elemek inicializalasanal nem talalnank.
            //TODO: ezt talan lehetne automatizalni: Ha olyan feature descriptort kerunk le ami nincs, akkor letrehozzuk
            minfd = FeatureDescriptor<List<double>>.Descriptor(minFeatureName);
            maxfd = FeatureDescriptor<List<double>>.Descriptor(maxFeatureName);
        }

        public void Transform(Signature signature)
        {
            List<double> values = signature.GetFeature(f);
            //find min and max values
            double min = values.Min();
            double max = values.Max();

            signature[minfd] = new List<double> { min };//proba: minden featureben lehessen több érték, akkor is ha csak 1-et tarolunk
            signature[maxfd] = new List<double> { max };
        }
    }
}
