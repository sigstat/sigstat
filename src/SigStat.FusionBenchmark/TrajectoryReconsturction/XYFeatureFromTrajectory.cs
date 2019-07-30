using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using Newtonsoft.Json;
using SigStat.FusionBenchmark.GraphExtraction;

namespace SigStat.FusionBenchmark.TrajectoryReconsturction
{
    class XYFeatureFromTrajectory: PipelineBase, ITransformation
    {

        [Input]
        public FeatureDescriptor<VertexList> InputTrajectory { get; set; }

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; }

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; }

        [Output("Button")]
        public FeatureDescriptor<List<bool>> OutputButton { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("XYFeatureFromTrajectory transform started.");
            var trajectory = signature.GetFeature<VertexList>(InputTrajectory);
            var xList = new List<double>();
            var yList = new List<double>();
            var bList = new List<bool>();
            foreach (var p in trajectory)
            {
                xList.Add((double)p.Pos.X);
                yList.Add((double)p.Pos.Y);
                bList.Add(p.On);
            }
            signature.SetFeature<List<double>>(OutputX, xList);
            signature.SetFeature<List<double>>(OutputY, yList);
            signature.SetFeature<List<bool>>(OutputButton, bList);
            this.LogInformation("XYFeatureFromTrajectory transform finished.");
        }
    }
}
