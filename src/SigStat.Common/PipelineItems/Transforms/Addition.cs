using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Addition : IEnumerable, ITransformation
    {
        //melyik feature-t mennyivel toljuk el
        private List<(FeatureDescriptor<List<double>> f, double t)> fts = new List<(FeatureDescriptor<List<double>> f, double t)>();

        //ezeket a featureoket ezzel toljuk el
        private List<FeatureDescriptor<List<double>>> fs = new List<FeatureDescriptor<List<double>>>();
        private readonly FeatureDescriptor<List<double>> byValues;//ez olyan hosszu, mint fs
        private readonly FeatureDescriptor<double> byValue;//vagy ezzel az eggyel

        public Addition()
        {

        }

        public Addition(FeatureDescriptor<List<double>> byValues)
        {
            this.byValues = byValues;
        }

        public Addition(FeatureDescriptor<double> byValue)
        {
            this.byValue = byValue;
        }

        public IEnumerator GetEnumerator()
        {
            return fts.GetEnumerator();
        }

        public void Add((FeatureDescriptor<List<double>> f, double t) newitem)
        {
            fts.Add(newitem);
        }

        public void Add(FeatureDescriptor<List<double>> newitem)
        {
            fs.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            if(byValue!=null)
            {//megadott feature ertek alapjan mindet, pl. time reset
                var by = signature.GetFeature(byValue);
                for (int iF = 0; iF < fs.Count; iF++)
                {
                    var values = signature.GetFeature(fs[iF]);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] + by;
                    signature.SetFeature(fs[iF], values);
                }
            }
            else if (byValues != null)
            {//megadott feature ertekek alapjan egyenkent (pl centroid.xy)
                var by = signature.GetFeature(byValues);
                for (int iF = 0; iF < fs.Count; iF++)
                {
                    var values = signature.GetFeature(fs[iF]);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] + by[iF];
                    signature.SetFeature(fs[iF], values);
                }
            }
            else
            {//megadott konstans ertekek alapjan
                foreach (var ft in fts)
                {
                    var values = signature.GetFeature(ft.f);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] + ft.t;
                    signature.SetFeature(ft.f, values);
                }
            }
        }
    }
}
