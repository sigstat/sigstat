using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model { 
    /// <summary>
    /// used to store results for testing different sampling frequencies
    /// </summary>
public class SampleRateResults 
{

        /// <summary>
        /// number of skipped points
        /// </summary>
        [Input]
        public int step { get; set; }
        /// <summary>
        /// average points of the signer
        /// </summary>
        [Input]
        public double pointsAvg { get; set; }
        /// <summary>
        /// AER for current sampling frequency test
        /// </summary>
        [Input]
        public double AER { get; set; }
        /// <summary>
        /// current samplerate tested
        /// </summary>
        [Input]
    public int samplerate { get; set; }





    }




}

