using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Common.Pipeline
{
    public class ParallelPipeline : IEnumerable, IPipelineItem
    {
        public List<IPipelineItem> Items = new List<IPipelineItem>();

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add(IPipelineItem newitem)
        {
            Items.Add(newitem);
        }

        public void Run(Signature input)
        {
            //TODO mindnek kovetni a progressét, a minimum lesz ezé is
            Parallel.ForEach(Items, (i) => i.Run(input));
        }
    }
}
