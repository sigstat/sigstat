using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Extracts the Centroid (aka. Center Of Gravity) of the input features.
    /// <para> Default Pipeline Output: (List{double}) Centroid. </para>
    /// </summary>
    public class CentroidExtraction : PipelineBase, IEnumerable, ITransformation
    {
        /// <summary> Initializes a new instance of the <see cref="CentroidExtraction"/> class. </summary>
        public CentroidExtraction()
        {
            //this.Input(Features.X, Features.X);
            InputFeatures = new List<FeatureDescriptor>();
            this.Output(FeatureDescriptor.Get<List<double>>("Centroid"));
        }


        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return InputFeatures.GetEnumerator();
        }

        /// <inheritdoc/>
        public void Add(FeatureDescriptor<List<double>> newitem)
        {
            InputFeatures.Add(newitem);
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            List<double> c = new List<double>(InputFeatures.Count);
            foreach (var f in InputFeatures)
            {
                var values = signature.GetFeature<List<double>>(f);
                double avg = values.Average();
                c.Add(avg);
                Progress += 100 / InputFeatures.Count;
            }

            signature.SetFeature(OutputFeatures[0], c);
            Progress = 100;
            Log(LogLevel.Info, "Centroid extraction done.");
        }
    }
}
