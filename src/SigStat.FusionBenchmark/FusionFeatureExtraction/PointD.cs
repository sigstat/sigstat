using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    public class PointD
    {
        public double X { get; set; }
    
        public double Y { get; set; }
    
        public void SetXY(double x, double y)
        {
            X = x;
            Y = y;
        }
        public PointD()
        {
            X = 0.0;
            Y = 0.0;
        }
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
        public PointD(int x, int y)
        {
            X = (double)x;
            Y = (double)y;
        }
        public PointD(Point point)
        {
            X = (double)point.X;
            Y = (double)point.Y;
        }
    }
    
}
