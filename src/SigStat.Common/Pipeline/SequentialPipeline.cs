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
        public List<ITransformation> Items = new List<ITransformation>();

        private Logger _logger;
        /// <summary>Passes Logger to child items as well.</summary>
        public new Logger Logger
        {
            get => _logger;
            set
            {
                _logger = value;
                Items.ForEach(i => i.Logger = _logger);
            }
        }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Add new transform to the list. Pass <see cref="Logger"/> and set up Progress event.
        /// </summary>
        /// <param name="newItem"></param>
        public void Add(ITransformation newItem)
        {
            if (_logger != null)
                newItem.Logger = _logger;
            newItem.ProgressChanged += ((o, p) => CalcProgress(i,p));
            Items.Add(newItem);

            //Set sequence output to last item's output (if given)
            if(newItem.OutputFeatures!=null)
                this.Output(newItem.OutputFeatures.ToArray());
        }

        private void CalcProgress(int i, int p)
        {
            double m = p / 100.0;
            Progress = (int)((i*(1-m)+(i+1)*m) / (Items.Count) * 100.0);
        }

        int i=0;
        /// <summary>
        /// Executes transform <see cref="Items"/> in sequence.
        /// Passes input features for each.
        /// Output is the output of the last Item in the sequence.
        /// </summary>
        /// <param name="signature">Signature to execute transform on.</param>
        public void Transform(Signature signature)
        {
            for (i = 0; i < Items.Count; i++)
            {
                //TODO: try
                //{
                if (Items[i].InputFeatures == null && i > 0)//pass previously calculated features if input not specified
                    Items[i].InputFeatures = Items[i - 1].OutputFeatures;
                Items[i].Transform(signature);
                //}
                //catch (Exception exc)
                //{
                //    throw PipelineException("Error while executing {pipelineItem.Type} with signature {sig.ToString()}", exc);
                //}
            }
        }
    }
}
