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

namespace SigStat.FusionBenchmark.VisualHelpers
{
    [JsonObject(MemberSerialization.OptOut)]
    class TrajectorySaver : PipelineBase, ITransformation
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

            int cnt = 0;
            for (int i = 0; i < trajectory.Count; i++)
            {
                if (i > 0 && trajectory[i] != trajectory[i-1] && !Vertex.AreNeighbours(trajectory[i - 1], trajectory[i]))
                {
                    cnt++;
                }
                img.VariableColour(trajectory[i].Pos, cnt);
            }
            img.SaveAsPng(File.Create(path));
        }
    }
}
