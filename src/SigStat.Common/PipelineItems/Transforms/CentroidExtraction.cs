using SigStat.Common.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// sulypont kiszamolasa, hozzaadas a Featureokhoz.
    /// Hasznos ezutan pl Translate a Centroidba.
    /// </summary>
    public class CentroidExtraction : PipelineBase, IEnumerable, ITransformation
    {

        public CentroidExtraction()
        {
            //this.Input(Features.X, Features.X);
            this.Output(FeatureDescriptor<List<double>>.Descriptor("Centroid"));
        }

        public IEnumerator GetEnumerator()
        {
            return InputFeatures.GetEnumerator();
        }

        public void Add(FeatureDescriptor<List<double>> newitem)
        {
            InputFeatures.Add(newitem);
        }

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
