using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// ...
    /// Ebbol jo dolog leszarmazni es az Itemeket az adott feladatszerint inicializalni
    /// pl. CentroidTranslate: CentroidExtraction + Multiply -1 + Translate
    /// TODO: Add() nem kene hogy latszodjon leszarmazottakban, kell egy koztes dolog
    /// </summary>
    public class SequentialTransformPipeline : PipelineBase, IEnumerable, ITransformation
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

        public void Add(ITransformation newItem)
        {
            if (_logger != null)
                newItem.Logger = _logger;
            newItem.ProgressChanged += ((o, p) => calcProgress(i,p));
            Items.Add(newItem);

            //Set sequence output to last item's output (if given)
            if(newItem.OutputFeatures!=null)
                this.Output(newItem.OutputFeatures.ToArray());
        }

        private void calcProgress(int i, int p)
        {
            double m = p / 100.0;
            Progress = (int)((i*(1-m)+(i+1)*m) / (Items.Count) * 100.0);
        }

        int i=0;
        public void Transform(Signature signature)
        {
            for (i = 0; i < Items.Count; i++)
            {
                //try
                //{
                if (Items[i].InputFeatures == null && i > 0)//pass previously calculated features if input not specified
                    Items[i].InputFeatures = Items[i - 1].OutputFeatures;
                Items[i].Transform(signature);
                //}
                //catch (Exception exc)
                //{
                //    throw PipelineException("Error while executing {pipelineItem.Type} with signature {sig.ToString()}", exc);
                //}

                //Progress = (int)(i / (double)(Items.Count - 1) * 100.0);
            }
        }
    }
}
