using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class Multiply : PipelineBase, IEnumerable, ITransformation
    {
        private readonly double byConst;

        public Multiply(double byConst)
        {
            this.byConst = byConst;
        }

        public IEnumerator GetEnumerator()
        {
            return InputFeatures.GetEnumerator();
        }

        public void Add(FeatureDescriptor newItem)
        {
            InputFeatures.Add(newItem);
        }

        public void Transform(Signature signature)
        {
            foreach (var f in InputFeatures)
            {
                if (f.IsCollection)
                {
                    var values = signature.GetFeature<List<double>>(f);
                    for (int i = 0; i < values.Count; i++)
                        values[i] = values[i] * byConst;
                    signature.SetFeature(f, values);
                    this.Output(f);
                }
                else
                {
                    var values = signature.GetFeature<double>(f);
                    values = values * byConst;
                    signature.SetFeature(f, values);
                    this.Output(f);
                }
                Progress += 100 / InputFeatures.Count;
            }
            
        }
    }
}
