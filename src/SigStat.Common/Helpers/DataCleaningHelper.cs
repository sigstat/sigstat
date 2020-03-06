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
            var x = signature.GetFeature(Features.X);
            var y = signature.GetFeature(Features.Y);

            if (x.Count != y.Count)
            {
                throw new ArgumentException("The length of X and Y are not the same");
            }

            var t = new List<double>(x.Count);
            int n = x.Count - 1;
            for (int i = 0; i < n; i++)
            {


            }
         }

        /// <summary>
        /// Inserts timestamps for border points of gaps in an online signature
        /// </summary>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="timestamps">Timestamps of the signature</param>
        /// <param name="difference">Defines the difference between the inserted timestamps and their neighbour timestamp</param>
        public static void InsertTimestampsForGapBorderPoints(int[] gapIndexes, List<double> timestamps, double difference)
        {
            foreach (var index in gapIndexes.Reverse())
            {
                timestamps.Insert(index, timestamps[index] - difference); //insert timestamp of the point which is after the gap

                if (index != 0)
                {
                    timestamps.Insert(index, timestamps[index - 1] + difference); //insert timestamp of the point which is before the gap
                }
            }
        }

        /// <summary>
        /// Insert PenUp values for border points of gaps in an online signature
        /// </summary>
        /// <param name="gapIndexes">Indexes of points where the gaps end in the signature</param>
        /// <param name="penUpValues">PenUp values of the signature</param>
        public static void InsertPenUpValuesForGapBorderPoints(int[] gapIndexes, List<bool> penUpValues)
        {
            foreach (var index in gapIndexes.Reverse())
            {
                penUpValues[index] = false; //set pen down point after the gap
                penUpValues.Insert(index, true); //insert pen up point at the end of the gap

                if (index != 0)
                {
                    penUpValues.Insert(index, true); //insert pen up point in the beginning of the gap
                    penUpValues[index - 1] = false; //set pen down point before the gap
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
            foreach (var index in gapIndexes.Reverse())
            {
                pressureValues.Insert(index, 0); //insert zero pressure point at the end of the gap

                if (index != 0)
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
            foreach (var index in gapIndexes.Reverse())
            {
                featureValues.Insert(index, featureValues[index]); //insert value of the point which is after the gap

                if (index != 0)
                    featureValues.Insert(index, featureValues[index - 1]); //insert value of the point which is before the gap
            }
        }
    }
}
