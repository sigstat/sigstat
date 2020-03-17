
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SigStat.Common;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;

namespace SigStat.UI
{
    public class SignatureVisualizer: Grid
    {
        private ScaleTransform scaleTransform = new ScaleTransform(1, 1, 0, 0);
        private TranslateTransform translateTransform = new TranslateTransform(0, 0);

        public Signature Signature
        {
            get { return (Signature)GetValue(SignatureProperty); }
            set { SetValue(SignatureProperty, value); }
        }
        // Using a DependencyProperty as the backing store for keyValuePairs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SignatureProperty =
            DependencyProperty.Register("Signature", typeof(Signature), typeof(SignatureVisualizer), new PropertyMetadata(null,SignatureChanged));



        public DisplayMode DisplayMode
        {
            get { return (DisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayModeProperty =
            DependencyProperty.Register("DisplayMode", typeof(DisplayMode), typeof(SignatureVisualizer), new PropertyMetadata(DisplayMode.Zoom));




        public bool ShowAxes
        {
            get { return (bool)GetValue(ShowAxesProperty); }
            set { SetValue(ShowAxesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowAxis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAxesProperty =
            DependencyProperty.Register("ShowAxes", typeof(bool), typeof(SignatureVisualizer), new PropertyMetadata(true, ShowAxesChanged));

        private static void ShowAxesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var visualizer = d as SignatureVisualizer;
            if (d == null)
                return;
            visualizer.Reload();
        }

        private Canvas canvas;
        private Viewbox viewbox;


        public SignatureVisualizer()
        {
            ClipToBounds = true;
            viewbox = new Viewbox();
            canvas = new Canvas();
            Children.Add(viewbox);
            viewbox.Child = canvas;
            viewbox.RenderTransform = new TransformGroup() { Children = { scaleTransform, translateTransform } };
            this.IsHitTestVisible = true;
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
           
        }


        private static void SignatureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var visualizer = d as SignatureVisualizer;
            if (d == null)
                return;
            visualizer.Reload();
            
        }

        private void Reload()
        {
            canvas.Children.Clear();
            //scaleTransform.ScaleX = 1;
            //scaleTransform.ScaleY = 1;
            //translateTransform.X = 0;
            //translateTransform.Y = 0;
            var sig = Signature;
            if (sig == null)
                return;
            FillPenUpDurations tr = new FillPenUpDurations()
            {
                InputFeatures = { Features.X, Features.Y },
                OutputFeatures = { Features.X, Features.Y },
                PressureInputFeature = Features.Pressure,
                PressureOutputFeature = Features.Pressure,
                TimeInputFeature = Features.T,
                TimeOutputFeature = Features.T,
                InterpolationType = typeof(CubicInterpolation)
            };

            tr.Transform(sig);
            var strokes = sig.GetStrokes();
            var xt = sig.GetFeature(Features.X);
            var yt = sig.GetFeature(Features.Y);



            switch (DisplayMode)
            {
                case DisplayMode.Original:
                    break;
                case DisplayMode.Zoom:
                    var minX = xt.Min();
                    xt = xt.Select(x => x - minX).ToList();
                    var minY = yt.Min();
                    yt = yt.Select(y => y - minY).ToList();
                    var maxY = yt.Max();
                    yt = yt.Select(y => maxY - y).ToList();
                    break;
                default:
                    throw new NotSupportedException($"Unsupported DisplayMode: {DisplayMode}");
            }
            canvas.Width = xt.Max();
            canvas.Height = yt.Max();

            foreach (var stroke in strokes)
            {
                Polyline polyline = new Polyline() { StrokeThickness = 3, StrokeLineJoin= PenLineJoin.Round };
                polyline.Stroke = stroke.StrokeType == StrokeType.Down ? Brushes.Blue : Brushes.DarkSalmon;
                for (int i = stroke.StartIndex; i <= stroke.EndIndex; i++)
                {
                    polyline.Points.Add(new Point(xt[i], yt[i]));
                }
                canvas.Children.Add(polyline);
                //AddLogicalChild(polyline);
                //AddVisualChild(polyline);
            }
            if (ShowAxes)
                RenderAxes(xt.Min(), xt.Max(), yt.Min(), yt.Max());




        }

        private void RenderAxes(double minX, double maxX, double minY, double maxY)
        {
            maxX = Math.Max(Math.Abs(minX), Math.Abs(maxX));
            maxY = Math.Max(Math.Abs(minY), Math.Abs(maxY));
            Line xAxis = new Line() { Stroke = Brushes.Black, StrokeThickness = 3, StrokeEndLineCap = PenLineCap.Triangle };
            xAxis.Y1 = -maxY;
            xAxis.Y2 = maxY;
            Line yAxis = new Line() { Stroke = Brushes.Black, StrokeThickness = 3, StrokeEndLineCap = PenLineCap.Triangle };
            yAxis.X1 = -maxX;
            yAxis.X2 = maxX;
            canvas.Children.Add(xAxis);
            canvas.Children.Add(yAxis);
        }

        private Point mouseOrigin;
        double tx;
        double ty;
        private bool moving = false;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            tx = translateTransform.X;
            ty = translateTransform.Y;
            moving = true;
            mouseOrigin = e.GetPosition(this.Parent as IInputElement);
            CaptureMouse();
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (moving)
            {
                var mousePos = e.GetPosition(this.Parent as IInputElement);
                translateTransform.X = tx + mousePos.X - mouseOrigin.X;
                translateTransform.Y = ty + mousePos.Y - mouseOrigin.Y;
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            moving = false;
            ReleaseMouseCapture();
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                scaleTransform.ScaleX *= 1.1;
                scaleTransform.ScaleY *= 1.1;
            }
            else
            {
                scaleTransform.ScaleX /= 1.1;
                scaleTransform.ScaleY /= 1.1;
            }

            e.Handled = true;
            base.OnPreviewMouseWheel(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
           

            base.OnMouseWheel(e);
        }
    }
}
