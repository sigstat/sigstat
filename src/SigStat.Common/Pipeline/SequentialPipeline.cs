using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SigStat.Common.Pipeline
{
    // TODO: Add() nem kene hogy latszodjon leszarmazottakban, kell egy koztes dolog

    /// <summary>
    /// Runs pipeline items in a sequence.
    /// <para>Default Pipeline Output: Output of the last Item in the sequence.</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SequentialTransformPipeline : PipelineBase, IEnumerable, ITransformation
    {
        [JsonProperty]
        /// <summary>List of transforms to be run in sequence.</summary>
        public List<ITransformation> Items = new List<ITransformation>();
        public override List<PipelineInput> PipelineInputs { get => Items[0].PipelineInputs; }
        public override List<PipelineOutput> PipelineOutputs { get => Items.Last().PipelineOutputs; }

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
            //itt most semmi ellenorzes nincsen, de lehetne
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
                var outputs = Items[i - 1].PipelineOutputs;
                var inputs = Items[i].PipelineInputs;
                for (int ii = 0; ii < inputs.Count; ii++)
                {
                    if (inputs[ii].AutoSetMode == AutoSetMode.Always || (inputs[ii].AutoSetMode == AutoSetMode.IfNull && inputs[ii].FD == null))
                    {
                        //elso, ami passzol es meg nem hasznaltuk
                        inputs[ii].FD = outputs.FirstOrDefault(po => po.Type == inputs[ii].Type && !inputs.Any(pi => pi.FD == po.FD))?.FD;
                    }
                    if (inputs[ii].FD == null)
                    {
                        this.LogError("Autowiring pipeline io failed, please provide an input to '{propName}' of '{item}'", inputs[ii].PropName, Items[i].ToString());
                        throw new /*SigStatPipelineAutoWiring*/Exception($"Autowiring pipeline io failed, please provide an input to '{inputs[ii].PropName}' of '{Items[i].ToString()}'");
                    }
                }

                Items[i].Transform(signature);
            }
        }
    }
}
