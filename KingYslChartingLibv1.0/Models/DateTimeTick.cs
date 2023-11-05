using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using KingYslChartingLibv1._0.Enums;

namespace KingYslChartingLibv1._0.Models
{
    public class DateTimeTick : Tick
    {
        public DateTime DateTime { get; set; }
        string Label { get; set; }
        public DateTimeTick(double datetimevalue, Point point) : base(datetimevalue, point)
        {
            DateTime = DateTime.FromOADate(datetimevalue);

            if (DateTime.DayOfYear == 1)
            {
                // First day of the year
                Orientation = TickOrientation.Major;
                Label = DateTime.ToString("yyyy"); // Just the year
            }
            else if (DateTime.Day == 1)
            {
                // First day of the month
                Orientation = TickOrientation.Major;
                Label = DateTime.ToString("MMMM"); // Full month name
            }
            else
            {
                // Any other day
                Orientation = TickOrientation.Minor;
                Label = GetOrdinalDay(DateTime); // Day with ordinal
            }
        }

        private string GetOrdinalDay(DateTime date)
        {
            int day = date.Day;
            string suffix;

            switch (day % 10)
            {
                case 1 when day != 11:
                    suffix = "st";
                    break;
                case 2 when day != 12:
                    suffix = "nd";
                    break;
                case 3 when day != 13:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            return $"{day}{suffix}";
        }
        public override void Draw(DrawingContext canvas)
        {
            Typeface typeface = new Typeface("Arial");
            FormattedText formattedText = new FormattedText(
                Label,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                typeface,
                10,
                Brushes.Black);

            // Determine the style of the tick based on its orientation
            if (Orientation == TickOrientation.Major)
            {
                // Major tick logic: larger line and bold label
                canvas.DrawLine(new Pen(Brushes.Black, 2), Position, new Point(Position.X, Position.Y + 10));
                formattedText.SetFontWeight(FontWeights.Bold);
            }
            else
            {
                // Minor tick logic: smaller line and regular label
                canvas.DrawLine(new Pen(Brushes.Black, 1), Position, new Point(Position.X, Position.Y + 5));
            }

            // Position the label text appropriately
            canvas.DrawText(formattedText, new Point(Position.X, Position.Y + 12));
        }
    }

}

