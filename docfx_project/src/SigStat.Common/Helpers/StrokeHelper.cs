using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Helper class for locating and manipulating strokes in an online signature
    /// </summary>
    public static class StrokeHelper
    {
        /// <summary>
        /// Gets the strokes from an online signature with standard features. Note that
        /// the signature has to contain <see cref="Features.T"/> and <see cref="Features.Pressure"/>
        /// </summary>
        /// <param name="signature">An online signature with standard features</param>
        /// <returns></returns>
        public static List<StrokeInterval> GetStrokes(this Signature signature)
        {

            var timestamps = signature.GetFeature(Features.T);
            var pressures = signature.GetFeature(Features.Pressure);


            if (timestamps.Count != pressures.Count)
                throw new ArgumentException("Pressure and timestamp count is inconsistent", nameof(signature));

            List<StrokeInterval> strokeIntervals = new List<StrokeInterval>();

            if (timestamps.Count == 0)
                return new List<StrokeInterval>();

            if (timestamps.Count == 1)
                return new List<StrokeInterval>() { GetStroke(0, pressures[0]) };

            var timestampLength = timestamps.Select((ts, i) => ts - timestamps[i > 0 ? i - 1 : 0]).Skip(1).Median();

            var stroke = GetStroke(0, pressures[0]);
            strokeIntervals.Add(stroke);

            int index = 1;
            while (index < timestamps.Count)
            {
                if (timestamps[index] - timestamps[index - 1] > timestampLength * 2
                    || pressures[index] > 0 && stroke.StrokeType == StrokeType.Up
                    || pressures[index] <= 0 && stroke.StrokeType == StrokeType.Down)
                {
                    stroke.EndIndex = index - 1;
                    stroke = GetStroke(index-1, pressures[index]);
                    strokeIntervals.Add(stroke);
                }
                index++;
            }
            stroke.EndIndex = timestamps.Count - 1;
            return strokeIntervals;
        }

        /// <summary>
        /// Creates a <see cref="StrokeInterval"/> and initializes it with the given parameters
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pressure">The pressure.</param>
        /// <returns></returns>
        private static StrokeInterval GetStroke(int startIndex, double pressure)
        {
            return new StrokeInterval(startIndex, startIndex, pressure > 0 ? StrokeType.Down : StrokeType.Up);
        }

    }
}
