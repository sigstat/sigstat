using SigStat.Common;
using SigStat.WpfSample.Helpers;
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
            double count = referenceSignatures.Length;

            for (int i = 0; i < referenceSignatures.Length; i++)
            {
                //TODO: refsig.length == 1 megnézni biztosra
                if (testSignature.ID != referenceSignatures[i].ID || referenceSignatures.Length == 1)
                {
                    Dtw dtw = new Dtw(testSignature, referenceSignatures[i], inputFeatures);
                    averageDtwScore += dtw.CalculateDtwScore();
                    averageWPathScore += dtw.CalculateWarpingPathScore();
                }
                else
                    count--;
            }

            averageDtwScore /= count;
            averageWPathScore /= count;

            //double similarityValueFromDtwScore = MyMath.Sigmoid(averageDtwScore);
            //double similarityValueFromWPathScore = MyMath.Sigmoid(averageWPathScore);

            //return similarityValueFromDtwScore + similarityValueFromWPathScore;
            return averageDtwScore + averageWPathScore;
        }
    }
}
