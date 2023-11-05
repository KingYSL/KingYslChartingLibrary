using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using KingYslChartingLibv1._0.Enums;

namespace KingYslChartingLibv1._0.Models
{
    public class YAxis : AxisBase
    {
        public const double MAXAMOUNTOFTICKS = 18;
        public YAxis(double min, double max, double interval, string label) : base(min, max, AxisType.YAxis, interval, label)
        {



        }
        public override void Render(DrawingContext dc, double canvasWidth, double canvasHeight)
        {
            AxisStart = new Point(canvasWidth, 0);
            AxisEnd = new Point(canvasWidth, canvasHeight);
            Rect labelAreaRect = LabelArea.GenerateRect(canvasWidth*0.9, canvasHeight * 0.95, AxisStart, AxisEnd);
            LabelArea.RectArea = labelAreaRect;

            dc.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Yellow, 4), labelAreaRect);
            // Draw axis line



            dc.DrawLine(new Pen(Brushes.Blue, 4), new Point(canvasWidth, 0), new Point(canvasWidth, canvasHeight));

            var ytickpos = CalculateTickPositions();

            foreach (var yTickPosition in ytickpos)
            {
                double yCoord = canvasHeight - ConvertToPixel(yTickPosition, canvasHeight);

                // Adjust tick position based on Y axis position
                if (Position == AxisPosition.Left)
                {
                    // Left Y axis
                    dc.DrawLine(new Pen(Brushes.LightGray, 1), new Point(0, yCoord), new Point(canvasWidth, yCoord));
                    dc.DrawLine(new Pen(Brushes.Black, 1), new Point(-5, yCoord), new Point(5, yCoord));
                }

                else if (Position == AxisPosition.Right)
                {
                    // Right Y axis
                    dc.DrawLine(new Pen(Brushes.LightGray, 1), new Point(labelAreaRect.Left, yCoord), new Point(0, yCoord));
                    dc.DrawLine(new Pen(Brushes.Black, 1), new Point(canvasWidth, yCoord), new Point(canvasWidth, yCoord));
                }

                // Draw tick labels for Y axis
                Ticks.Add(new NormalTick(yTickPosition, new Point(labelAreaRect.Left, yCoord)));
                foreach (var tick in Ticks)
                {
                    tick.Draw(dc);
                }
                //dc.DrawText(new FormattedText(yTickPosition.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(Position == AxisPosition.Left ? -30 : canvasWidth - 30, yCoord - 5));
                // Here you can add logic to draw tick marks, labels, and grid lines
            }
        }

        public override (Point start, Point end) CalculateAxisPoints(double canvasWidth, double canvasHeight)
        {
            switch (Position)
            {
                case AxisPosition.Left:
                    return (new Point(0, 0), new Point(0, canvasHeight));
                case AxisPosition.Right:
                    return (new Point(canvasWidth, 0), new Point(canvasWidth, canvasHeight));
                case AxisPosition.Top:
                    return (new Point(0, 0), new Point(canvasWidth, 0));
                case AxisPosition.Bottom:
                    return (new Point(0, canvasHeight), new Point(canvasWidth, canvasHeight));
                default:
                    return (new Point(0, 0), new Point(0, 0));
            }
        }
        public new List<double> CalculateTickPositions()
        {
            Interval = Max / MAXAMOUNTOFTICKS;
            List<double> ticks = new List<double>();
            double tickValue = Min;

            while (tickValue <= Max)
            {
                ticks.Add(tickValue);
                tickValue += Interval;
            }
            return ticks;


        }
    }
}
