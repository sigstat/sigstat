using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.LineTransforms
{
    [JsonObject(MemberSerialization.OptOut)]
    class Off2OnTransform : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<Vertex>> InputTrajectory { get; set; }

        [Input]
        public double InputScaleRate { get; set; }

        [Input]
        public double InputVMax { get; set; }

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; }

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; }

        [Output("Button")]
        public FeatureDescriptor<List<bool>> OutputButton { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("Off2OnTransform - transform started");
            var xs = new List<double>();
            var ys = new List<double>();
            var bs = new List<bool>();
            
            var trajectory = signature.GetFeature<List<Vertex>>(InputTrajectory);
            int inputScale = Math.Max((int)(trajectory.Count * InputScaleRate), 1);
            double inputVMax = trajectory.Count * InputVMax; 

            List<PointF> pointFs = new List<PointF>();
            trajectory.ForEach(vertex => pointFs.Add(vertex.Pos.ToPointF()));
            pointFs = EqualResampling.Calculate(pointFs, inputScale);
            /*var dosList = DOSBasedAlgorithm.Calculate(pointFs, 1, FusionPipelines.DOSConst);
            var realTraj = PseudoVelocityAlgorithm.Calculate(pointFs, dosList, inputVMax);*/ 
            foreach (var pointF in pointFs)
            {
                xs.Add((double)pointF.X);
                ys.Add((double)pointF.Y);
                bs.Add(true);
            }
            signature.SetFeature<List<double>>(OutputX, xs);
            signature.SetFeature<List<double>>(OutputY, ys);
            signature.SetFeature<List<bool>>(OutputButton, bs);
            this.LogInformation(trajectory.Count.ToString() + " -> " + xs.Count.ToString());
            this.LogInformation("Off2OnTransform - transform finished");
        }

       
    }
}
