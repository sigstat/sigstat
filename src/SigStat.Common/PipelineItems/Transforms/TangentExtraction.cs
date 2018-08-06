using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    public class TangentExtraction : PipelineBase, ITransformation
    {
        FeatureDescriptor<List<double>> tangentFd;
        public TangentExtraction()
        {
            //itt letre kell hozni azt a feature descriptort, amit ki fog szamolni. 
            //Kulonben a kesobbi pipeline elemek inicializalasanal nem talalnank.
            //TODO: ezt talan lehetne automatizalni: Ha olyan feature descriptort kerunk le ami nincs, akkor letrehozzuk
            tangentFd = FeatureDescriptor<List<double>>.Descriptor("Tangent");
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
                Progress += 100 / xs.Count-2;
            }
            res.Insert(0, res[0]);//elso
            res.Add(res[res.Count - 1]);//utolso
            res.Add(res[res.Count - 1]);//utolso
            signature[tangentFd] = res;
            Progress = 100;
        }
    }
}
