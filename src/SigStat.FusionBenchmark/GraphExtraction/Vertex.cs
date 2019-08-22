using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Drawing;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public class Vertex
    {
        public List<Vertex> Neighbours { get; set; }

        public Point Pos { get; set; }

        public PointF PosF { get; set; }

        public bool On { get; set; }

        public int Rutovitz { get; set; }

        public Vertex(Point pos, bool on = true)
        {
            Pos = pos;
            PosF = new PointF((float) pos.X, (float)pos.Y);
            Neighbours = null;
            On = on;
            Rutovitz = -1;
        }


        public override bool Equals(object obj)
        {
            Vertex rhs = obj as Vertex;
            return ReferenceEquals(this, obj) || 
                (!ReferenceEquals(obj, null) && (rhs.Pos.X == this.Pos.X && rhs.Pos.Y == this.Pos.Y));
        }
        
        private static readonly VertexType[] rutovitz = new VertexType[5]
                                                {
                                                    VertexType.Endpoint,
                                                    VertexType.Endpoint,
                                                    VertexType.ConnectionPoint,
                                                    VertexType.CrossingPoint,
                                                    VertexType.CrossingPoint
                                                };  
        
        public VertexType PointType  
        {
            get
            {
                return rutovitz[this.Rutovitz];
            }
        }

        public static bool AreNeighbours(object objL, object objR)
        {
            Vertex lhs = objL as Vertex;
            Vertex rhs = objR as Vertex;
            return Math.Abs(lhs.Pos.X - rhs.Pos.X) <= 1 && Math.Abs(lhs.Pos.Y - rhs.Pos.Y) <= 1 && !lhs.Equals(rhs) 
                   && lhs.On && rhs.On;
        }

        public int Degree
        {
            get
            {
                return Neighbours.Count;
            }
        }
    }
}
