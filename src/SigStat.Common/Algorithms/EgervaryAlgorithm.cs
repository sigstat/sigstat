using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using MnMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix;

namespace Alairas.Common
{
    public static class EgervaryAlgorithm
    {
        private static double _maximumEdge;

        private static double EPSILON
        {
            get { return 0.01; }
        }

        private static void Initialize(MnMatrix graph, double[] verticesXLabel, double[] verticesYLabel)
        {
            _maximumEdge = 0;
            for (int i = 0; i < verticesXLabel.Length; i++)
            {
                double max = 0;
                for (int j = 0; j < verticesYLabel.Length; j++)
                {
                    if (graph.At(i, j) > max) max = graph.At(i, j);
                }
                verticesXLabel[i] = max;
                if (max > _maximumEdge) _maximumEdge = max;
            }

            for (int j = 0; j < verticesYLabel.Length; j++)
            {
                verticesYLabel[j] = 0;
            }

        }

        private static DenseMatrix CreateEqualityGraph(MnMatrix graph, double[] verticesXLabel, double[] verticesYLabel)
        {
            DenseMatrix subGraph = new DenseMatrix(verticesXLabel.Length, verticesYLabel.Length);
            for (int i = 0; i < verticesXLabel.Length; i++)
            {
                for (int j = 0; j < verticesYLabel.Length; j++)
                {
                    if (Math.Abs(graph.At(i, j) - (verticesXLabel[i] + verticesYLabel[j])) < EPSILON) subGraph.At(i, j, 1);
                }
            }
            return subGraph;
        }

        private static int CountCopulation(IEnumerable<int> copulation)
        {
            return copulation.Count(t => t == -1);
        }

        public static void RunAlgorithm(double[,] graph, int[] copulationVerticesX, int[] copulationVerticesY)
        {
            RunAlgorithm(new DenseMatrix(graph), copulationVerticesX, copulationVerticesY);
        }

        public static void RunAlgorithm(MnMatrix graph, int[] copulationVerticesX, int[] copulationVerticesY)
        {
            double min = graph.ToColumnWiseArray().Min();
            if (min < 0)
            {
                for (int i = 0; i < graph.ColumnCount; i++)
                    for (int j = 0; j < graph.RowCount; j++)
                    {
                        graph[j, i] = graph[j, i] - i+1;

                    }
            }

            int limit = 0;
            if (copulationVerticesX.Length > copulationVerticesY.Length)
                limit = copulationVerticesX.Length - copulationVerticesY.Length;

            double[] verticesXLabel = new double[copulationVerticesX.Length];
            double[] verticesYLabel = new double[copulationVerticesY.Length];

            Initialize(graph, verticesXLabel, verticesYLabel);

            while (true)
            {
                for (int i = 0; i < copulationVerticesX.Length; i++)
                {
                    copulationVerticesX[i] = -1;
                }

                for (int i = 0; i < copulationVerticesY.Length; i++)
                {
                    copulationVerticesY[i] = -1;
                }

                DenseMatrix equalityGraph = CreateEqualityGraph(graph, verticesXLabel, verticesYLabel);

                int[,] vertices;
                HungarianAlgorithm.RunAlgorithm(equalityGraph, copulationVerticesX, copulationVerticesY, out vertices);

                if (CountCopulation(copulationVerticesX) == limit)
                {
                    return;
                }

                //if (CountCopulation(copulationVerticesY) == 0)
                //{
                //    return;
                //}

                var F1UF2 = new List<int>();
                var L2 = new List<int>();
                var L1UL3 = new List<int>();

                for (int i = 0; i < copulationVerticesX.Length; i++)
                {
                    if (vertices[0, i] == 0) F1UF2.Add(i);
                }


                for (int i = 0; i < copulationVerticesY.Length; i++)
                {
                    if (vertices[1, i] == 0) L2.Add(i);
                    else L1UL3.Add(i);
                }

                double minDelta = _maximumEdge + 1;
                foreach (var i in F1UF2)
                {
                    foreach (var j in L1UL3)
                    {
                        //                       if (graph[i, j] == -1) continue;
                        double delta = verticesXLabel[i] + verticesYLabel[j] - graph.At(i, j);
                        if (delta < minDelta) minDelta = delta;
                    }
                }

                foreach (var i in F1UF2)
                {
                    verticesXLabel[i] -= minDelta;
                }

                foreach (var i in L2)
                {
                    verticesYLabel[i] += minDelta;
                }
            }
        }
    }
}
