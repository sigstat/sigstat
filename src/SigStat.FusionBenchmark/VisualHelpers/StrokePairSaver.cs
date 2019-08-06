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
    class StrokePairSaver : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<Image<Rgba32>> InputImage { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputTrajectory { get; set; }

        [Input]
        public string InputBasePath { get; set; }

        [Input]
        public string InputFileName { get; set; }

        public void Transform(Signature signature)
        {
            string path = InputBasePath + "/" + signature.Signer.ID + "_" + signature.ID + InputFileName + ".png";
            var img = signature.GetFeature<Image<Rgba32>>(InputImage).Clone();
            var trajectory = signature.GetFeature<List<Vertex>>(InputTrajectory);
            var strokeMatches = (List< Tuple<int, Stroke, double>>)signature["tmp"];
            strokeMatches = SelectStrokes(strokeMatches);
            //int cnt = 0;
            //for (int i = 0; i < trajectory.Count; i++)
            //{
            //    if (i > 0 && trajectory[i] != trajectory[i-1] && !Vertex.AreNeighbours(trajectory[i - 1], trajectory[i]))
            //    {
            //        cnt++;
            //    }
            //    img.VariableColour(trajectory[i].Pos, cnt);
            //}

            var referenceTrajectory = signature.Signer.Signatures[0].GetFeature(FusionFeatures.BaseTrajectory);

            for (int i = 0; i < referenceTrajectory.Count; i++)
            {
                img.VariableColour(referenceTrajectory[i].Pos, 0);
            }



            var cnt = 0;
            foreach (var strokeMatch in strokeMatches)
            {
                var stroke = strokeMatch.Item2;
                for (int i = 0; i < stroke.Count; i++)
                {
                    img.VariableColour(stroke[i].Pos, cnt);
                    if (i%10==0)
                        img.VariableColourLine(stroke[i].Pos, referenceTrajectory[strokeMatch.Item1+i].Pos, cnt);
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

        private List<Tuple<int, Stroke, double>> SelectStrokes(List<Tuple<int, Stroke, double>> order)
        {
            List<Tuple<int, Stroke, double>> res = new List<Tuple<int, Stroke, double>>();
            foreach (var tuple in order)
            {
                var sibling = order.Find(find => find.Item2 == tuple.Item2.Sibling);
                if (tuple.Item3 < sibling.Item3) { res.Add(tuple); }
            }
            return res;
        }
    }
}
