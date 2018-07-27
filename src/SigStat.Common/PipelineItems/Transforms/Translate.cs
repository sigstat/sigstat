using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Translate : IEnumerable, ITransformation
    {
        //melyik feature-t mennyivel toljuk el
        private List<(FeatureDescriptor<List<double>> f, double t)> fts = new List<(FeatureDescriptor<List<double>> f, double t)>();

        //ezeket a featureoket ezzel toljuk el
        private readonly FeatureDescriptor<List<double>> byFeature;
        private List<FeatureDescriptor<List<double>>> fs = new List<FeatureDescriptor<List<double>>>();

        public Translate()
        {

        }

        public Translate(FeatureDescriptor<List<double>> byFeature)
        {
            this.byFeature = byFeature;
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
            if (byFeature != null)
            {//megadott feature alapjan (pl centroid)
                var by = signature.GetFeature(byFeature);
                for(int iF = 0; iF<fs.Count;iF++)
                {
                    var values = signature.GetFeature(fs[iF]);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] + by[iF];
                    signature.SetFeature(fs[iF], values);
                }
            }
            else
            {//megadott ertekek alapjan
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
