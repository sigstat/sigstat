using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Helpers
{
    /// <summary>
    /// Helper class for cleaning online signature data in loaders
    /// </summary>
    public static class DataCleaningHelper
    {
        /// <summary>
        /// Initialize timestamps of an online signature which does not have captured timestamps
        /// </summary>
        /// <param name="signature">The online signature which's timestamps are initialized</param>
        /// <param name="unitTimeSlot">The unit time slot between two points of the signature</param>
        public static void InitializeTimestamps(Signature signature, double unitTimeSlot)
        {
            var x = signature.GetFeature(Features.X);
            var y = signature.GetFeature(Features.Y);
            var pointTypes = signature.GetFeature(Features.PointType);

            if (x.Count != y.Count)
            {
                throw new ArgumentException("The length of X and Y are not the same");
            }

            var shifts = new List<double>();
            for (int i = 0; i < x.Count - 1; i++)
            {
                if (pointTypes[i] != 2 || (pointTypes[i] == 2 && pointTypes[i + 1] != 1))
                {
                    var xShift = Math.Abs(x[i + 1] - x[i]);
                    var yShift = Math.Abs(y[i + 1] - y[i]);
                    shifts.Add(Math.Sqrt((xShift * xShift) + (yShift * yShift)));
                }
            }

            var avgShift = shifts.Average();
            var t = new List<double>(x.Count);
            t.Add(0); //initialize the timestamp of the first point of the signature
            for (int i = 1; i < x.Count; i++)
            {
                if (pointTypes[i - 1] == 2 && pointTypes[i] == 1)
                {
                    var xShift = Math.Abs(x[i] - x[i - 1]);
                    var yShift = Math.Abs(y[i] - y[i - 1]);
                    var shift = Math.Sqrt((xShift * xShift) + (yShift * yShift));

                    t.Add(t[i - 1] + (shift * unitTimeSlot) / avgShift); //timestamp of the gap end (length of the gap)
                }
                else
                {
                    t.Add(t[i - 1] + unitTimeSlot);
                }
            }

            signature.SetFeature(Features.T, t);
        }

        /// <summary>
        /// Generate point type values of an online signature based on its pressure values (zero pressure points are required)
        /// </summary>
        /// <param name="pressure">The preussure values of an online signature</param>
        /// <returns></returns>
        public static double[] GeneratePointTypeValuesFromPressure(double[] pressure)
        {
            var pointType = new double[pressure.Length];
            for (int i = 0; i < pressure.Length; i++)
            {
                // First point of the singature
                if (i == 0)
                {
                    if (pressure[i].EqualsZero()) pointType[i] = 0; // There are zero pressure points before the signature
                    else pointType[i] = 1;
                }

                // Last point of the signature
                else if (i == pressure.Length - 1)
                {
                    if (pressure[i].EqualsZero()) pointType[i] = 0; // There are zero pressure points after the signature
                    else pointType[i] = 2;
                }

                else if (pressure[i] > 0) // pen is down in the actual point
                {
                    if (pressure[i - 1] > 0)
                    {
                        if (pressure[i + 1] > 0) pointType[i] = 0; // p p p --> x 0 x (pressure values --> point type values)
                        else pointType[i] = 2; // p p 0 --> x 2 0
                    }
                    else if (pressure[i + 1] > 0) pointType[i] = 1; // 0 p p --> 0 1 x
                    else pointType[i] = 3; // 0 p 0 --> 0 3 0
                }
                else pointType[i] = 0; // pen is up in the actual point
            }
            return pointType;
        }
    }
}
