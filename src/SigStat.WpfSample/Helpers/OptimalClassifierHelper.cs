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
        public ClassifierType ClassifierType { get; private set; }
        public List<Signature> Signatures { get;private set; }
        public List<FeatureDescriptor> InputFeatures { get; private set; }
        public ThresholdResult  ThresholdResult { get; private set; }

        private List<Signature> originals;
        private double threshold;

        public OptimalClassifierHelper(ClassifierType classifierType, List<Signature> signatures, List<FeatureDescriptor> inputFeatures)
        {
            ClassifierType = classifierType;
            Signatures = signatures;
            InputFeatures = inputFeatures;

            originals = signatures.FindAll(s => s.Origin == Origin.Genuine);
        }


        //TODO: átgondolni, átbeszélni
        public double CalculateThresholdForOptimalClassification()
        {
            threshold = GetAvgDist(Signatures);
            double FAR, FRR;
            FAR = CalculateFAR();
            FRR = CalculateFRR();
            double roundedFAR = Math.Round(FAR, 2, MidpointRounding.AwayFromZero);
            double roundedFRR = Math.Round(FRR, 2, MidpointRounding.AwayFromZero);
            double multipFAR = roundedFAR * 100;
            double multipFRR = roundedFRR * 100;
            double diff = Math.Abs(multipFAR - multipFRR);
            int round = 0;

            while (diff > 0 && round < 10000)
            {
                if (FAR > FRR)
                {
                    threshold -= Math.Abs(FAR - FRR) * threshold * 0.1;
                }
                else if (FAR < FRR)
                {
                    threshold += Math.Abs(FAR - FRR) * threshold * 0.1;
                }
                FAR = CalculateFAR();
                FRR = CalculateFRR();

                roundedFAR = Math.Round(FAR, 2, MidpointRounding.AwayFromZero);
                roundedFRR = Math.Round(FRR, 2, MidpointRounding.AwayFromZero);
                multipFAR = roundedFAR * 100;
                multipFRR = roundedFRR * 100;
                diff = Math.Abs(multipFAR - multipFRR);
                round++;

            }
            ThresholdResult = new ThresholdResult(originals[0].Signer.ID, FRR, FAR, (FRR + FAR) / 2, threshold);
            return threshold;
        }

        public double CalculateFAR()
        {
            int numAcceptedForged = 0;
            int numForged = 0;

            foreach (var signature in Signatures)
            {
                if (signature.Origin == Origin.Forged) { numForged++; }
                if (signature.Origin == Origin.Forged && Test(signature)) { numAcceptedForged++; }
            }

            return (double)numAcceptedForged / numForged;
        }

        public double CalculateFRR()
        {
            int numRejectedOriginal = 0;
            int numOriginal = 0;

            foreach (var signature in Signatures)
            {
                if (signature.Origin == Origin.Genuine) { numOriginal++; }
                if (signature.Origin == Origin.Genuine && !Test(signature)) { numRejectedOriginal++; }
            }

            return (double)numRejectedOriginal / numOriginal;
        }

        
        private bool Test(Signature testSignature)
        {
            return GetAvgDistFrom(testSignature, originals) <= threshold;
        }

        private double GetAvgDist(List<Signature> signatures)
        {
            double avg = 0;

            for (int i = 0; i < signatures.Count - 1; i++)
            {
                for (int j = 1; j < signatures.Count; j++)
                {
                    if (ClassifierType == ClassifierType.DTW)
                        avg += new Dtw(signatures[i], signatures[j], InputFeatures).CalculateDtwScore();
                    else if (ClassifierType == ClassifierType.FusedScore)
                        avg += FusedScore.CalculateFusionOfDtwAndWPathScore(signatures[i], new Signature[] { signatures[j] }, InputFeatures);
                    else
                        throw new NotImplementedException("Not implemented for this classifier type");
                }
            }

            avg /= (originals.Count * (originals.Count - 1) / 2);

            return avg;
        }

        private double GetAvgDistFrom(Signature test, List<Signature> references)
        {
            if (ClassifierType == ClassifierType.DTW)
            {
                double avg = 0;
                bool isTestInRefs = false;
                foreach (var reference in references)
                {
                    if (reference.ID != test.ID)
                        avg += new Dtw(test, reference, InputFeatures).CalculateDtwScore();
                    else
                        isTestInRefs = true;
                }
                return avg /= (isTestInRefs ? references.Count - 1 : references.Count);
            }
            else if (ClassifierType == ClassifierType.FusedScore)
                return FusedScore.CalculateFusionOfDtwAndWPathScore(test, references.ToArray(), InputFeatures);
            else
                throw new NotImplementedException("Not implemented for this classifier type");
            
        }
    }
}
