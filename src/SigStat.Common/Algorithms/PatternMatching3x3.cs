using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Algorithms
{
    public class PatternMatching3x3
    {
        private bool?[,] p;

        /// <summary>
        /// ha patternben null ertek: don't care
        /// </summary>
        public PatternMatching3x3(bool?[,] pattern)
        {
            p = pattern;
        }

        /// <summary>
        /// Match the 3x3 input with the 3x3 pattern
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Match(bool[,] input)
        {
            for (int di = 0; di < 3; di++)
            {
                for (int dj = 0; dj < 3; dj++)
                {
                    if (p[di, dj] != null)
                        if (input[di, dj] != p[di, dj])
                            return false;
                    //else don't care
                }
            }
            return true;
        }

        /// <summary>
        /// Match the 3x3 input with the 3x3 pattern from all 4 directions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool RotMatch(bool[,] input)
        {
            for (int k = 0; k < 4; k++)
            {
                if (Match(input))
                    return true;
                p = Rotate(p);
            }
            return false;
        }

        /// <summary>
        /// Rotate a 3x3 pattern by 90d
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool?[,] Rotate(bool?[,] input)
        {
            bool?[,] result = new bool?[3, 3];

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result[3 - j - 1, i] = input[i, j];

            return result;
        }

    }
}
