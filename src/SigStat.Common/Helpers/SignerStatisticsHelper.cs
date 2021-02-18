using System;
using System.Collections.Generic;
using System.Text;
using SigStat.Common.Helpers;
namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Calculates general statistics of the signatures of a given signer
    /// </summary>
    public static class SignerStatisticsHelper
    {
        /// <summary>
        /// Return the average od signatures points number
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public static double GetLengthAverage(this Signer signer)
        {
            double sum = 0;
            foreach (Signature signature in signer.Signatures)
            {
                sum = sum + SignatureHelper.GetSignatureLength(signature);
            }

            return sum / signer.Signatures.Count;
        }
        /// <summary>
        /// Return signer width average
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public static double GetWidthAvg(this Signer signer)
        {
            double sum = 0;
            foreach (Signature s in signer.Signatures)
            {
                sum = sum + s.GetFeature(Features.Size).Width;
            }

            return sum / signer.Signatures.Count;
        }
        /// <summary>
        /// return signer height average
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public static double GetHeightAvg(this Signer signer)
        {
            double sum = 0;
            foreach (Signature s in signer.Signatures)
            {
                sum = sum + s.GetFeature(Features.Size).Height;
            }

            return sum / signer.Signatures.Count;
        }
        /// <summary>
        /// return signer points average
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public static double GetPointsAvg(this Signer signer)
        {
            double sum = 0;
            foreach (Signature signature in signer.Signatures)
            {
                sum = sum + (signature.GetFeature(Features.X).Count);
            }

            return sum / signer.Signatures.Count;
        }
        /// <summary>
        /// return the min signature points number of a signer
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public static double GetMinSignaturePoints(this Signer signer)
        {

            double min = signer.Signatures[0].GetFeature(Features.X).Count;
            foreach (Signature signature in signer.Signatures)
            {
                if (signature.GetFeature(Features.X).Count < min)
                    min = signature.GetFeature(Features.X).Count;
            }
            return min;
        }
        /// <summary>
        /// return the min signature points number of a signer
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public static double GetMaxSignaturePoints(this Signer signer)
        {

            double max = signer.Signatures[0].GetFeature(Features.X).Count;
            foreach (Signature signature in signer.Signatures)
            {
                if (signature.GetFeature(Features.X).Count > max)
                    max = signature.GetFeature(Features.X).Count;
            }
            return max;
        }
    }
}
