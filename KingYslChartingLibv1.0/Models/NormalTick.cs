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
    public class NormalTick : Tick
    {
        public NormalTick(double value, Point point) : base(value, point)
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
                Brushes.Black);

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

