using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Pipeline
{
    // TODO: Add() nem kene hogy latszodjon leszarmazottakban, kell egy koztes dolog

    /// <summary>
    /// Runs pipeline items in a sequence.
    /// <para>Default Pipeline Output: Output of the last Item in the sequence.</para>
    /// </summary>
    public class SequentialTransformPipeline : PipelineBase, IEnumerable, ITransformation
    {
        /// <summary>List of transforms to be run in sequence.</summary>
        public List<ITransformation> Items { get; set; }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Add new transform to the list. 
        /// </summary>
        /// <param name="newItem"></param>
        public void Add(ITransformation newItem)
        {
            Items.Add(newItem);

            //Set sequence output to last item's output (if given)
            if(newItem.OutputFeatures!=null)
            {
                this.Output(newItem.OutputFeatures.ToArray());
            }
        }

     

        /// <summary>
        /// Executes transform <see cref="Items"/> in sequence.
        /// Passes input features for each.
        /// Output is the output of the last Item in the sequence.
        /// </summary>
        /// <param name="signature">Signature to execute transform on.</param>
        public void Transform(Signature signature)
        {
            if (Items == null || Items.Count == 0)
            {
                return;
            }
            // Do the first transformation
            Items[0].Transform(signature);

            for (int i = 1; i < Items.Count; i++)
            {
                if (Items[i].InputFeatures == null || Items[i].InputFeatures.Count==0)//pass previously calculated features if input not specified
                {
                    Items[i].InputFeatures = new List<FeatureDescriptor>(Items[i - 1].OutputFeatures);
                }

                Items[i].Transform(signature);
            }
        }
    }
}
