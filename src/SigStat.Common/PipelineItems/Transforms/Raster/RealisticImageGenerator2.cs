using System.Collections.Generic;
using SigStat.Common.Pipeline;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System.Linq;
using System;
using System.Numerics;

namespace SigStat.Common.PipelineItems.Transforms.Raster
{
    public class RealisticImageGenerator2 : PipelineBase, ITransformation
    {
        private int Frame { get; set; }
        private double CatmullRomAlpha { get; set; }

        private float CatmullRomStep = 0.01f;

        private double BaseIntensity = 1;

        private double OutDpi;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> describing the X coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> X { get; set; } = Features.X;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> describing the Y coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> Y { get; set; } = Features.Y;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> describing the time values of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> T { get; set; } = Features.T;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> describing the pressure values of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> Pressure { get; set; } = Features.Pressure;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> describing the pressure values of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<SizeF> Size { get; set; } = Features.Size;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> describing the stroke endings of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<bool>> PenDown { get; set; } = Features.PenDown;

        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> dots per inch
        /// </summary>
        public FeatureDescriptor<int> Dpi { get; set; } = Features.Dpi;

        public RealisticImageGenerator2(int frame, double CatmullRomAlpha, double OutDpi)
        {
            Frame = frame;
            this.CatmullRomAlpha = CatmullRomAlpha;
            this.OutDpi = OutDpi;
        }

        /// <summary>
        /// Output <see cref="FeatureDescriptor"/> describing the generated image of the signature
        /// </summary>
        [Output("RealisticImage")]
        public FeatureDescriptor<Image<Rgba32>> OutputImage { get; set; } = Features.Image;
        public void Transform(Signature signature)
        {
            List<double> Xs = signature.GetFeature(X);
            List<double> Ys = signature.GetFeature(Y);
            List<double> Ts = signature.GetFeature(T);
            List<double> CPs = signature.GetFeature(Pressure);
            List<bool> PenDowns = signature.GetFeature(PenDown);
            SizeF Size = signature.GetFeature(this.Size);
            int dpi = signature.GetFeature(Dpi);


            // Normalized controll pint pressure values
            List<double> NCPs = Normalize(CPs, CPs.Min<double>(), CPs.Max<double>() - CPs.Min<double>());

            // Signature controll points (normalized X-Y pairs) translated to image points
            List<PointF> ControllPoints = CreateImageControllPoints(dpi, Xs, Ys);

            // Calculating the velocities in each controll point
            List<Vector2> Velocities = CalculateVelocities(ControllPoints, Ts);
            // Extracting the length of the velocity vectors
            List<double> CVs = new List<double>();
            foreach (Vector2 v in Velocities)
            {
                CVs.Add(v.Length());
            }

            // Normalized velocity values (only the length of the vectors)
            List<double> NCVs = Normalize(CVs, CVs.Min(), CVs.Max() - CVs.Min());

            // Image points of the signature calculated from the controll points using
            // a curve fitting method.
            List<Point> Points = CalculateImagePoints(ControllPoints, PenDowns);

            // Fill missing pressure values between controll points
            List<double> NPs = LinearFill(NCPs, PenDowns);

            // Fill missing velocity values between controll points
            List<double> NVs = LinearFill(NCVs, PenDowns);

            Image<Rgba32> img = CreateImage(dpi, Size);

            // Set background color to white
            img.Mutate(ctx => ctx.Fill(Rgba32.White));

            // Draw the signature
            DrawSignature(img, Points, NPs, PenDowns, NVs);

            signature.SetFeature(OutputImage, img);
        }

        private Image<Rgba32> CreateImage(int dpi, SizeF size)
        {
            double scale = OutDpi / dpi;
            return new Image<Rgba32>((int)Math.Ceiling(size.Width * scale) + 2 * Frame, (int)Math.Ceiling(size.Height * scale) + 2 * Frame);
        }

