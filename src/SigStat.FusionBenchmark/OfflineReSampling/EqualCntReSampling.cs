using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.OfflineReSampling
{
    class EqualCntReSampling : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Input]
        public int InputCnt { get; set; }

        [Output("Button")]
        public FeatureDescriptor<List<bool>> OutputButton { get; set; }

        [Output("X")]
        public FeatureDescriptor<List<double>> OutputX { get; set; }

        [Output("Y")]
        public FeatureDescriptor<List<double>> OutputY { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("EqualCntReSampling - transform started");
            var xs = signature.GetFeature<List<double>>(InputX);
            var ys = signature.GetFeature<List<double>>(InputY);
            var bs = signature.GetFeature<List<bool>>(InputButton);

            int n = bs.Count;
            if (n != xs.Count || n != ys.Count || InputCnt <= 0)
            {
                throw new ArgumentException();
            }
            var resXs = new List<double>();
            var resYs = new List<double>();
            var resBs = new List<bool>();

            int cnt = 0;
            for (int i = 0; i < n - 1; i++)
            {
                if (bs[i + 1] != bs[i])
                {
                    cnt = 0;
                    resXs.Add(xs[i]);
                    resYs.Add(ys[i]);
                    resBs.Add(bs[i]);
                }
                else
                {
                    if (cnt == 0)
                    {
                        resXs.Add(xs[i]);
                        resYs.Add(ys[i]);
                        resBs.Add(bs[i]);
                    }
                    cnt = (cnt + 1) % InputCnt;
                }
            }
            resXs.Add(xs[n - 1]);
            resYs.Add(ys[n - 1]);
            resBs.Add(bs[n - 1]);

            int newN = resBs.Count;
            if (newN != resXs.Count || newN != resYs.Count)
            {
                throw new Exception();
            }
            this.LogInformation(newN.ToString() + " elements in sequence");
            signature.SetFeature<List<double>>(OutputX, resXs);
            signature.SetFeature<List<double>>(OutputY, resYs);
            signature.SetFeature<List<bool>>(OutputButton, resBs);             
            this.LogInformation("EqualCntReSampling - transform finished");

        }

    }
}
