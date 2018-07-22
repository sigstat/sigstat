using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MnMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix;

namespace Alairas.Common
{
    class GreedyMatchingAlgorithm: ILogger, IProgress
    {
        #region ILogger, IProgress
        public event EventHandler<ProgressEventArgs> ProgressChanged;

        protected void DoProgressChanged(int percentReady)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressEventArgs(percentReady));
            }
        }

        protected void WriteLog(string message)
        {
            if (Log != null)
            {
                Log(this, new LogEventArgs(message));
            }
        }

        protected void WriteLog(string format, params object[] args)
        {
            WriteLog(string.Format(format, args));
        }
        public event LogEventHandler Log;
        #endregion

        public void RunAlgorithm(MnMatrix graph, int[] copulationVerticesX, int[] copulationVerticesY)
        {
            copulationVerticesX.SetValues(-1);
            copulationVerticesY.SetValues(-1);
            var array = graph.ToArray();
            for (int i = 0; i < Math.Min(graph.RowCount, graph.ColumnCount); i++)
            {
                var max = array.Max();
                if (max == double.MinValue) break;
                var pos = array.IndexOf(max);
                copulationVerticesX[pos.Item1] = pos.Item2;
                copulationVerticesY[pos.Item2] = pos.Item1;
                array.SetColumn(pos.Item1, double.MinValue);
                array.SetRow(pos.Item2, double.MinValue);
            }

            //for (int i = 0; i < copulationVerticesY.Length; i++)
            //{
            //    copulationVerticesY[i] = copulationVerticesX.IndexOf(i);
            //}

        }
    }
}