        private List<double> Normalize(List<double> values, double minimum, double range)
        {
            List<double> NormalizedValues = new List<double>();
            for (int i = 0; i < values.Count(); i++)
            {
                if (range == 0)
                {
                    NormalizedValues.Add(0);
                }
                else
                {
                    NormalizedValues.Add(((values[i] - minimum) / range));
                }
            }
            return NormalizedValues;
        }
        private List<PointF> CreateImageControllPoints(double dpi, List<double> Xs, List<double> Ys)
        {
            List<PointF> Points = new List<PointF>();
            double scale = OutDpi / dpi;

            for (int i = 0; i < Xs.Count(); i++)
            {
                Points.Add(new PointF(
                    (float)((Xs[i]  - Xs.Min() + Frame) * scale),
                    (float)((Ys.Max() - Ys[i] + Frame) * scale)
                ));
            }
            return Points;
        }

        private Point CatmullRomSpline(List<PointF> Points, int i, float _t)
        {
            double[] t = new double[4];
            double[] x = new double[4];
            double[] y = new double[4];

            if (i == 0)
            {
                x[0] = Points[0].X + (Points[0].X - Points[1].X) / 4;
                y[0] = Points[0].Y + (Points[0].Y - Points[1].Y) / 4;
                for (int k = 1; k < 4; k++)
                {
                    x[k] = Points[i + k - 1].X;
                    y[k] = Points[i + k - 1].Y;
                }
            }
            else if (i == Points.Count() - 2)
            {
                x[3] = Points[Points.Count() - 1].X + (Points[Points.Count() - 1].X - Points[Points.Count() - 2].X) / 4;
                y[3] = Points[Points.Count() - 1].Y + (Points[Points.Count() - 1].Y - Points[Points.Count() - 2].Y) / 4;
                for (int k = 1; k < 3; k++)
                {
                    x[k] = Points[i + k - 1].X;
                    y[k] = Points[i + k - 1].Y;
                }
            }
            else
            {
                for (int k = 0; k < 4; k++)
                {
                    x[k] = Points[i + k - 1].X;
                    y[k] = Points[i + k - 1].Y;
                }
            }

            t[0] = 0;
            for (int k = 0; k < 3; k++)
                t[k + 1] = Math.Pow(Math.Pow(x[k + 1] - x[k], 2) + Math.Pow(y[k + 1] - y[k], 2), CatmullRomAlpha / 2) + t[k];

            double t_j = t[1] + (t[2] - t[1]) * _t;
            double a1_x = (t[1] - t_j) / (t[1] - t[0]) * x[0] + (t_j - t[0]) / (t[1] - t[0]) * x[1];
            double a1_y = (t[1] - t_j) / (t[1] - t[0]) * y[0] + (t_j - t[0]) / (t[1] - t[0]) * y[1];
            double a2_x = (t[2] - t_j) / (t[2] - t[1]) * x[1] + (t_j - t[1]) / (t[2] - t[1]) * x[2];
            double a2_y = (t[2] - t_j) / (t[2] - t[1]) * y[1] + (t_j - t[1]) / (t[2] - t[1]) * y[2];
            double a3_x = (t[3] - t_j) / (t[3] - t[2]) * x[2] + (t_j - t[2]) / (t[3] - t[2]) * x[3];
            double a3_y = (t[3] - t_j) / (t[3] - t[2]) * y[2] + (t_j - t[2]) / (t[3] - t[2]) * y[3];

            double b1_x = (t[2] - t_j) / (t[2] - t[0]) * a1_x + (t_j - t[0]) / (t[2] - t[0]) * a2_x;
            double b1_y = (t[2] - t_j) / (t[2] - t[0]) * a1_y + (t_j - t[0]) / (t[2] - t[0]) * a2_y;
            double b2_x = (t[3] - t_j) / (t[3] - t[1]) * a2_x + (t_j - t[1]) / (t[3] - t[1]) * a3_x;
            double b2_y = (t[3] - t_j) / (t[3] - t[1]) * a2_y + (t_j - t[1]) / (t[3] - t[1]) * a3_y;

            double p_x = (t[2] - t_j) / (t[2] - t[1]) * b1_x + (t_j - t[1]) / (t[2] - t[1]) * b2_x;
            double p_y = (t[2] - t_j) / (t[2] - t[1]) * b1_y + (t_j - t[1]) / (t[2] - t[1]) * b2_y;

            return new Point((int)p_x, (int)p_y);
        }

