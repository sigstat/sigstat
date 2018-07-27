using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Translate : IEnumerable, ITransformation
    {
        //melyik feature-t mennyivel toljuk el
        public List<(FeatureDescriptor<List<double>> f, double t)> fts = new List<(FeatureDescriptor<List<double>> f, double t)>();
        
        public Translate()
        {

        }

        public IEnumerator GetEnumerator()
        {
            return fts.GetEnumerator();
        }

        public void Add((FeatureDescriptor<List<double>> f, double t) newitem)
        {
            fts.Add(newitem);
        }

        public void Transform(Signature signature)
        {
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
