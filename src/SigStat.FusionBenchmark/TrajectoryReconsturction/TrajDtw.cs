using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using Newtonsoft.Json;
using SigStat.FusionBenchmark.GraphExtraction;
using SigStat.FusionBenchmark.FusionFeatureExtraction;

namespace SigStat.FusionBenchmark.TrajectoryReconsturction
{
    class TrajDtw
    {
        public enum DirectionInDtw { Match, Insert, Delete }

        public static double Dtw(Queue<double[]> sequence1, Queue<double[]> sequence2, Func<double[], double[], double> distance)
        {
            var vector1 = sequence1.ToArray();
            var vector2 = sequence2.ToArray();
            int n = vector1.Length;
            int m = vector2.Length;
            var dtw = new double[n + 1, m + 1];
            var fromCell = new DirectionInDtw[n + 1, m + 1];
            dtw.SetValues(double.MaxValue);
            dtw[0, 0] = 0.0;

            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < m + 1; j++)
                {
                    var cost = distance(vector1[i - 1], vector2[j - 1]);
                    dtw[i, j] = cost + MathHelper.Min(
                        dtw[i - 1, j], // Insert 
                        dtw[i, j - 1], // Delete
                        dtw[i - 1, j - 1]); // Match
                    fromCell[i, j] = DirectionInMatrix(dtw, i, j);
                }
            }
            Point mainMatch = BackTrack(fromCell);
            for (int i = 0; i < mainMatch.X; i++)
            {
                sequence1.Dequeue();
            }
            for (int j = 0; j < mainMatch.Y; j++)
            {
                sequence2.Dequeue();
            }
            return (dtw[mainMatch.X, mainMatch.Y] / (mainMatch.X + mainMatch.Y));
        }

        private static Point BackTrack(DirectionInDtw[,] fromCell)
        {
            Point res = new Point(fromCell.GetLength(0) - 1, fromCell.GetLength(1) - 1);
            if (fromCell[res.X, res.Y] == DirectionInDtw.Match)
                return res;
            var direction = fromCell[res.X, res.Y];
            while (fromCell[res.X, res.Y] == direction && res.X >= 0 && res.Y >= 0)
            {
                DirectPoint(ref res, fromCell[res.X, res.Y]);
            }
            return res;
        }

        private static void DirectPoint(ref Point p, DirectionInDtw direction)
        {
            if (direction == DirectionInDtw.Match)
            { p.X--; p.Y--; }
            else if (direction == DirectionInDtw.Insert)
            { p.X--; }
            else if (direction == DirectionInDtw.Delete)
            { p.Y--; }
            else { throw new Exception(); }
        }

        private static DirectionInDtw DirectionInMatrix(double[,] dtw, int i, int j)
        {
            double er = MathHelper.Min(
                        dtw[i - 1, j],
                        dtw[i, j - 1],
                        dtw[i - 1, j - 1]);
            if (er == dtw[i - 1, j - 1])
                return DirectionInDtw.Match;
            if (er == dtw[i - 1, j])
                return DirectionInDtw.Insert;
            if (er == dtw[i, j - 1])
                return DirectionInDtw.Delete;
            throw new Exception();
        }

        public static double EuclideanDistance(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException();
            }

            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                double dist = vector1[i] - vector2[i];
                sum += dist * dist;
            }
            return Math.Sqrt(sum);
        }

        private static void DiffSequence(double[][] sequence, int idx, int dist)
        {
            int n = sequence.GetLength(0);
            if (n <= dist + 1)
            {
                return;
            }
            for (int i = 0; i < n - dist; i++)
            {
                sequence[i][idx] = sequence[i + dist][idx] - sequence[i][idx];
            }
            for (int i = n - dist; i < n; i++)
            {
                sequence[i][idx] = sequence[i - 1][idx];
            }
        }

        public static double MyDistance(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException();
            }
            double sum = 0;
            for (int i = 0; i < 2; i++)
            {
                double dist = vector1[i] - vector2[i];
                sum += Math.Abs(dist);
            }
            Section section1 = new Section(new PointD(0.0, 0.0), new PointD(vector1[2], vector1[3]));
            Section section2 = new Section(new PointD(0.0, 0.0), new PointD(vector2[2], vector2[3]));
            sum += Math.Abs(DOSBasedFeature.CalculateDiffAngle(section1, section2)) / Math.PI;
            return sum;
        }

        public static Func<double[], double[], double> Optimal()
        {
            return TrajDtw.EuclideanDistance;
        }

    }

   
}
