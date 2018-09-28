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
    public class OptimalClassifierHelper
    {
        public List<SimilarityResult> SimilarityResults { get; }

        private double threshold;

        public OptimalClassifierHelper(List<SimilarityResult> distsFromReferences)
        {
            SimilarityResults = distsFromReferences;
        }

        //TODO: elkezd egy pont körül "ugrálni" debug
        public double CalculateThresholdForOptimalClassification()
        {
            SetAvgDistAsInitThreshold();
            double FAR, FRR;
            CalculateRates(out FAR, out FRR);
            double roundedFAR = Math.Round(FAR, 2, MidpointRounding.AwayFromZero);
            double roundedFRR = Math.Round(FRR, 2, MidpointRounding.AwayFromZero);
            double multipFAR = roundedFAR * 100;
            double multipFRR = roundedFRR * 100;
            double diff = Math.Abs(multipFAR - multipFRR);
            int round = 0;

            while (diff > 0 && round < 1000)
            {
                if (FAR > FRR)
                {
                    threshold -= Math.Abs(FAR - FRR) * threshold * 0.1;
                }
                else if (FAR < FRR)
                {
                    threshold += Math.Abs(FAR - FRR) * threshold * 0.1;
                }
                CalculateRates(out FAR, out FRR);

                roundedFAR = Math.Round(FAR, 2, MidpointRounding.AwayFromZero);
                roundedFRR = Math.Round(FRR, 2, MidpointRounding.AwayFromZero);
                multipFAR = roundedFAR * 100;
                multipFRR = roundedFRR * 100;
                diff = Math.Abs(multipFAR - multipFRR);
                round++;

            }
            return threshold;
        }

        private void CalculateRates(out double FAR, out double FRR)
        {
            int numAcceptedForged = 0;
            int numForged = 0;

            int numRejectedOriginal = 0;
            int numOriginal = 0;

            foreach (var simRes in SimilarityResults)
            {
                if (simRes.TestSignature.Origin == Origin.Forged) { numForged++; }
                if (simRes.TestSignature.Origin == Origin.Forged && Test(simRes)) { numAcceptedForged++; }

                if (simRes.TestSignature.Origin == Origin.Genuine) { numOriginal++; }
                if (simRes.TestSignature.Origin == Origin.Genuine && !Test(simRes)) { numRejectedOriginal++; }
            }

            FAR = (double)numAcceptedForged / numForged;
            FRR = (double)numRejectedOriginal / numOriginal;
        }

        private bool Test(SimilarityResult simRes)
        {
            return simRes.AvgDistFromReferences <= threshold;
        }

        private void SetAvgDistAsInitThreshold()
        {
            double avg = 0;
            foreach (var simRes in SimilarityResults)
            {
                avg += simRes.AvgDistFromReferences;
            }

            avg /= SimilarityResults.Count;
            threshold = avg;
        }

       
    }
}
