using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class TangentExtraction : ITransformation
    {
        FeatureDescriptor<List<double>> tangentfd;
        public TangentExtraction()
        {
            //itt letre kell hozni azt a feature descriptort, amit ki fog szamolni. 
            //Kulonben a kesobbi pipeline elemek inicializalasanal nem talalnank.
            //TODO: ezt talan lehetne automatizalni: Ha olyan feature descriptort kerunk le ami nincs, akkor letrehozzuk
            tangentfd = FeatureDescriptor<List<double>>.Descriptor("Tangent");//new FeatureDescriptor<List<double>>("Tangent", "Tangent");
        }

        public void Transform(Signature signature)
        {
            var xs = signature.GetFeature(Features.X);
            var ys = signature.GetFeature(Features.Y);

            List<double> res = new List<double>();
            for (int i = 1; i < xs.Count - 2; i++)
            {
                double dx = xs[i + 1] - xs[i - 1];
                double dy = ys[i + 1] - ys[i - 1];
                res.Add(Math.Atan2(dy, dx));
            }
            res.Insert(0, res[0]);//elso
            res.Add(res[res.Count - 1]);//utolso
            res.Add(res[res.Count - 1]);//utolso
            signature[tangentfd] = res;
        }
    }
}
