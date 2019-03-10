using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;

namespace SigStat.Common.Transforms
{
    // TODO: Noise

    /// <summary>
    /// Generates a realistic looking image of the Signature based on standard features. Uses blue ink and white paper. It does NOT save the image to file.
    /// <para>Default Pipeline Input: X, Y, Button, Pressure, Azimuth, Altitude <see cref="Features"/></para>
    /// <para>Default Pipeline Output: <see cref="Features.Image"/></para>
    /// </summary>
    public class RealisticImageGenerator : PipelineBase, ITransformation
    {

        [Input]
        FeatureDescriptor<List<double>> X { get; set; } = Features.X;

        [Input]
        FeatureDescriptor<List<double>> Y { get; set; } = Features.Y;

        [Input]
        FeatureDescriptor<List<bool>> Button { get; set; } = Features.Button;

        [Input]
        FeatureDescriptor<List<double>> Pressure { get; set; } = Features.Pressure;

        [Input]
        FeatureDescriptor<List<double>> Altitude { get; set; } = Features.Altitude;

        [Input]
        FeatureDescriptor<List<double>> Azimuth { get; set; } = Features.Azimuth;

        [Output("RealisticImage")]
        FeatureDescriptor<Image<Rgba32>> OutputImage { get; set; } = Features.Image;

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

        /// <summary> Initializes a new instance of the <see cref="RealisticImageGenerator"/> class with specified settings. </summary>
        /// <param name="resolutionX">Output image width.</param>
        /// <param name="resolutionY">Output image height.</param>
        public RealisticImageGenerator(int resolutionX, int resolutionY)
        {
            w = resolutionX;
            h = resolutionY;
            penScale = Math.Min(w,h) / 300 + 0.5f;
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            xs = signature.GetFeature(X);
            ys = signature.GetFeature(Y);
            pendowns = signature.GetFeature(Button);
            ps = signature.GetFeature(Pressure);
            alts = signature.GetFeature(Altitude);
            azs = signature.GetFeature(Azimuth);
            //+ egyeb ami kellhet

            Image<Rgba32> img = new Image<Rgba32>(w, h);
            img.Mutate(ctx => ctx.Fill(bg));

            int len = xs.Count;
            List<PointF> points = new List<PointF>();
            double maxval = 1;
            double minval = Double.PositiveInfinity;
            xs.ForEach(x => {
                maxval = Math.Max(maxval, x);
                minval = Math.Min(minval, x);
            });
            ys.ForEach(y => {
                maxval = Math.Max(maxval, y);
                minval = Math.Min(minval, y);
            });
            for (int i = 0; i < len; i++)
            {
                points.Add(ToImageCoords(xs[i], ys[i], minval, maxval));
                Progress = (int)(i / (double)len * 90);
            }
            DrawRealisticLines(img, points);




            signature.SetFeature(OutputImage, img);
            Progress = 100;
            this.LogInformation( "Realistic image generation done.");
        }

        private PointF ToImageCoords(double x, double y, double minval, double maxval)
        {
            //ha x-et w-vel, y-t pedig h-val szoroznank, akkor torzulna
            //megtartjuk az aranyokat ugy, hogy w es h bol a kisebbiket valasztjuk (igy biztos belefer a kepbe minden)
            int minres = Math.Min(w, h);

            int frame = minres / 20;//keretet hagyunk, hogy ne a kep legszelerol induljon

            double mx = (x - minval) / (maxval - minval);//0-1
            double my = (1 - (y - minval) / (maxval - minval));

            //TODO: center

            //Y-okat meg kell forditani, hogy ne legyen fejjel lefele a kepen
            //betesszuk a kep kozepere is
            return new PointF(
                (float)(frame + mx * (minres - frame * 2)),
                (float)(frame + my * (minres - frame * 2))
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
                        {
                            continue;
                        }
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
        private static float Lerp(float t0, float t1, float t)
        {
            return (1 - t) * t0 + t * t1;
        }

    }
}
