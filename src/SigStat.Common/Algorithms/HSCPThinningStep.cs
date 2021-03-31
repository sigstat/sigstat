using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Algorithms
{
    //TODO: ez parhuzamosithato

    /// <summary>
    /// HSCP thinning algorithm
    /// http://www.ppgia.pucpr.br/~facon/Afinamento/1987holt.pdf
    /// </summary>
    public class HSCPThinningStep
    {
        /// <summary>
        /// Gets whether the last <see cref="Scan(bool[,])"/> call was effective.
        /// </summary>
        public bool? ResultChanged { get; private set; } = null;

        /// <summary>
        /// Does one step of the thinning. Call it iteratively while ResultChanged.
        /// </summary>
        /// <param name="b">Binary raster.</param>
        /// <returns>Thinned binary raster.</returns>
        public bool[,] Scan(bool[,] b)
        {
            int w = b.GetLength(0);
            int h = b.GetLength(1);
            ResultChanged = false;

            bool[,] deletable = FindDeletables(b);

            bool[,] o = new bool[w, h];
            for (int i = 1; i < w - 1; i++)
            {
                for (int j = 1; j < h - 1; j++)
                {
                    if (deletable[i, j])
                    {
                        bool[] nbs = Neighbourhood(b, i, j);

                        //proba: masik iranyok is
                        if ((nbs[2] && nbs[6] && (deletable[i - 1, j] || deletable[i + 1, j])) || //fuggoleges szomszedok vannak
                            (nbs[0] && nbs[4] && (deletable[i, j - 1] || deletable[i, j + 1])) || //vizszintes szomszedok vannak
                            (deletable[i - 1, j] && deletable[i - 1, j - 1] && deletable[i, j - 1]) ||
                            (deletable[i + 1, j] && deletable[i + 1, j + 1] && deletable[i, j + 1]) ||
                            (deletable[i + 1, j] && deletable[i + 1, j - 1] && deletable[i, j - 1]) ||
                            (deletable[i - 1, j] && deletable[i - 1, j + 1] && deletable[i, j + 1]))//sarkok
                        {
                            o[i, j] = true;
                        }

                        else
                        {
                            //ok, torolhetjuk
                            ResultChanged = true;
                            o[i, j] = false;
                        }

                    }
                    else
                    {
                        o[i, j] = b[i, j];
                    }
                }
            }

            return o;
        }

        private bool[,] FindDeletables(bool[,] b)
        {
            int w = b.GetLength(0);
            int h = b.GetLength(1);
            bool[,] deletable = new bool[w, h];
            for (int i = 1; i < w - 1; i++)
            {
                for (int j = 1; j < h - 1; j++)
                {
                    if (b[i, j])
                    {
                        bool[] nbs = Neighbourhood(b, i, j);
                        int ncnt = 0;//count neighbours
                        for (int ni = 0; ni < 8; ni++)
                        {
                            ncnt += nbs[ni] ? 1 : 0;
                        }

                        if (ncnt > 1 && ncnt < 7)
                        {//Gabor: min 1, max 7 szomszed kell a torleshez, >1 && <7
                            int CN = 0;//rutovitz crossing number
                            for (int ni = 0; ni < 7; ni++)
                            {
                                CN += (nbs[ni] == nbs[ni + 1]) ? 0 : 1;
                            }

                            CN += (nbs[7] == nbs[0]) ? 0 : 1;

                            if (CN == 2)
                            {
                                deletable[i, j] = true;
                            }
                        }
                    }
                }
            }

            return deletable;
        }

        /// <summary>
        /// Gets neighbour pixels in order.
        /// </summary>
        private bool[] Neighbourhood(bool[,] b, int i, int j)
        {
            // n[3] n[2] n[1]
            // n[4]  p   n[0]
            // n[5] n[6] n[7]
            return new [] {//8 szomszed sorrendben
                b[i+1, j],
                b[i+1, j-1],
                b[i,   j-1],
                b[i-1, j-1],
                b[i-1, j],
                b[i-1, j+1],
                b[i,   j+1],
                b[i+1, j+1]
            };
        }
    }
}
