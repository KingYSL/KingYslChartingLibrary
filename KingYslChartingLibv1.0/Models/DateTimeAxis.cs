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
    public class DateTimeAxis : AxisBase
    {
        public DateTime MinDateTime { get; set; }
        public DateTime MaxDateTime { get; set; }

        public const double MAXAMOUNTOFTICKS = 9;
        public DateTimeAxis(DateTime min, DateTime max) : base(min.ToOADate(), max.ToOADate(), AxisType.XAxis, 10, "Time Axis")
        {
            MinDateTime = min; MaxDateTime = max;
            LabelArea = new LabelArea(System.Windows.Controls.Orientation.Horizontal, 30);
            CalculateTickPositions();


        }

        public DateTimeAxis(double min, double max) : base(min, max, AxisType.XAxis, 10, "Time Axis")
        {

            LabelArea = new LabelArea(System.Windows.Controls.Orientation.Horizontal, 30);
            CalculateTickPositions();
        }
        public override void Render(DrawingContext dc, double canvasWidth, double canvasHeight)
        {
            AxisStart = new Point(0, canvasHeight);
            AxisEnd = new Point(canvasWidth, canvasHeight - 30);

            Rect labelAreaRect = LabelArea.GenerateRect(canvasWidth - 36, canvasHeight, AxisStart, AxisEnd);
            LabelArea.RectArea = labelAreaRect;

            dc.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Yellow, 4), labelAreaRect);

            // Draw axis line


            dc.DrawLine(new Pen(Brushes.Pink, 4), new Point(0, labelAreaRect.Top), new Point(canvasWidth - 36, labelAreaRect.Top));

            var xtickpos = CalculateTickPositions();

            // Here you can add logic to draw tick marks, labels, and grid lines
            foreach (var xTickPosition in xtickpos)
            {
                //Draw GridLines
                double xCoord = ConvertToPixel(xTickPosition, labelAreaRect.Right);
                dc.DrawLine(new Pen(Brushes.LightGray, 1), new Point(xCoord, 0), new Point(xCoord, labelAreaRect.Top));


                // Draw tick labels
                Ticks.Add(new DateTimeTick(xTickPosition, new Point(xCoord, labelAreaRect.Top)));
                NormalizeTickLabels();
                foreach (var tick in Ticks)
                {
                    tick.Draw(dc);
                }
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
        public void NormalizeTickLabels()
        {
            DateTimeTick previousTick = null;

            foreach (var genericTick in Ticks)
            {
                // Attempt to cast the ITick to a DateTimeTick
                var currentTick = genericTick as DateTimeTick;
                if (currentTick == null)
                    continue; // Skip if it's not a DateTimeTick

                // Skip the first tick as there is no previous tick to compare
                if (previousTick == null)
                {
                    previousTick = currentTick;
                    continue;
                }

                // Check if the previous tick is in a different year
                if (previousTick.DateTime.Year != currentTick.DateTime.Year)
                {
                    // Update the label of the current tick to include the year and the month
                    currentTick.Label = currentTick.DateTime.ToString("yyyy MMM");
                }

                // Update previousTick to be the current one for the next iteration
                previousTick = currentTick;
            }
        }

        public string fromDoubleToDateTimeString(double pixelposition)
        {
            var dat = DateTime.FromOADate(pixelposition);
            string datestring = dat.ToShortDateString();
            return datestring;
        }



    }
}
