using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public static class Translations
    {
        public static List<double[]> MergeLists(List<double>[] lists)
        {
            int n = lists[0].Count;
            int m = lists.Length;
            foreach (var list in lists)
            {
                if (list.Count != n)
                {
                    throw new ArgumentException();
                }
            }
            var res = new List<double[]>();
            for (int i = 0; i < n; i++)
            {
                double[] newVal = new double[m];
                for (int j = 0; j < m; j++)
                {
                    newVal[j] = lists[j][i];
                }
                res.Add(newVal);
            }
            return res;
        }
        
        public static List<double> Normalize(List<double> values, double left, double right)
        {
            if (left == right)
            {
                throw new ArgumentException();
            }
            List<double> res = new List<double>();
            double minVal = values.Min();
            double maxVal = values.Max();

            double divider = (maxVal - minVal) / (right - left);
            for (int i = 0; i < values.Count; i++)
            {
                double newVal = (values[i] - minVal) / divider;
                res.Add(newVal);
            }
            return res;
        }

        public static List<double> Normalize(List<double> values)
        {
            return Normalize(values, 0.0, 1.0);
        }

        public static List<double> GetDirections(List<double> xs, List<double> ys)
        {
            var directions = new List<double>();

            int n = xs.Count;
            if (n != xs.Count || n != ys.Count)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < n - 1; i++)
            {
                double dy = ys[i + 1] - ys[i];
                double dx = xs[i + 1] - xs[i];
                double newVal = Math.Atan2(dy, dx);
                directions.Add(newVal);
            }
            directions.Add(0.0);
            if (n != directions.Count)
            {
                throw new Exception();
            }
            return directions;
        }

        
    }
}
