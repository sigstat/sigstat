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
