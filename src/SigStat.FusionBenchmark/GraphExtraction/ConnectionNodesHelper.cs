using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public static class ConnectionNodesHelper
    {
        public static ConnectionNode FindConnectionNode(this List<ConnectionNode> connectionNodes, Vertex vertex)
        {
            foreach (var connectionNode in connectionNodes)
            {
                foreach (var p in connectionNode)
                {
                    if (p.Equals(vertex) || Vertex.AreNeighbours(vertex, p))
                    {
                        return connectionNode;
                    }
                }
            }
            return null;
        }

        public static void Add(this List<ConnectionNode> connectionNodes, Vertex vertex)
        {
            var connectionNode = connectionNodes.FindConnectionNode(vertex);
            if (connectionNode == null)
            {
                connectionNode = new ConnectionNode();
                connectionNodes.Add(connectionNode);
            }
            connectionNode.Add(vertex);
        }

        public static void Add(this List<ConnectionNode> connectionNodes, List<Vertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                connectionNodes.Add(vertex);
            }
        }

        public static void Add(this List<ConnectionNode> connectionNodes, StrokeComponent comp)
        {
            var stroke = comp.Strokes[0];
            connectionNodes.Add(stroke);
        }
    }
}
