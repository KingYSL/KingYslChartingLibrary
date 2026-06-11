using ChartingLibrary1._4.Enums;
using ChartingLibrary1._4.Interfaces;
using ChartingLibrary1._4.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models
{
    public class AxisCanvas : Canvas
    {

        public AxisPosition Position
        {
            get;
            set;
        }

        public const double MAXAMOUNTOFTICKS = 18;
        [TypeConverter(typeof(ScaleTypeConverter))]
        public IScale Scale { get; set; }
        public static readonly DependencyProperty ScaleProperty =
           DependencyProperty.Register(
               nameof(Scale),
               typeof(IScale),
               typeof(AxisCanvas),
               new PropertyMetadata(default(IScale)));

        public bool FullyLoaded { get; set; } = false;

        private bool isMouseWithinCanvas;

        private Point currentMousePosition;

        public Point CurrentMousePosition
        {
            get
            {
                return currentMousePosition;
            }
            set
            {
                currentMousePosition = value;

            }
        }

        private Point previousmouseposition;
        public Point PreviousMousePosition
        {
            get
            {
                return previousmouseposition;
            }
            set
            {
                previousmouseposition = value;

            }
        }

        public double Width => this.ActualWidth;
        public double Height => this.ActualHeight;
        public AxisCanvas(IScale scale, AxisPosition position)
        {
            Scale = scale;
            
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.MouseMove += OnMouseMove;
           
            
            FullyLoaded = false;
            Position = position;
            this.SizeChanged += Window_SizeChanged;
            this.Background = ColorHelper.FromHex("#FF131722");
            InvalidateVisual();
        }

        public AxisCanvas()
        {
            
            this.Background = ColorHelper.FromHex("#FF131722");
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.MouseMove += OnMouseMove;
            this.SizeChanged += Window_SizeChanged;
            this.MouseWheel += OnMouseWheel; 
            InvalidateVisual();
           
           
        }
        public void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Scale == null) return;

            double factor = e.Delta > 0 ? 1.1 : 0.9;
            Point pos = e.GetPosition(this);

            // Pass the right coordinate for vertical vs horizontal axes
            var zoomCenter = Position switch
            {
                AxisPosition.Left or AxisPosition.Right => new Point(0, pos.Y),
                AxisPosition.Top or AxisPosition.Bottom => new Point(pos.X, 0),
                _ => pos
            };

            Scale.Zoom(factor, zoomCenter);
            InvalidateVisual();
        }

        /*
                public AxisCanvas()
                {

                    this.Background= new SolidColorBrush(Colors.Red);
                    this.MouseEnter += OnMouseEnter;
                    this.MouseLeave += OnMouseLeave;
                    this.MouseMove += OnMouseMove;
                    this.SizeChanged += Window_SizeChanged;
                    switch (Position)
                    {

                        case AxisPosition.Right:


                            Scale = new NumericScale();
                            break;
                        case AxisPosition.Bottom:
                            Scale = new DateTimeScale();
                            break;
                    }
                    FullyLoaded = true;
                }
        */
        public IScale GetScale()
        {
            return Scale;
        }
        private static IScale CreateDefaultScale(AxisPosition pos) =>
       pos switch
       {
           AxisPosition.Bottom or AxisPosition.Top => new DateTimeScale(),
           AxisPosition.Left or AxisPosition.Right => new NumericScale(),
           _ => throw new ArgumentOutOfRangeException(nameof(pos))
       };

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Scale.DrawTicks(drawingContext);

            switch (Position)
            {

                case AxisPosition.Right:
                    drawingContext.DrawText(new FormattedText(CurrentMousePosition.Y.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip), new Point(currentMousePosition.X, currentMousePosition.Y));

                    break;
                case AxisPosition.Bottom:
                    drawingContext.DrawText(new FormattedText(CurrentMousePosition.X.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip), new Point(currentMousePosition.X, currentMousePosition.Y + 10));
                    break;

                case AxisPosition.Left:
                    drawingContext.DrawText(new FormattedText(CurrentMousePosition.Y.ToString(), CultureInfo.CurrentCulture, FlowDirection.RightToLeft, new Typeface("Arial"), 10, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip), new Point(currentMousePosition.X, currentMousePosition.Y));
                    break;
                case AxisPosition.Top:
                    drawingContext.DrawText(new FormattedText(CurrentMousePosition.X.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip), new Point(currentMousePosition.X, currentMousePosition.Y + 10));
                    break;

            }

        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                PreviousMousePosition = e.GetPosition(this);
            }


        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Scale != null)
                switch (Position)
                {
                    case AxisPosition.Right:

                        Scale.CanvasDimension = e.NewSize.Height;
                        InvalidateVisual();

                        break;
                    case AxisPosition.Bottom:
                        Scale.CanvasDimension = e.NewSize.Width;
                        InvalidateVisual();

                        break;
                    case AxisPosition.Left:
                        Scale.CanvasDimension = e.NewSize.Height;
                        InvalidateVisual();

                        break;
                    case AxisPosition.Top:
                        Scale.CanvasDimension = e.NewSize.Width;
                        InvalidateVisual();

                        break;

                }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            isMouseWithinCanvas = true;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            isMouseWithinCanvas = false;
            InvalidateVisual();
        }
        public async void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseWithinCanvas) return;

            // Update mouse position
            currentMousePosition = e.GetPosition(this);

            // Check if left mouse button is pressed for panning
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var delta = Position switch
                {
                    AxisPosition.Left or AxisPosition.Right => CurrentMousePosition.Y - PreviousMousePosition.Y,
                    AxisPosition.Top or AxisPosition.Bottom => CurrentMousePosition.X - PreviousMousePosition.X,
                    _ => 0
                };

                Scale.Translate(delta);

            }



            // Update rendering
            InvalidateVisual();
            previousmouseposition = e.GetPosition(this);
        }

    }
}
