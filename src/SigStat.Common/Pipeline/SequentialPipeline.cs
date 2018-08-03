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

        public void Add(ITransformation newitem)
        {
            if (_logger != null)
                newitem.Logger = _logger;
            Items.Add(newitem);
        }

        public void Transform(Signature signature)
        {
            //TODO: progress szamolo
            Items.ForEach(i => i.Transform(signature));
        }
    }
}
