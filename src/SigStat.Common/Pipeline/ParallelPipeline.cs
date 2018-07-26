using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Common.Pipeline
{
    public class ParallelTransformPipeline : IEnumerable, ITransformation
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
            //TODO mindnek kovetni a progressét, a minimum lesz ezé is
            Parallel.ForEach(Items, (i) => i.Transform(signature));
        }
    }
}
