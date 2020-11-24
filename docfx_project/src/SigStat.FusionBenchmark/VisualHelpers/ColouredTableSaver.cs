using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.VisualHelpers
{
    class ColouredTableSaver
    {
        public static void Save(double[][] table, string path )
        {
            var img = new Image<Rgba32>(table.GetLength(0), table.GetLength(1));


        }
    }
}
