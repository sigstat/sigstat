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
        public const int DefaultSpacingParameter = 5;

        public static readonly List<string> PreprocessingTransformations = new List<string>(new string[] 
        {
            "Translate",
            "UniformScale",
            "Scale",
            "NormalizeRotation"
        });

        public static readonly List<FeatureDescriptor> DefaultInputFeatures = new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X, Features.Y });
        public static readonly List<FeatureDescriptor>[] XYP = new[] { new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure } };

        public static readonly List<FeatureDescriptor>[] TestInputFeatures =
            new List<FeatureDescriptor>[] {
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X }),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Y }),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Azimuth }),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Altitude }),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Pressure }),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODY}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODAzimuth}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODAltitude}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODY}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedSO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration}),
                #region pairs from opti DTW top features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.FODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.CosineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.Acceleration}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, DerivedSvc2004Features.CosineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, DerivedSvc2004Features.Acceleration}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.Acceleration}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX, DerivedSvc2004Features.Acceleration}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Pressure, DerivedSvc2004Features.Acceleration}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Pressure, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, Features.Y }),
                #endregion
                #region pairs from opti FusedScore top features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.CosineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SODX, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, Features.Y}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.Pressure, Features.Y}),
                #endregion
                #region pairs from auto DTW top features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.Velocity}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, Features.Pressure}),
                #endregion
                #region pairs from auto FusedScore top features
                // new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, DerivedSvc2004Features.LengthBasedSO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Acceleration, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, DerivedSvc2004Features.LengthBasedFO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, DerivedSvc2004Features.LengthBasedSO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.SineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.LengthBasedSO}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedSO, Features.Pressure}),
                #endregion
                #region triples from opti DTW top features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.FODX, DerivedSvc2004Features.CosineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.FODX, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.FODX, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.CosineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SODX, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, DerivedSvc2004Features.CosineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODX, DerivedSvc2004Features.SODX, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX, Features.Pressure}),
                #endregion
                #region triples from auto DTW top features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.Velocity, DerivedSvc2004Features.LengthBasedFO}),
                new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.Velocity, DerivedSvc2004Features.SineMeasure}),
                new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.Velocity, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.LengthBasedFO, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.SineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity, DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SineMeasure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity, DerivedSvc2004Features.LengthBasedFO, Features.Pressure}),
                new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.Velocity, DerivedSvc2004Features.SineMeasure, Features.Pressure}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SineMeasure, Features.Pressure}),
                #endregion
                // set of top 4 opti DTW features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.FODX, DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX}),
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.FODX, DerivedSvc2004Features.CosineMeasure, DerivedSvc2004Features.SODX, Features.Pressure}),
                // set of top 4 auto DTW features
                //new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.Velocity, DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SineMeasure}),
                new List<FeatureDescriptor>(new FeatureDescriptor[] { DerivedSvc2004Features.FODPressure, DerivedSvc2004Features.Velocity, DerivedSvc2004Features.LengthBasedFO, DerivedSvc2004Features.SineMeasure, Features.Pressure}),

            };


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
