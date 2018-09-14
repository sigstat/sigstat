using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    public static class Configuration
    {
        public const int DefaultSpacingParameter = 1;

        public static readonly List<FeatureDescriptor> DefaultInputFeatures = new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X, Features.Y });
        public static readonly List<FeatureDescriptor>[] TestInputFeatures = 
            new List<FeatureDescriptor>[] { new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X, Features.Y }) };


        public const int SignerCount = 40;

        public const int SignatureCount = 40;
        public const int OriginalSignatureCount = 20;
        public const int ForgedSignatureCount = SignatureCount - OriginalSignatureCount;

        public static readonly string FusedScoreAutoClassificationName = DateTime.Today.ToShortDateString() + "_AllFeaturesAlone_FusedScoreAutoClass.xlsx";


        public static int[] GetIndexes(int signerCount)
        {
            int[] signerIndexes = new int[signerCount];
            for (int i = 0; i < signerCount; i++)
            {
                signerIndexes[i] = i + 1;
            }
            return signerIndexes;
        }
    }
}
