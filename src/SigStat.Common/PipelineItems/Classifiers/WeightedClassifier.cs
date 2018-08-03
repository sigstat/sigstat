using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class WeightedClassifier : PipelineBase, IEnumerable, IClassification
    {
        public List<(IClassification classifier, double weight)> Items = new List<(IClassification classifier, double weight)>();

        private Logger _logger;
        public new Logger Logger
        {
            get => _logger;
            set
            {
                _logger = value;
                Items.ForEach(i => i.classifier.Logger = _logger);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add((IClassification classifier, double weight) newitem)
        {
            if (_logger != null)
                newitem.classifier.Logger = _logger;
            Items.Add(newitem);
        }

        public double Pair(Signature signature1, Signature signature2)
        {
            //TODO: sulyokat normalizalni pl

            double score = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                score += Items[i].classifier.Pair(signature1, signature2) * Items[i].weight;
                //TODO: progress szamolas
            }
            return score;
        }

    }
}
