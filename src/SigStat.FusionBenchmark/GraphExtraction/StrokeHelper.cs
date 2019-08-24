using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.FusionBenchmark.GraphExtraction
{

    public static class StrokeHelper
    {
        public static List<Stroke> FindNeighbours(this Stroke myStroke, List<Stroke> strokes, List<Stroke> connects)
        {
            List<Stroke> neighbours = new List<Stroke>();
            foreach (var stroke in strokes)
            {
                if (StrokesAreNeigbours(myStroke, stroke, connects))
                {
                    neighbours.Add(stroke);
                }
            }

            return neighbours;
        }

        private static bool StrokesAreNeigbours(Stroke fromStroke, Stroke toStroke, List<Stroke> connects)
        {
            //0. eset sajat maganak ne legyen szomszedja
            if (fromStroke.Component == toStroke.Component)
            {
                return false;
            }

            //1. eset End == Start ||  Neighbour(Start, End)
            if (fromStroke.End.Equals(toStroke.Start) || Vertex.AreNeighbours(fromStroke.Start, fromStroke.End))
            {
                return true;
            }

            //2. eset fromStroke -> rovidszakasz -> toStroke
            foreach (var stroke in connects)
            {
                if (ConnectStrokePredicate(fromStroke, toStroke, stroke))
                {
                    return true;
                }
            }
            return false;

        }

        public static bool ConnectStrokePredicate(Stroke fromStroke, Stroke toStroke, Stroke stroke)
        {
            //Ne az egyik stroke legyen
            if (stroke.Component == fromStroke.Component || stroke.Component == toStroke.Component)
            {
                return false;
            }
            //Legyen rovid
            if (stroke.Count > FusionPipelines.strokeConnectMaxLength)
            {
                return false;
            }
            //Legyen 3 - 3 croosing 
            if (stroke.Start.Rutovitz != 3 || stroke.End.Rutovitz != 3)
            {
                return false;
            }
            return true;
        }
    }

    
}
