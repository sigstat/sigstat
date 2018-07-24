//using Alairas.WpfTemalabor.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Alairas.WpfTemalabor.DTW
//{
//    class FusedScore
//    {
//        public static double CalculateFusionOfDtwAndWPathScore(SignatureData testSignature, SignatureData[] referenceSignatures, Features featureFilter)
//        {
//            double averageDtwScore = 0;
//            double averageWPathScore = 0;

//            for (int i = 0; i < referenceSignatures.Length; i++)
//            {
//                Dtw dtw = new Dtw(testSignature, referenceSignatures[i], featureFilter);
//                averageDtwScore += dtw.CalculateDtwScore();
//                averageWPathScore += dtw.CalculateWarpingPathScore();
//            }

//            averageDtwScore /= referenceSignatures.Length;
//            averageWPathScore /= referenceSignatures.Length;

//            double similarityValueFromDtwScore = Analyzer.Sigmoid(averageDtwScore);
//            double similarityValueFromWPathScore = Analyzer.Sigmoid(averageWPathScore);

//            return similarityValueFromDtwScore + similarityValueFromWPathScore;
//        }
//    }
//}
