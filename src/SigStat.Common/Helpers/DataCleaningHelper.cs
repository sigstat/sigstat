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
        /// Inserts two points as border points of gaps in an online signature.
        /// The inserted values are X- and Y-coordinates and a calculated timestamp.
        /// </summary>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="signature">The online signature in which the points are inserted</param>
        /// <param name="unitTimeSlot">The unit time slot between two points of the signature</param>
        public static void Insert2DPointsForGapBorders(int[] gapIndexes, Signature signature, double unitTimeSlot)
        {
            //Filter the first point of th signature from gaps
            gapIndexes = Array.FindAll(gapIndexes, i => i != 0);

            var x = signature.GetFeature(Features.X);
            var y = signature.GetFeature(Features.Y);

            if (x.Count != y.Count)
            {
                throw new ArgumentException("The length of X and Y are not the same");
            }

            var shifts = new List<double>();
            for (int i = 0; i < x.Count - 1; i++)
            {
                if (!gapIndexes.Contains(i + 1))
                {
                    var xShift = Math.Abs(x[i + 1] - x[i]);
                    var yShift = Math.Abs(y[i + 1] - y[i]);
                    shifts.Add(Math.Sqrt((xShift * xShift) + (yShift * yShift)));
                }
            }

            var avgShift = shifts.Average();
            var t = new List<double>(x.Count);
            double epszilonTime = unitTimeSlot / x.Count;
            t.Add(0); //initialize the timestamp of the first point of the signature
            for (int i = 1; i < x.Count; i++)
            {
                if (gapIndexes.Contains(i))
                {
                    var xShift = Math.Abs(x[i] - x[i - 1]);
                    var yShift = Math.Abs(y[i] - y[i - 1]);
                    var shift = Math.Sqrt((xShift * xShift) + (yShift * yShift));

                    x.Insert(i, x[i]); //duplicate the value after the gap
                    x.Insert(i, x[i - 1]); //duplicate the value before the gap
                    y.Insert(i, y[i]); //duplicate the value after the gap
                    y.Insert(i, y[i - 1]); //duplicate the value before the gap

                    t.Add(t[i - 1] + epszilonTime); //timestamp of the gap start
                    t.Add(t[i] + (shift * unitTimeSlot) / avgShift); //timestamp of the gap end (length of the gap)
                    t.Add(t[i + 1] + epszilonTime); //timestamp of the point after the gap

                    //update indexes after insertation of two points
                    gapIndexes = Array.FindAll(gapIndexes, idx => idx != i);
                    gapIndexes = gapIndexes.Select(i => i + 2).ToArray();
                    i = i + 2;

                }
                else
                {
                    t.Add(t[i - 1] + unitTimeSlot);
                }
            }

            signature.SetFeature(Features.X, x);
            signature.SetFeature(Features.Y, y);
            signature.SetFeature(Features.T, t);
        }

        /// <summary>
        /// Inserts timestamps for border points of gaps in an online signature
        /// </summary>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="timestamps">Timestamps of the signature</param>
        /// <param name="difference">Defines the difference between the inserted timestamps and their neighbour timestamp</param>
        public static void InsertTimestampsForGapBorderPoints(int[] gapIndexes, List<double> timestamps, double difference)
        {
            //Filter the first point of th signature from gaps
            gapIndexes = Array.FindAll(gapIndexes, i => i != 0);

            foreach (var index in gapIndexes.Reverse())
            {
                timestamps.Insert(index, timestamps[index] - difference); //insert timestamp of the point which is after the gap
                timestamps.Insert(index, timestamps[index - 1] + difference); //insert timestamp of the point which is before the gap
            }
        }

        /// <summary>
        /// Insert PenUp values for border points of gaps in an online signature
        /// </summary>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="penDownValues">PenDown values of the signature</param>
        public static void InsertPenUpValuesForGapBorderPoints(int[] gapIndexes, List<bool> penDownValues)
        {
            foreach (var index in gapIndexes.Reverse())
            {
                penDownValues[index] = true; //set pen down point after the gap

                if (index != 0)
                {
                    penDownValues.Insert(index, false); //insert pen up point at the end of the gap
                    penDownValues.Insert(index, false); //insert pen up point in the beginning of the gap
                    penDownValues[index - 1] = true; //set pen down point before the gap
                }
            }
        }

        /// <summary>
        /// Insert zero pressure values for border points of gaps in an online signature
        /// </summary>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="pressureValues">Pressure values of the signature</param>
        public static void InsertPressureValuesForGapBorderPoints(int[] gapIndexes, List<double> pressureValues)
        {
            //Filter the first point of th signature from gaps
            gapIndexes = Array.FindAll(gapIndexes, i => i != 0);

            foreach (var index in gapIndexes.Reverse())
            {
                pressureValues.Insert(index, 0); //insert zero pressure point at the end of the gap
                pressureValues.Insert(index, 0); //insert zero pressure point in the beginning of the gap
            }
        }

        /// <summary>
        /// Insert feature values for border points of gaps in an online signature using duplicated neighbour values
        /// </summary>
        /// <typeparam name="T">Type of the values in featureValues</typeparam>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="featureValues">Feature values of the signature</param>
        public static void InsertDuplicatedValuesForGapBorderPoints<T>(int[] gapIndexes, List<T> featureValues)
        {
            //Filter the first point of th signature from gaps
            gapIndexes = Array.FindAll(gapIndexes, i => i != 0);

            foreach (var index in gapIndexes.Reverse())
            {
                featureValues.Insert(index, featureValues[index]); //insert value of the point which is after the gap
                featureValues.Insert(index, featureValues[index - 1]); //insert value of the point which is before the gap
            }
        }
    }
}
