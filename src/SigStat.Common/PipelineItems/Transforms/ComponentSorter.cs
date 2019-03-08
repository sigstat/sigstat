using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Sorts Component order by comparing each starting X value, and finding nearest components.
    /// <para>Default Pipeline Input: (bool[,]) Components</para>
    /// <para>Default Pipeline Output: (bool[,]) Components</para>
    /// </summary>
    public class ComponentSorter : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<List<PointF>>> Input { get; set; }

        [Output("Components")]
        public FeatureDescriptor<List<List<PointF>>> Output { get; set; }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var components = signature.GetFeature(Input);

            //egyenkent megforditjuk ha kell (balrol jobbra megy kb)
            foreach (List<PointF> component in components)
            {
                if (component[0].X > component[component.Count - 1].X)  //OTLET: ide lehetne valami epsilon kornyezetet beallitani, hasznos pl ha B betu alja tul visszalog
                {
                    component.Reverse();
                }
            }

            //sorbamegyunk es egyik utan a kovetkezo az lesz amelyik a legkozelebb van
            List<List<PointF>> sorted = new List<List<PointF>>();
            List<PointF> curr = components[0];
            sorted.Add(curr);//elso X szerint
            components.RemoveAt(0);
            while (components.Count > 0)
            {
                double mindistance = Double.PositiveInfinity;
                int minindex = 1;
                for (int i = 0; i < components.Count; i++)
                {//megkeressuk a kovetkezo legkozelebbit
                    double d = Distance(curr, components[i]);
                    if (d < mindistance)
                    {
                        mindistance = d;
                        minindex = i;
                    }
                }
                //megtalaltuk a legkozelebbit
                curr = components[minindex];
                sorted.Add(curr);
                components.RemoveAt(minindex);
            }

            signature.SetFeature(Output, sorted);

        }

        /// <summary>
        /// Calculates distance between two components by comparing last and first points.
        /// Components that are left behind are in advantage.
        /// </summary>
        private static double Distance(List<PointF> curr, List<PointF> next)
        {
            double cx = curr[curr.Count - 1].X;//mostani vege
            double cy = curr[curr.Count - 1].Y;
            double nx = next[0].X;//kovetkezo eleje
            double ny = next[0].Y;

            //csel: elonyt adunk a hatra maradt szakaszoknak
            if (nx < cx)
            {//kozelebb tesszuk
                nx = cx;
                ny = (ny + cy) / 2.0;
            }

            return Math.Sqrt((cx - nx) * (cx - nx) + (cy - ny) * (cy - ny));
        }

    }
}
