using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    [JsonObject(MemberSerialization.OptOut)]
    public class StrokePairSaver : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputTrajectory { get; set; }

        [Input]
        public FeatureDescriptor<List<Tuple<int, Stroke, double, int>>> InputStrokeMatches { get; set; }


        [Input]
        public string InputBasePath { get; set; }

        [Input]
        public string InputFileName { get; set; }

        public void Transform(Signature signature)
        {
            string path = InputBasePath + "/" + signature.Signer.ID + "_" + signature.ID + InputFileName + ".png";
            var img = signature.GetFeature<Image<Rgba32>>(InputImage).Clone();
            var trajectory = signature.GetFeature<List<Vertex>>(InputTrajectory);
            var strokeMatches = signature.GetFeature<List<Tuple<int, Stroke, double, int>>>(InputStrokeMatches);
            strokeMatches = SelectStrokes(strokeMatches);

            var referenceTrajectory = signature.Signer.Signatures[0].GetFeature(FusionFeatures.BaseTrajectory);

            for (int i = 0; i < referenceTrajectory.Count; i++)
            {
                img.ReColour(referenceTrajectory[i].Pos.Move(30, 30), Rgba32.Black);
            }



            var cnt = 0;
            foreach (var strokeMatch in strokeMatches)
            {
                var stroke = strokeMatch.Item2;
                for (int i = 0; i < stroke.Count; i++)
                {
                    img.VariableColour(stroke[i].Pos, cnt);
                }
                for (int i = 0; i < Math.Min(strokeMatch.Item4, stroke.Count); i += 7)
                {
                    img.VariableColourLine(stroke[i].Pos, referenceTrajectory[strokeMatch.Item1 + i].Pos.Move(30, 30), cnt);
                }

                cnt++;
            }

            //cnt = 0;

            //for (int i = 0; i < Math.Min(referenceTrajectory.Count, trajectory.Count) ; i++)
            //{
            //    if (i > 0 && trajectory[i] != trajectory[i - 1] && !Vertex.AreNeighbours(trajectory[i - 1], trajectory[i]))
            //    {
            //        cnt++;
            //    }
            //    if (i%10==0)
            //        img.VariableColourLine(trajectory[i].Pos, referenceTrajectory[i].Pos, cnt);
            //}


            img.SaveAsPng(File.Create(path));
        }

        private List<Tuple<int, Stroke, double, int>> SelectStrokes(List<Tuple<int, Stroke, double, int>> order)
        {
            List<Tuple<int, Stroke, double, int>> res = new List<Tuple<int, Stroke, double, int>>();
            foreach (var tuple in order)
            {
                var sibling = order.Find(find => find.Item2 == tuple.Item2.Sibling);
                if (tuple.Item3 < sibling.Item3) { res.Add(tuple); }
            }
            return res;
        }
    }
}
