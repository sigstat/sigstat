using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SigStat.Common.PipelineItems.Classifiers
{
    /// <summary>
    /// Classifies Signatures by weighing other Classifier results.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class WeightedClassifier : PipelineBase, IEnumerable, IClassifier
    {
        /// <summary>List of classifiers and belonging weights.</summary>
        
        public List<(IClassifier classifier, double weight)> Items = new List<(IClassifier classifier, double weight)>();

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>Add a new classifier with given weight to the list of items.</summary>
        /// <param name="newItem">Classifier with belonging weight.</param>
        public void Add((IClassifier classifier, double weight) newItem)
        {
            Items.Add(newItem);
        }


        /// <inheridoc/>
        public ISignerModel Train(List<Signature> signatures)
        {
            //double score = 0;
            //for (int i = 0; i < Items.Count; i++)
            //{
            //    score += Items[i].classifier.Pair(signature1, signature2) * Items[i].weight;
            //}
            //this.Log($"Paired SigID {signature1.ID} with SigID {signature2.ID}");
            //Logger?.LogTrace($"Pairing result of SigID {signature1.ID} with SigID {signature2.ID}: {score}");
            //return score;
            throw new NotImplementedException();
        }

        /// <inheridoc/>
        public double Test(ISignerModel model, Signature signature)
        {
            throw new NotImplementedException();
        }
    }
}
