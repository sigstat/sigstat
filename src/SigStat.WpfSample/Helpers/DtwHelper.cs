using NDtw;
using SigStat.Common;
using SigStat.Common.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public static class DtwHelper
    {
        public static double GetCost(Signature sig1, Signature sig2, DtwType dtwType, List<FeatureDescriptor> features)
        {
            switch (dtwType)
            {
                case DtwType.DtwPy:
                    return DtwPy.Dtw(sig1.GetAggregateFeature(features), sig2.GetAggregateFeature(features), DtwPy.EuclideanDistance);
                case DtwType.NDtw:
                    //hatalmas hack, csak egy-egy featurere működik, kombinációra nem
                    return new NDtw.Dtw(sig1.GetFeature<List<double>>(features[0]).ToArray(), sig2.GetFeature<List<double>>(features[0]).ToArray(),
                        DistanceMeasure.Manhattan).GetCost();
                case DtwType.FrameworkDtw:
                    return new SigStat.Common.Algorithms.Dtw(Accord.Math.Distance.Manhattan)
                        .Compute(sig1.GetAggregateFeature(features).ToArray(), sig2.GetAggregateFeature(features).ToArray());
                case DtwType.MyDtw:
                    return new Common.Dtw(sig1, sig2, features).CalculateDtwScore();
                default:
                    throw new ArgumentException(nameof(dtwType) + typeof(DtwType) + "not exists");
            }
        }

    }
}
