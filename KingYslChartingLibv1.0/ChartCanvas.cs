using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using KingYslChartingLibv1._0.Models;
using KingYslChartingLibv1._0.Enums;

namespace KingYslChartingLibv1._0
{
    public class ChartCanvas : Canvas
    {
        private Point currentMousePosition;
        private bool isMouseWithinCanvas;

        public AxisBase XAxis { get; set; }
        public AxisBase YAxis { get; set; }
        public double width;
        public double height;
        public ChartCanvas()
        {
            width = this.Width;
            height = this.Height;

            // Initialize default axes


            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.MouseMove += OnMouseMove;

        }


        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            isMouseWithinCanvas = true;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            isMouseWithinCanvas = false;
        }


        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            XAxis = new DateTimeAxis(this.MinWidth, this.ActualWidth);
            YAxis = new YAxis(this.MinHeight, this.ActualHeight, 10, "Y-Axis") { Position = AxisPosition.Right };
            // Render Axes
            var xAxisPoints = XAxis.CalculateAxisPoints(this.ActualWidth, this.ActualHeight);
            var yAxisPoints = YAxis.CalculateAxisPoints(this.ActualWidth, this.ActualHeight);

            YAxis.Render(dc, this.ActualWidth, this.ActualHeight);
            XAxis.Render(dc, this.ActualWidth, this.ActualHeight);
            //X AXIS Label area


            dc.DrawText(new FormattedText((XAxis.fromDoubleToDateTimeString(currentMousePosition.X), YAxis.ConvertToValue(currentMousePosition.Y, this.ActualHeight)).ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(currentMousePosition.X, currentMousePosition.Y + 10));



            // Draw grid lines for Y axis


        }

        private double MapValueToCanvasCoordinate(double value, double minValue, double maxValue, double canvasMin, double canvasMax)
        {
            // Map a value from the axis range to the canvas coordinate range
            return (value - minValue) / (maxValue - minValue) * (canvasMax - canvasMin) + canvasMin;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            currentMousePosition = e.GetPosition(this);
            this.InvalidateVisual(); // Redraw canvas
        }
    }
}