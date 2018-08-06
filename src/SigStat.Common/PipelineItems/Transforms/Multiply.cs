using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Multiply : PipelineBase, IEnumerable, ITransformation
    {
        //melyik feature-t mennyivel szorozzuk
        private List<(FeatureDescriptor f, double m)> fms = new List<(FeatureDescriptor f, double m)>();

        public IEnumerator GetEnumerator()
        {
            return fms.GetEnumerator();
        }

        public void Add((FeatureDescriptor f, double t) newItem)
        {
            fms.Add(newItem);
        }

        public void Transform(Signature signature)
        {
            foreach (var fm in fms)
            {
                if (fm.f.IsCollection)
                {
                    var values = signature.GetFeature<List<double>>(fm.f);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] * fm.m;
                    signature[fm.f] = values;
                }
                else
                {
                    var values = signature.GetFeature<double>(fm.f);
                    values = values * fm.m;
                    signature[fm.f] = values;
                }
                Progress += 100/fms.Count;
            }
            
        }
    }
}
