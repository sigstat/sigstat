using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Helpers
{
    public class BenchmarkConfig
    {
        //string config = "{sampling:  s1,s2,s3}" +
        //    " database: SVC2004, MCYT100" +
        //    "Filter: none, P" +
        //    "Rotation: true, false" +
        //    "translation: None, CogToOiriginX,CogToOriginY,CogToOiriginXY, BottomLeftToOrigin;" +
        //    "UniformScaling: None, X01Y0prop, Y01X0prop" +
        //    "Scaling: None, X01, Y01, X01Y01" +
        //    "ResamplingType: none, CubicTimeSlotLength, CubicSampleCount, CubicFillPenUp, LinearTimeSlotLength, LinearSampleCount, LinearFillPenUp
        //    "Interpolation: , }";

        private BenchmarkConfig() { }

        public BenchmarkConfig FromString(string jsonSting)
        {
            throw new NotImplementedException();
        }

        public string Sampling { get; set; }
        public string Database { get; set; }
        public string Filter { get; set; }
        public bool Rotation { get; set; }
        public string Translation { get; set; }
        public string UniformScaling { get; set; }
        public string Scaling { get; set; }
        public string ResamplingType { get; set; }
        public string Interpolation { get; set; }

    }
}
