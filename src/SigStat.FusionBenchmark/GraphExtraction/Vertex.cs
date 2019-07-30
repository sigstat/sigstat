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
using SigStat.FusionBenchmark.FusionFeatureExtraction;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    public class Vertex: ObjectWithID
    {
        public int ID { get; set; }

        public List<Vertex> Neighbours { get; set; }

        public Vertex Parent { get; set; }

        public Point Pos { get; set; }

        public PointD PosD { get; set; }

        public bool On { get; set; }

        public double[] RelPos { get; set; }

        public Vertex(int iD, Point pos, bool on = true)
        {
            ID = iD;
            Pos = pos;
            Neighbours = null;
            Parent = null;
            On = on;
            PosD = new PointD();
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
