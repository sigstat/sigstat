using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace SigStat.Common.Pipeline
{
    /// <summary>
    /// Runs pipeline items in parallel.
    /// <para>Default Pipeline Output: Range of all the Item outputs.</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ParallelTransformPipeline : PipelineBase, IEnumerable, ITransformation
    {
        /// <summary>List of transforms to be run parallel.</summary>
        
        public List<ITransformation> Items = new List<ITransformation>();
        public override List<PipelineInput> PipelineInputs { get => Items.SelectMany(i=>i.PipelineInputs).ToList(); }
        public override List<PipelineOutput> PipelineOutputs { get => Items.SelectMany(i => i.PipelineOutputs).ToList(); }

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
                i.Transform(signature);
            });
        }
    }
}
