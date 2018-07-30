using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class WeightedClassifier : IEnumerable, IClassification
    {
        public List<(IClassification classifier, double weight)> Items = new List<(IClassification classifier, double weight)>();

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add((IClassification classifier, double weight) newitem)
        {
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
