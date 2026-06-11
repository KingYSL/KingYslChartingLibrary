using ChartingLibrary1._4.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models
{
    public class DefaultTick : Tick
    {
        public DefaultTick(double value, Point point) : base(value, point)
        {
            Label = value.ToString();
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
                Brushes.White);

            // Determine the style of the tick based on its orientation
            if (Orientation == TickOrientation.Major)
            {
                // Major tick logic: larger line and bold label
                canvas.DrawLine(new Pen(Brushes.Black, 2), Position, new Point(Position.X, Position.Y));
                formattedText.SetFontWeight(FontWeights.Bold);
            }
            else
            {
                // Minor tick logic: smaller line and regular label
                canvas.DrawLine(new Pen(Brushes.Black, 1), Position, new Point(Position.X, Position.Y));
            }

            // Position the label text appropriately
            canvas.DrawText(formattedText, new Point(Position.X, Position.Y));
        }
    }

}

