using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    [JsonObject(MemberSerialization.OptOut)]
    class DOSBasedExtract : PipelineBase, ITransformation
    {

        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Output("curvature")]
        public FeatureDescriptor<List<double>> OutputCurvature { get; set; }

        public static readonly double wLength = 8.0;

        public static readonly double mLength = 2.0;

        public void Transform(Signature signature)
        {
            throw new NotImplementedException();
        }

        public static PointSection MakeSection(List<Vertex> list, int idx)
        {
            PointSection res = new PointSection(list[idx].Pos, list[idx].Pos);
            for (int i = idx; i < list.Count && res.Length < wLength; i++)
            {
                res.End = list[i].Pos;
            }
            return res;
        }

        public static PointSection MakeSectionReverse(List<Vertex> list, int idx)
        {
            PointSection res = new PointSection(list[idx].Pos, list[idx].Pos);
            res.End = res.Start = list[idx].Pos;
            for (int i = idx; i >= 0 && res.Length < wLength; i--)
            {
                res.Start = list[i].Pos;
            }
            return res;
        }
    }
}
