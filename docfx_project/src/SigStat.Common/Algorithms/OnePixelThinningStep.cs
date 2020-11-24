using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Algorithms
{
    /// <summary>
    /// One pixel thinning algorithm.
    /// Use this after <see cref="HSCPThinningStep"/> to generate final skeleton.
    /// </summary>
    class OnePixelThinningStep
    {
        /// <summary>
        /// Gets whether the last <see cref="Scan(bool[,])"/> call was effective.
        /// </summary>
        public bool? ResultChanged { get; private set; } = null;

        readonly PatternMatching3x3 m1;
        readonly PatternMatching3x3 m2;

        public OnePixelThinningStep()
        {
            //https://homepages.inf.ed.ac.uk/rbf/HIPR2/thin.htm
            //pattern1:
            //000
            //?1?
            //111
            bool?[,] p1 = {
                { false, false, false },
                { null, true, null },
                { true, true, true } };
            m1 = new PatternMatching3x3(p1);

            //pattern2:
            //?00
            //110
            //?1?
            bool?[,] p2 = {
                { null, false, false },
                { true, true, false },
                { null, true, null } };
            m2 = new PatternMatching3x3(p2);

        }

        /// <summary>
        /// Does one step of the thinning. Call it iteratively while ResultChanged.
        /// Scans the input matrix and generates a 1-pixel thinned version.
        /// </summary>
        /// <param name="binaryImage">Binary raster.</param>
        /// <returns>Thinned binary raster.</returns>
        public bool[,] Scan(bool[,] binaryImage)
        {
            int w = binaryImage.GetLength(0);
            int h = binaryImage.GetLength(1);
            bool[,] b = binaryImage.Clone() as bool[,];
            ResultChanged = false;

            bool[,] res = new bool[w, h];
            for (int i = 1; i < w - 1; i++)
            {
                for (int j = 1; j < h - 1; j++)
                {
                    res[i, j] = b[i, j];
                    if (b[i, j])
                    {
                        bool[,] ns = { //szomszed 3x3
                            { b[i - 1, j - 1] , b[i, j - 1] , b[i + 1, j - 1] },
                            { b[i - 1, j] , b[i, j] , b[i + 1, j] },
                            { b[i - 1, j + 1] , b[i, j + 1] , b[i + 1, j + 1] }
                        };

                        if (m1.RotMatch(ns) || m2.RotMatch(ns))
                        {
                            b[i, j] = false;//Ez hianyzott a helyes mukodeshez (pl. 4 szomszed igy nem fog torlodni)
                            res[i, j] = false;
                            ResultChanged = true;
                        }
                    }
                }
            }

            return res;
        }
    }
}
