using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.Processing.Drawing.Pens;
using SixLabors.Primitives;
using SixLabors.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms
{
    /// <summary>
    /// Pen width is auto-approximated based on resolution.
    /// Light Blue Foreground, White Background.
    /// TODO: Noise.
    /// </summary>
    public class RealisticImageGenerator : PipelineBase, ITransformation
    {
        private readonly int w;
        private readonly int h;
        private readonly float penScale;
        private readonly Rgba32 fg = Rgba32.LightBlue;
        private readonly Rgba32 bg = Rgba32.White;

        private List<double> xs;
        private List<double> ys;
        private List<bool> pendowns;
        private List<double> ps;
        private List<double> alts;
        private List<double> azs;

        public RealisticImageGenerator(int resolutionX, int resolutionY)
        {
            w = resolutionX;
            h = resolutionY;
            penScale = Math.Min(w,h) / 300 + 0.5f;
            this.Output(Features.Image);
        }

        public void Transform(Signature signature)
        {
            xs = signature.GetFeature(Features.X);
            ys = signature.GetFeature(Features.Y);
            pendowns = signature.GetFeature(Features.Button);
            ps = signature.GetFeature(Features.Pressure);
            alts = signature.GetFeature(Features.Altitude);
            azs = signature.GetFeature(Features.Azimuth);
            //+ egyeb ami kellhet

            Image<Rgba32> img = new Image<Rgba32>(w, h);
            img.Mutate(ctx => ctx.Fill(bg));

            int len = xs.Count;
            List<IPath> paths = new List<IPath>();
            List<PointF> points = new List<PointF>();
            for (int i = 0; i < len; i++)
            {
                /*if (pendowns[i])
                {
                    points.Add(ToImageCoords(xs[i], ys[i]));
                }
                else
                {
                    if (points.Count > 0)
                        DrawLines(img, points);
                    points = new List<PointF>();
                    points.Add(ToImageCoords(xs[i], ys[i]));
                }*/
                points.Add(ToImageCoords(xs[i], ys[i]));
                Progress = (int)(i / (double)len * 90);
            }
            DrawRealisticLines(img, points);




            signature.SetFeature(OutputFeatures[0], img);
            Progress = 100;
            Log(LogLevel.Info, "Realistic image generation done.");
        }

        private PointF ToImageCoords(double x, double y)
        {
            //ha x-et w-vel, y-t pedig h-val szoroznank, akkor torzulna
            //megtartjuk az aranyokat ugy, hogy w es h bol a kisebbiket valasztjuk (igy biztos belefer a kepbe minden)
            int m = Math.Min(w, h);

            int frame = m / 20;//keretet hagyunk, hogy ne a kep legszelerol induljon

            //Y-okat meg kell forditani, hogy ne legyen fejjel lefele a kepen
            //betesszuk a kep kozepere is
            return new PointF(
                (float)(frame + x * (m - frame * 2) + (w - m) / 2),
                (float)(frame + (1 - y) * (m - frame * 2) + (h - m) / 2)
            );
        }

        private void DrawRealisticLines(Image<Rgba32> img, List<PointF> points)
        {
            int maxtiers = 4;
            for (int tier = 0; tier < maxtiers; tier++)
            {
                float tierRadius = 0.05f+ 0.95f * (1.0f - tier / (float)maxtiers);
                float tierDarken = 0.7f + 0.3f * (1.0f - tier / (float)maxtiers);
                img.Mutate(ctx =>
                {
                    for (int i = 0; i < points.Count - 1; i++)
                    {//each point
                        PointF iP = points[i];
                        PointF jP = points[i + 1];
                        if (!pendowns[i + 1])
                            continue;
                        //kb mennyit kell a ket pont koze rajzolni: tavolsaguktol fugg
                        float step = 1.5f / (Math.Abs(iP.X - jP.X) + Math.Abs(iP.Y - jP.Y));//lehetne euclidean is de minek es draga
                        for (float t = 0.0f; t <= 1.0f; t += step)
                        {
                            PointF p = new PointF(
                                Lerp(iP.X, jP.X, t),
                                Lerp(iP.Y, jP.Y, t)
                            );
                            float scale = 0.5f + penScale * Lerp((float)alts[i], (float)alts[i + 1], t) * Lerp((float)ps[i], (float)ps[i + 1], t);//pen width depends on altitude and pressure
                            Rgba32 color = fg;
                            float darker = 0.8f + 0.2f * (1 - Lerp((float)ps[i], (float)ps[i + 1], t));//color depends on pressure
                            color.R = (byte)(tierDarken * darker * fg.R);
                            color.G = (byte)(tierDarken * darker * fg.G);
                            //color.B = (byte)(darker * fg.B);

                            float ellipseRadius = scale * tierRadius;
                            p.X -= ellipseRadius / 2.0f;
                            p.Y -= ellipseRadius / 2.0f;
                            ctx.Fill(color, new EllipsePolygon(p, ellipseRadius));
                        }

                        double tierProgress = i / (double)(points.Count - 1);
                        Progress = (int)((tier * (1 - tierProgress) + (tier + 1) * tierProgress) / maxtiers * 100.0);
                    }
                });
            }
        }

        /// <summary>
        /// Basic linear interpolation
        /// </summary>
        /// <param name="t0"></param>
        /// <param name="t1"></param>
        /// <param name="t">0.0f to 1.0f</param>
        /// <returns></returns>
        private float Lerp(float t0, float t1, float t)
        {
            return (1 - t) * t0 + t * t1;
        }

    }
}
