using SigStat.Common;
using SigStat.Common.Model;
using SigStat.WpfSample.Common;
using SigStat.WpfSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public static class OptimalClassifierHelper
    {
        public class ClassificationResult
        {
            public double Threshold { get; set; }
            public List<ClassificationResultLine> Lines { get; set; }
        }

        public class ClassificationResultLine
        {
            public double Threshold { get; set; }
            public double Far { get; set; }
            public double Frr { get; set; }
            public double Aer { get; set; }
        }

        public static ClassificationResult CalculateThreshold(List<SimilarityResult> similarityResults)
        {
            var result = new ClassificationResult() { Lines = new List<ClassificationResultLine>() };
            similarityResults = similarityResults.OrderBy(s => s.AvgDistFromReferences).ToList();
            for (int i = 0; i < similarityResults.Count - 1; i++)
            {
                var threshold = (similarityResults[i].AvgDistFromReferences + similarityResults[i + 1].AvgDistFromReferences) / 2;
                CalculateRates(similarityResults, threshold, out var far, out var frr);
                result.Lines.Add(new ClassificationResultLine { Threshold = threshold, Far = far, Frr = frr, Aer = far / frr });
            }
            result.Threshold = result.Lines.OrderBy(er => er.Aer).First().Threshold;
            return result;
        }

        private static void CalculateRates(List<SimilarityResult> similarityResults, double threshold, out double FAR, out double FRR)
        {
            int numAcceptedForged = 0;
            int numForged = 0;

            int numRejectedOriginal = 0;
            int numOriginal = 0;

            foreach (var simRes in similarityResults)
            {
                if (simRes.TestSignature.Origin == Origin.Forged) { numForged++; }
                if (simRes.TestSignature.Origin == Origin.Forged && Test(simRes, threshold)) { numAcceptedForged++; }

                if (simRes.TestSignature.Origin == Origin.Genuine) { numOriginal++; }
                if (simRes.TestSignature.Origin == Origin.Genuine && !Test(simRes, threshold)) { numRejectedOriginal++; }
            }

            FAR = (double)numAcceptedForged / numForged;
            FRR = (double)numRejectedOriginal / numOriginal;
        }

        private static bool Test(SimilarityResult simRes, double threshold)
        {
            return simRes.AvgDistFromReferences <= threshold;
        }


    }
}
