using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;

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
                        inputs[ii].FD = outputs.FirstOrDefault(po => po.Type == inputs[ii].Type && !inputs.Any(pi => pi.FD == po.FD))?.FD;//elso, ami passzol es meg nem hasznaltuk
                                                                                                                                          //inputs[ii].FD = outputs[ii].FD;//old method
                    if (inputs[ii].FD == null)
                    {
                        Exception ex = new /*SigStatPipelineAutoWiring*/Exception($"Autowiring pipeline io failed, please provide an input to '{inputs[ii].FieldName}' of '{Items[i].ToString()}'");
                        Logger?.LogError(ex, "Autowiring Exception");
                        //throw ex;
                    }
                }

                Items[i].Transform(signature);
            }
        }
    }
}
