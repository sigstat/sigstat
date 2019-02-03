using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Runs pipeline items in parallel.
    /// <para>Default Pipeline Output: Range of all the Item outputs.</para>
    /// </summary>
    public class ParallelTransformPipeline : PipelineBase, IEnumerable, ITransformation
    {
        /// <summary>List of transforms to be run parallel.</summary>
        private List<ITransformation> items = new List<ITransformation>();

        /// <summary>Gets the minimum progess of all the child items.</summary>

        public List<ITransformation> Items { get => items; set => items = value; }

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
        }

        /// <summary>
        /// Executes transform <see cref="Items"/> parallel.
        /// Passes input features for each.
        /// Output is a range of all the Item outputs.
        /// </summary>
        /// <param name="signature">Signature to execute transform on.</param>
        public void Transform(Signature signature)
        {
            Parallel.ForEach(Items, (i) => {
                if (i.InputFeatures == null)//pass previously calculated features if input not specified
                {
                    i.InputFeatures = this.InputFeatures;
                }

                i.Transform(signature);
                });


            //Add the new item's output to output
            OutputFeatures = new List<FeatureDescriptor>();
            foreach (var item in Items)
            {
                OutputFeatures.AddRange(item.OutputFeatures);
            }
        }
    }
}
