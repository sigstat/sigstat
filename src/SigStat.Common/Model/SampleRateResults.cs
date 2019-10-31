using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model { 

public class SampleRateResults 
{

        /// <summary>
        /// Gets or sets the input feature representing the X coordinates of an online signature
        /// 
         [Input]
        public int step { get; set; }
        /// </summary>
        /// 
        [Input]
        public double pointsAvg { get; set; }
        [Input]
    public int samplerate { get; set; }
    /// <summary>
    /// Gets or sets the input feature representing the X coordinates of an online signature
    /// </summary>
    [Input]
    public double AER { get; set; }




    }




}

