using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Model
{
    /// <summary>
    /// Used to approximate the classification limit in the training process.
    /// </summary>
    public class ApproximateLimit
    {
        private readonly IClassification pipeline;

        /// <summary>
        /// Initialize a new instance of the <see cref="ApproximateLimit"/> class.
        /// </summary>
        /// <param name="pipeline">The classification pipeline to be used for calculation.</param>
        public ApproximateLimit(IClassification pipeline)
        {
            this.pipeline = pipeline;
        }
        
        /// <summary>
        /// Calculate the limit by pairing each signature.
        /// Limit = AverageCost + StdDeviation.
        /// </summary>
        /// <param name="sigs">List of genuine signatures</param>
        /// <returns>Approximate limit.</returns>
        public double Calculate(List<Signature> sigs)
        {
            //TODO: itt is lehet szolni log.warn ha nem genuine

            //calc each pair
            List<double> pairingResults = new List<double>();
            for (int i = 0; i < sigs.Count - 1; i++)
            {
                for (int j = i + 1; j < sigs.Count; j++)
                {
                    pairingResults.Add(pipeline.Pair(sigs[i], sigs[j]));
                }
            }

            //calc average cost
            double avg = 0;
            foreach (double v in pairingResults)
            {
                avg += v;
            }

            avg /= pairingResults.Count;

            //calc standard deviation
            double dev = 0;
            foreach (double v in pairingResults)
                dev += Math.Pow(v - avg, 2);
            dev = Math.Sqrt(dev / (pairingResults.Count - 1));

            //limit = average + deviation
            return avg + dev;


        }
    }
}
