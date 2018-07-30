using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// sulypont kiszamolasa, hozzaadas a Featureokhoz.
    /// Hasznos ezutan pl Translate a Centroidba.
    /// </summary>
    public class CentroidExtraction : IEnumerable, ITransformation
    {
        public List<FeatureDescriptor<List<double>>> fs = new List<FeatureDescriptor<List<double>>>();

        private FeatureDescriptor<List<double>> centroidfd;//akar lehetne egy gettere is

        public CentroidExtraction()
        {
            //itt letre kell hozni azt a feature descriptort, amit ki fog szamolni. 
            //Kulonben a kesobbi pipeline elemek inicializalasanal nem talalnank.
            //TODO: ezt talan lehetne automatizalni: Ha olyan feature descriptort kerunk le ami nincs, akkor letrehozzuk
            centroidfd = new FeatureDescriptor<List<double>>("Centroid", "Centroid");

        }

        public IEnumerator GetEnumerator()
        {
            return fs.GetEnumerator();
        }

        public void Add(FeatureDescriptor<List<double>> newitem)
        {
            fs.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            List<double> c = new List<double>(fs.Count);
            foreach (var f in fs)
            {
                var values = signature.GetFeature(f);
                double avg = 0;
                values.ForEach((d) => {
                    avg += d;
                });
                avg /= values.Count;
                c.Add(avg);

            }

            signature[centroidfd] = c;
            //signature.SetFeature(centroidfd, c);
            //signature["Centroid"] = c;
        }
    }
}
