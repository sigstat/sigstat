using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.GraphExtraction
{
    class StrokeCollection: MyCollection<StrokeComponent>
    {
        public StrokeComponent Get(Stroke stroke)
        {
            return Get(stroke.Component.ID);
        }

        public List<Stroke> GetAllStrokes()
        {
            var list = new List<Stroke>();
            foreach (var comp in this.Values)
            {
                list.Add(comp.A);
                list.Add(comp.B);
            }
            return list;
        }

        public void CheckStrokeComponents()
        {
            foreach (var comp in this.Values)
            {
                if (comp.A.Start().ID != comp.B.End().ID || comp.A.End().ID != comp.B.Start().ID)
                {
                    throw new Exception();
                }
            }
        }

    }
}
