using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Classifiers
{
    public class WeightedClassifier : IEnumerable, IClassification
    {
        public List<(IClassification classifier, double weight)> Items = new List<(IClassification classifier, double weight)>();
        private List<Signature> training = new List<Signature>();

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add((IClassification classifier, double weight) newitem)
        {
            Items.Add(newitem);
        }

        public double Test(Signature signature)
        {
            //TODO: sulyokat normalizalni pl

            double score=0;
            for(int i=0;i<Items.Count;i++)
            {
                score += Items[i].classifier.Test(signature) * Items[i].weight;
                //TODO: progress szamolas
            }
            return score;
        }

        public void Train(IEnumerable<Signature> signatures)
        {
            training.Clear();
            training.AddRange(signatures);
            for(int i=0;i<Items.Count;i++)
            {
                Items[i].classifier.Train(training);
                //TODO: progress szamolas
            }
            //TODO: calculate limit, etc. here
        }
    }
}
