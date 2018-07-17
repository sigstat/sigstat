using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace SigStat.Common
{
    [Feature("SigStat.Loop")]
    public class Loop
    {
        public PointF Center { get; set; }
        public RectangleF Bounds { get; set; }

        public List<PointF> Points { get; set; }

        static Loop()
        {
        }
    }
}
