using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Pipeline
{
    public class SequentialTransformPipeline : IEnumerable, ITransformation
    {
        public List<ITransformation> Items = new List<ITransformation>();

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add(ITransformation newitem)
        {
            Items.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            //TODO: progress szamolo
            Items.ForEach(i => i.Transform(signature));
        }
    }
}
