using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common.Transforms
{
    //TODO: xml doc, ha kesz
    public class PrepareForThinning : PipelineBase, ITransformation
    {
        public PrepareForThinning()
        {
            this.Output(FeatureDescriptor.Get<bool[,]>("Prepared"));
        }

        public void Transform(Signature signature)
        {
            bool[,] b = signature.GetFeature<bool[,]>(InputFeatures[0]);
            int w = b.GetLength(0);
            int h = b.GetLength(1);

            for (int x = 1; x < w - 1; x++)
            {
                for (int y = 1; y < h - 1; y++)
                {
                    if (b[x, y] == false)
                    {
                        if (NeighbourhoodCount(b, x, y) > 6)
                        {
                            b[x, y] = true;
                        }
                    }
                }
            }
            //TODO: ehelyett Dilate, Erode lehet

            signature.SetFeature(OutputFeatures[0], b);
            Progress = 100;
        }

        private int NeighbourhoodCount(bool[,] b, int i, int j)
        {
            bool[] tmp = {
                b[i+1, j],
                b[i+1, j-1],
                b[i,   j-1],
                b[i-1, j-1],
                b[i-1, j],
                b[i-1, j+1],
                b[i,   j+1],
                b[i+1, j+1]
            };
            return tmp.Where(bi => bi == true).Count();
        }

    }
}
