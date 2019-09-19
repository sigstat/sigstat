using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    class CogExtract : PipelineBase, ITransformation
    {   
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }
        
        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Output]
        public FeatureDescriptor<Point> OutputCog { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("CogExtract - transform started");
            var xs = signature.GetFeature(InputX);
            var ys = signature.GetFeature(InputY);
            int n = xs.Count;
            if (xs.Count != n || ys.Count != n)
            {
                throw new Exception();
            }
            double avgX = xs.Average();
            double avgY = ys.Average();
            signature.SetFeature<Point>(OutputCog, new Point((int)avgX,(int)avgY));
            this.LogInformation("CogExtract - transform finished");

        }
    }
}
