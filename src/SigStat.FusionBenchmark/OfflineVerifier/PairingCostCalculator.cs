/*using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.TrajectoryRecovery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.FusionBenchmark.OfflineVerifier
{
    class PairingCostCalculator : PipelineBase
    {
        [Input]
        public Signer InputSigner { get; set; }

        [Input]
        public Signer InputOnlineSigner { get; set; }

        [Input]
        public int InputBaseCount { get; set; }

        public void Calculate()
        {
            int n = InputSigner.Signatures.Count;
            var signatures = InputSigner.Signatures;
            var onlineSignatures = InputOnlineSigner.Signatures;
            if (InputSigner.ID != InputOnlineSigner.ID)
            {
                throw new ArgumentException();
            }
            var costMatrix = new double[InputBaseCount][];
            for (int i = 0; i < InputBaseCount; i++)
          
                costMatrix[i] = CalculateIdx();
            }
        }
    
        private double[] CalculateIdx()
        {
            int n = InputSigner.Signatures.Count;
            var signatures = InputSigner.Signatures;
            var costs = new double[n];
            Parallel.For(0, n, idx =>
            {
                var matches = signatures[idx].GetFeature<List<Tuple<int, Stroke, double, int>>>(FusionFeatures.StrokeMatches);
                matches = SelectStrokes(matches);
                costs[idx] = 0.0;
                foreach (var match in matches)
                {
                    costs[idx] += match.Item3;
                }
            }
            );
            return costs;
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
}*/
