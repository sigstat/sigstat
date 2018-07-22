using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace SigStat.Common
{
    [Feature("Baseline")]
    public class Baseline
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }
        public Baseline()
        {
        }

        public Baseline(int x1, int y1, int x2, int y2)
        {
            Start = new PointF(x1, y1);
            End = new PointF(x2, y2);
        }

        public override string ToString()
        {
            return $"Baseline {Start}-{End}";
        }
    }
}
