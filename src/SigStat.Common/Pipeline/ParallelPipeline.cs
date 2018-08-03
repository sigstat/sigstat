using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.Common.Pipeline
{
    public class ParallelTransformPipeline : PipelineBase, IEnumerable, ITransformation
    {
        public List<ITransformation> Items = new List<ITransformation>();

        private Logger _logger;
        public new Logger Logger
        {
            get => _logger;
            set
            {
                _logger = value;
                Items.ForEach(i => i.Logger = _logger);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add(ITransformation newitem)
        {
            if (_logger != null)
                newitem.Logger = _logger;
            Items.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            //TODO mindnek kovetni a progressét, a minimum lesz ezé is
            Parallel.ForEach(Items, (i) => i.Transform(signature));
        }
    }
}
