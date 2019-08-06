using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public static class SkeletonHelper 
    {
        public static int GetRutovitz(this bool[,] skeleton, Point p)
        {
            return skeleton.GetRutovitz(p.X, p.Y);
        }

        public static int GetRutovitz(this bool[,] skeleton, int x, int y)
        {
            bool[] neighbourHood = skeleton.GetRutovitzNeighbourHood(x, y);
            int res = 0;
            int n = neighbourHood.Length;
            for (int i = 0; i < n; i++)
            {
                res += (neighbourHood[(i + 1) % n] ^ neighbourHood[i % n]) ? 1 : 0;
            }
            return res / 2;
        }

        public static bool[] GetRutovitzNeighbourHood(this bool[,] skeleton, int x, int y)
        {
            bool[] res = new bool[]
            {
                skeleton.Valid(x, y) && skeleton[x-1, y-1],
                skeleton.Valid(x, y) && skeleton[x-1, y],
                skeleton.Valid(x, y) && skeleton[x-1, y+1],
                skeleton.Valid(x, y) && skeleton[x, y+1],
                skeleton.Valid(x, y) && skeleton[x+1, y+1],
                skeleton.Valid(x, y) && skeleton[x+1, y],
                skeleton.Valid(x, y) && skeleton[x+1, y-1],
                skeleton.Valid(x, y) && skeleton[x, y-1],
            };
            return res;
        }

        public static bool Valid(this bool[,] skeleton, Point p)
        {
            return skeleton.Valid(p.X, p.Y);
        }

        public static bool Valid(this bool[,] skeleton, int x, int y)
        {
            return x >= 0 && x <= skeleton.GetLength(0) &&
                    y >= 0 && y <= skeleton.GetLength(1);
        }
    }
}
