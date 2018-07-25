using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model
{
    public class ProcessingPipeline : IEnumerable
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
    }
}
