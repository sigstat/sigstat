using SigStat.Common;
using SigStat.WpfSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public class ClassifierHelper
    {
        public static double CalculateFAR(List<Signature> signatures, IClassifier classifier)
        {
            int numAcceptedForged = 0;
            int numForged = 0;

            foreach (var signature in signatures)
            {
                if (signature.Origin == Origin.Forged) { numForged++; }
                if (signature.Origin == Origin.Forged && classifier.Test(signature)) { numAcceptedForged++; }
            }

            return (double)numAcceptedForged / numForged;
        }

        public static double CalculateFRR(List<Signature> signatures, IClassifier classifier)
        {
            int numRejectedOriginal = 0;
            int numOriginal = 0;

            foreach (var signature in signatures)
            {
                if (signature.Origin == Origin.Genuine) { numOriginal++; }
                if (signature.Origin == Origin.Genuine && !classifier.Test(signature)) { numRejectedOriginal++; }
            }

            return (double)numRejectedOriginal / numOriginal;
        }

        public static double CalculateAER(List<Signature> signatures, IClassifier classifier)
        {
            return (CalculateFAR(signatures, classifier) + CalculateFRR(signatures, classifier)) / 2.0;
        }

        public static void Normalize(Signature sig, List<FeatureDescriptor> inputFeatures)
        {
            foreach (var feature in inputFeatures)
            {
                var originalValues = sig.GetFeature<List<double>>(feature).ToArray();
                var normalizedValues = MyMath.MinMaxTransformationNormalization(originalValues).ToList();
                sig.SetFeature(feature, normalizedValues);
            }
        }
    }
}
