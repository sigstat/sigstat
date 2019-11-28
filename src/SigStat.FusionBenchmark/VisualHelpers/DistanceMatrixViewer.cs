using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Algorithms;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.TrajectoryRecovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    [JsonObject(MemberSerialization.OptOut)]
    public class DistanceMatrixViewer : PipelineBase
    {
        [Input]
        public List<FeatureDescriptor> InputFeatures { get; set; }
       
        [Input]
        public List<Signature> InputSignatures { get; set; }

        [Input]
        public Func<double[], double[], double> InputFunc { get; set; }

        public double[,] Calculate()
        {
            var distances = new double[InputSignatures.Count, InputSignatures.Count];
            for (int i = 0; i < InputSignatures.Count; i++){
                for (int j = 0; j < InputSignatures.Count; j++)
                {
                    distances[i, j] =
                        DtwPy.Dtw<double[]>(
                            InputSignatures[i].GetAggregateFeature(InputFeatures),
                            InputSignatures[j].GetAggregateFeature(InputFeatures),
                            InputFunc);
                }
            }
            return distances;
        }

       
    }
}
