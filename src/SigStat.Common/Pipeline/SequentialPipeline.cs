using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public new Logger Logger { get => _logger;
            set {
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
            Items.Add(newItem);
        }

        public void Transform(Signature signature)
        {
            for(int i=0;i<Items.Count;i++)
            {
                Items[i].Transform(signature);
                Progress = (int)(i / (double)(Items.Count - 1) * 100.0);
            }
        }
    }
}
