using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    public class FusedScore
    {
        public static double CalculateFusionOfDtwAndWPathScore(Signature testSignature, Signature[] referenceSignatures, List<FeatureDescriptor> inputFeatures)
        {
            double averageDtwScore = 0;
            double averageWPathScore = 0;
            bool isTestSigInReferences = false;

            for (int i = 0; i < referenceSignatures.Length; i++)
            {
                if (testSignature.ID != referenceSignatures[i].ID)
                {
                    Dtw dtw = new Dtw(testSignature, referenceSignatures[i], inputFeatures);
                    averageDtwScore += dtw.CalculateDtwScore();
                    averageWPathScore += dtw.CalculateWarpingPathScore();
                }
                else
                    isTestSigInReferences = true;
            }

            averageDtwScore /= (isTestSigInReferences ? referenceSignatures.Length-1 : referenceSignatures.Length);
            averageWPathScore /= (isTestSigInReferences ? referenceSignatures.Length - 1 : referenceSignatures.Length);

            //double similarityValueFromDtwScore = Analyzer.Sigmoid(averageDtwScore);
            //double similarityValueFromWPathScore = Analyzer.Sigmoid(averageWPathScore);

            //return similarityValueFromDtwScore + similarityValueFromWPathScore;
            return averageDtwScore + averageWPathScore;
        }
    }
}
