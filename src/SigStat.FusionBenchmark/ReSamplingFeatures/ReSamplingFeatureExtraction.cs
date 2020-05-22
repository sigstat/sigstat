using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.FusionMathHelper;
using SigStat.FusionBenchmark.ReSamplingFeatures.FeatureExtractAlgorithms;
using SigStat.FusionBenchmark.ReSamplingFeatures.ReSamplingFuncs;
using SigStat.FusionBenchmark.VisualHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.ReSamplingFeatures
{
    public class ReSamplingFeatureExtraction : PipelineBase
    {
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; }

        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; }

        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        private object o = new object();

        private readonly double fromKitevo = 0.33;
        private readonly double toKitevo = 0.33;
        private readonly double kitevoJump = 0.15;
        private readonly double diffToIdx = 3;

        public Tuple<List<double>, List<List<double>>> Calculate(List<Signer> signers)
        {
            var resList = new List<double>();
            var dataLists = new List<List<double>>();
            Parallel.ForEach(signers, signer =>
            {
                var newRes = Calculate(signer);
                var newResList = newRes.Item1;
                var newDataLists = newRes.Item2;
                lock (o)
                {
                    resList.AddRange(newResList);
                    if (dataLists.Count == 0)
                    {
                        for (int i = 0; i < newDataLists.Count; i++) { dataLists.Add(new List<double>()); }
                    }
                    if (dataLists.Count != newDataLists.Count)
                    {
                        throw new Exception();
                    }
                    for (int i = 0; i < dataLists.Count; i++)
                    {
                        dataLists[i].AddRange(newDataLists[i]);
                    }
                }
            }
            );
            Check(resList, dataLists);
            return new Tuple<List<double>, List<List<double>>>(resList, dataLists);
        }

        public Tuple<List<double>, List<List<double>>> Calculate(Signer signer)
        {
            var resList = new List<double>();
            var dataLists = new List<List<double>>();
            Parallel.ForEach(signer.Signatures, sig =>
            {
                if (sig.Origin == Origin.Genuine)
                {
                    var newRes = Calculate(sig);
                    List<double> newResList = newRes.Item1;
                    List<List<double>> newDataLists = newRes.Item2;
                    lock (o)
                    {
                        resList.AddRange(newResList);
                        if (dataLists.Count == 0)
                        {
                            for (int i = 0; i < newDataLists.Count; i++) { dataLists.Add(new List<double>()); }
                        }
                        if (dataLists.Count != newDataLists.Count)
                        {
                            throw new Exception();
                        }
                        for (int i = 0; i < dataLists.Count; i++)
                        {
                            dataLists[i].AddRange(newDataLists[i]);
                        }
                    }
                }
            }
            );
            Check(resList, dataLists);
            var res = new Tuple<List<double>, List<List<double>>>(resList, dataLists);
            TxtHelper.Save(TxtHelper.ReSamplingResultsToLines(res), "resamplingdata" + signer.ID);
            return res;
        }


        public Tuple<List<double>, List<List<double>>> Calculate(Signature signature)
        {
            var xs = signature.GetFeature<List<double>>(InputX);
            var ys = signature.GetFeature<List<double>>(InputY);
            var bs = signature.GetFeature<List<bool>>(InputButton);
            int n = xs.Count;
            if (n != xs.Count || n != ys.Count)
            {
                throw new Exception();
            }

            var dirs = DirectionAlgorithm.Calculate(xs, ys);
            var tangs = FuncOneAlgorithm.Calculate(dirs, Math.Tan);

            var diffXs = DerivatorAlgorithm.Calculate(xs, 1, Kivono.Calculate);
            var diffYs = DerivatorAlgorithm.Calculate(ys, 1, Kivono.Calculate);
            diffXs.RemoveAt(diffXs.Count - 1);
            diffYs.RemoveAt(diffYs.Count - 1);

            var dataLists = new List<List<double>>();

            dataLists.Add(dirs);
            dataLists.Add(tangs);
            for (int i = 1; i < diffToIdx; i++)
            {
                dataLists.Add(DerivatorAlgorithm.Calculate(dirs, i, Geometry.DiffAngle));
                dataLists.Add(DerivatorAlgorithm.Calculate(dirs, -i, Geometry.DiffAngle));
                dataLists.Add(DerivatorAlgorithm.Calculate(tangs, i, Kivono.Calculate));
                dataLists.Add(DerivatorAlgorithm.Calculate(tangs, -i, Kivono.Calculate));
            }

            var resList = FuncTwoAlgorithm.Calculate(new List<List<double>>() { diffXs, diffYs }, EuclideanFunc.Calculate);

            int m = dataLists.Count;
            var functors = GetFuncs();
            for (int i = 0; i < m; i++)
            {
                foreach (var func in functors)
                {
                    dataLists.Add(FuncOneAlgorithm.Calculate(dataLists[i], func));
                }
            }

            resList = JustOnAlgorithm.Calculate(resList, bs);
            dataLists = JustOnAlgorithm.Calculate(dataLists, bs);
            Check(resList, dataLists);
            return new Tuple<List<double>, List<List<double>>>(resList, dataLists);
        }

        private List<Func<double, double>> GetFuncs()
        {
            var res = new List<Func<double, double>>();
            res.Add(Math.Log);
            for (double kitevo = fromKitevo; kitevo <= toKitevo; kitevo += kitevoJump)
            {
                res.Add(new HatvanyozoFunc(kitevo).Calculate);
            }
            return res;
        }

        private void Check(List<double> resList, List<List<double>> dataLists)
        {
            int n = resList.Count;
            foreach (var list in dataLists)
            {
                if (list.Count != n)
                {
                    throw new Exception();
                }
            }
        }

       
    }
}