        private List<List<Point>> GetCirclePoints(Point center, double radius)
        {
            List<List<Point>> circle_points = new List<List<Point>>();
            for (int y = center.Y - (int)radius; y <= center.Y + (int)radius; y++)
            {
                int start = -(int)-(center.X - Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(center.Y - y, 2)));
                int end = (int)(center.X + Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(center.Y - y, 2)));
                List<Point> pair = new List<Point>();
                pair.Add(new Point(start, y));
                pair.Add(new Point(end, y));
                circle_points.Add(pair);
            }
            return circle_points;
        }

        private void DrawCircle(Image<Rgba32> img, Point center, double radius, Rgba32 color)
        {
            List<List<Point>> circle_points = GetCirclePoints(center, radius);
            for (int i = 0; i < circle_points.Count; i++)
                if (circle_points[i][0].Y >= 0 & circle_points[i][0].Y < img.Height)
                    for (int j = circle_points[i][0].X; j <= circle_points[i][1].X; j++)
                        if (j >= 0 & j < img.Width)
                        {
                            img[j, circle_points[i][0].Y] = color;
                        }
        }

        private List<Vector2> CalculateVelocities(List<PointF> Points, List<double> Ts)
        {
            Vector2[] Velocities = new Vector2[Points.Count()];
            Velocities[0] = new Vector2(0.0f, 0.0f);
            Velocities[Points.Count() - 1] = new Vector2(0.0f, 0.0f);
            for (int i = 1; i < Points.Count() - 1; i++)
            {
                double timedelta0 = Ts[i] == Ts[i - 1] ? 10 : Ts[i] - Ts[i - 1];
                double timedelta1 = Ts[i + 1] == Ts[i] ? 10 : Ts[i + 1] - Ts[i];
                Vector2 speed1 = new Vector2(Points[i].X - Points[i - 1].X, Points[i].Y - Points[i - 1].Y) / (float) timedelta0;
                Vector2 speed2 = new Vector2(Points[i + 1].X - Points[i].X, Points[i + 1].Y - Points[i].Y) / (float) timedelta1;
                Velocities[i] = (speed1 + speed2) / 2;
            }
            List<Vector2> tmp = new List<Vector2>();
            tmp.AddRange(Velocities);
            return tmp;
        }

        private List<Point> CalculateImagePoints(List<PointF> ControllPoints, List<bool> PenDowns)
        {
            List<Point> Points = new List<Point>();
            for (int i = 0; i < ControllPoints.Count() - 1; i++)
            {
                if (!PenDowns[i + 1])
                {
                    continue;
                }

                for (float t = 0.0f; t <= 1.0f; t += CatmullRomStep)
                {
                    Points.Add(CatmullRomSpline(ControllPoints, i, t));
                }
            }
            return Points;
        }

        private List<double> LinearFill(List<double> values, List<bool> PenDowns)
        {
            int PointsPerSpline = (int) (1 / CatmullRomStep);
            List<double> propagatedValues = new List<double>();
            for(int i = 0; i < values.Count() - 1; i++)
            {
                if(!PenDowns[i + 1])
                {
                    continue;
                }

                propagatedValues.Add(values[i]);
                for(int j = 0; j < PointsPerSpline; j++)
                {
                    propagatedValues.Add((values[i] * (PointsPerSpline - j) + values[i + 1] * j) / PointsPerSpline);
                }
            }
            return propagatedValues;
        }

        private void DrawSignature(Image<Rgba32> img, List<Point> Points, List<double> NPs, List<bool> PenDowns, List<double> NVs)
        {
            float BaseRadius = 5;
            img.Mutate(ctx =>
            {
                for (int i = 0; i < Points.Count() - 1; i++)
                {

                    double intensity = BaseIntensity;
                    intensity *=  NPs[i];
                    intensity = Math.Min(intensity, 1);
                    double radius = BaseRadius;
                    radius = NVs[i] >= 1 ? radius * 0.1 : radius * (1 - NVs[i]);
                    Rgba32 color = new Rgba32((byte)(255 - intensity * 155.0), (byte)(255 - intensity * 165.0), (byte)(255 - intensity * 75.0), (byte)255);
                    DrawCircle(img, Points[i], radius, color);
                }
            });
        }

    }
}
