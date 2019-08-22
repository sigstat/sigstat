using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class NormalizeToRange : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> Input { get; set; }
        
        [Input]
        public Tuple<double, double> InputRange { get; set; }

        [Output]
        public FeatureDescriptor<List<double>> Output { get; set; }

        public void Transform(Signature signature)
        {
            if (InputRange.Item1 >= InputRange.Item2)
            {
                throw new ArgumentException();
            }
            var values = new List<double>(signature.GetFeature(Input));
            double min = values.Min();
            double max = values.Max();

            double rangeWidth = InputRange.Item2 - InputRange.Item1;

            for (int i = 0; i < values.Count; i++)
            {

                double helpVal = (values[i] - min) / (max - min);//0-1
                double newVal = helpVal * rangeWidth + InputRange.Item1;
                values[i] = newVal;
                Progress += 100 / values.Count;
            }
            signature.SetFeature(Output, values);
        }
    }
}
