using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.Processing.Drawing.Pens;
using SixLabors.Primitives;
using SigStat.Common.Helpers;
using SixLabors.Shapes;

namespace SigStat.Common.PipelineItems.Transforms
{
    public class BinaryRasterizer : PipelineBase, ITransformation
    {
        private readonly int w;
        private readonly int h;
        private readonly float penwidth;
        Byte4 fg = new Byte4(0, 0, 0, 255);
        Byte4 bg = new Byte4(255, 255, 255, 255);
        GraphicsOptions noAA = new GraphicsOptions(false);//aa kikapcs, mert binarisan dolgozunk es ne legyenek szakadasok
        Pen<Byte4> pen;

        public BinaryRasterizer(int resolutionX, int resolutionY, float penwidth)
        {
            this.w = resolutionX;
            this.h = resolutionY;
            this.penwidth = penwidth;
            pen = new Pen<Byte4>(fg, penwidth);
        }

        public void Transform(Signature signature)
        {
            List<double> xs = signature.GetFeature(Features.X);
            List<double> ys = signature.GetFeature(Features.Y);
            List<int> pendowns = signature.GetFeature(Features.Button);
            List<double> ps = signature.GetFeature(Features.Pressure);
            List<int> alts = signature.GetFeature(Features.Altitude);
            List<int> azs = signature.GetFeature(Features.Azimuth);
            //+ egyeb ami kellhet

            //TODO: X,Y legyen normalizalva, normalizaljuk ha nincs, ahhoz kell az Extrema, ..

            Image<Byte4> img = new Image<Byte4>(w, h);
            img.Mutate(ctx => ctx.Fill(bg));

            int len = xs.Count;
            List<IPath> paths = new List<IPath>();
            List<PointF> points = new List<PointF>();
            for (int i=0;i<len;i++)
            {
                if (pendowns[i]>0)
                {
                    points.Add(new PointF((float)(xs[i] * w), (float)(ys[i] * w)));//y-t nem h-val szorozzuk, mert akkor torzulna
                }
                else
                {
                    if(points.Count>0)
                        img.Mutate(ctx => DrawLines(ctx, points));
                    points = new List<PointF>();
                    points.Add(new PointF((float)(xs[i] * w), (float)(ys[i] * w)));
                }
                Progress = (int)(i / (double)len * 90);
            }
            img.Mutate(ctx => DrawLines(ctx, points));

            bool[,] b = new bool[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    b[x, y] = (img[x, y] == fg);

            signature.SetFeature(FeatureDescriptor<bool[,]>.Descriptor("Binarized"), b);
            Progress = 100;
            Log(LogLevel.Info, "Rasterization done.");
        }

        void DrawLines(IImageProcessingContext<Byte4> ctx, List<PointF> ps)
        {
            ctx.DrawLines(noAA, pen, ps.ToArray());
            ctx.DrawLines(noAA, pen, ps.ToArray());// 2x kell meghivni hogy mukodjon??
        }
    }
}
