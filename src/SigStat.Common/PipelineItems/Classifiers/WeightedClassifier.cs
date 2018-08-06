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

        public void Add((IClassification classifier, double weight) newItem)
        {
            if (_logger != null)
                newItem.classifier.Logger = _logger;
            Items.Add(newItem);
        }

        public double Pair(Signature signature1, Signature signature2)
        {
            //TODO: sulyokat normalizalni pl

            double score = 0;
            Progress = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                score += Items[i].classifier.Pair(signature1, signature2) * Items[i].weight;
                Progress = (int)(i / (double)(Items.Count - 1) * 100.0);
            }
            Log(LogLevel.Info, $"Paired SigID {signature1.ID} with SigID {signature2.ID}");
            Log(LogLevel.Debug, $"Pairing result of SigID {signature1.ID} with SigID {signature2.ID}: {score}");
            Progress = 100;
            return score;
        }

    }
}
