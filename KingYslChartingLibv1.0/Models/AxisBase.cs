using KingYslChartingLibv1._0.Enums;
using KingYslChartingLibv1._0.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KingYslChartingLibv1._0.Models
{
    public abstract class AxisBase
    {
        public double Min { get; set; }
        public LabelArea LabelArea { get; set; }
        public double Max { get; set; }
        public AxisType Type { get; set; }
        public double Interval { get; set; }
        public string Label { get; set; }
        public Point AxisStart { get; set; }
        public Point AxisEnd { get; set; }
        public AxisPosition Position { get; set; }

        public List<ITick> Ticks { get; set; }

        public AxisBase(double min, double max, AxisType type, double interval, string label)
        {
            Min = min;
            Max = max;
            Type = type;
            Interval = interval;
            Label = label;
            LabelArea = new LabelArea(System.Windows.Controls.Orientation.Vertical, 30);
            Ticks = new List<ITick>();
        }

        public abstract void Render(DrawingContext dc, double canvasWidth, double canvasHeight);

        public abstract (Point start, Point end) CalculateAxisPoints(double canvasWidth, double canvasHeight);

        public List<double> CalculateTickPositions()
        {
            List<double> tickPositions = new List<double>();
            double tickValue = Min;

            while (tickValue <= Max)
            {
                tickPositions.Add(tickValue);
                tickValue += Interval;
            }

            return tickPositions;
        }
        public double ConvertToPixel(double value, double canvasSize)
        {
            return (value - Min) / (Max - Min) * canvasSize;
        }

        public double ConvertToValue(double pixel, double canvasSize)
        {
            return Min + (Max - Min) * pixel / canvasSize;
        }

        public string fromDoubleToDateTimeString(double pixelposition)
        {
            var dat = DateTime.FromOADate(pixelposition);
            string datestring = dat.ToString();
            return datestring;
        }
    }

}