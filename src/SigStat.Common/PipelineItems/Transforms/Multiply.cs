using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Multiply : IEnumerable, ITransformation
    {
        //melyik feature-t mennyivel szorozzuk
        private List<(FeatureDescriptor<List<double>> f, double m)> fms = new List<(FeatureDescriptor<List<double>> f, double m)>();

        public IEnumerator GetEnumerator()
        {
            return fms.GetEnumerator();
        }

        public void Add((FeatureDescriptor<List<double>> f, double t) newitem)
        {
            fms.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            foreach (var fm in fms)
            {
                var values = signature.GetFeature(fm.f);
                for (int i = 0; i < values.Count; i++)
                    values[i] = values[i] * fm.m;
                signature.SetFeature(fm.f, values);
            }
        }
    }
}
