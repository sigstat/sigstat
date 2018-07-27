using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Multiply : IEnumerable, ITransformation
    {
        //melyik feature-t mennyivel szorozzuk
        private List<(FeatureDescriptor<double> f, double m)> fms = new List<(FeatureDescriptor<double> f, double m)>();

        public IEnumerator GetEnumerator()
        {
            return fms.GetEnumerator();
        }

        public void Add((FeatureDescriptor<double> f, double t) newitem)
        {
            fms.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            //ez igy faj, de refaktor -> igazabol ez ket kulon muvelet kene hogy legyen

            /*foreach (var fm in fms)
            {
                if (fm.f.IsCollection)
                {
                    var values = signature.GetFeature(fm.f);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] * fm.m;
                    signature.SetFeature(fm.f, values);
                }
                else
                {
                    var values = signature.GetFeature(fm.f);
                    values = values * fm.m;
                    signature.SetFeature(fm.f, values);
                }
            }*/
            
        }
    }
}
