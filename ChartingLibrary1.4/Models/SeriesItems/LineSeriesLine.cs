using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models.SeriesItems
{
    public class LineSeriesLine:RenderableBase
    {
        public Pen Pencil { get; set; }
        public LineSeriesLine(Point start, Point end)
        {
            Start = start;
            End = end;
            Pencil= new Pen();
            Pencil.Brush = Color;
        }

        public override void OnRender(DrawingContext context)
        {
            context.DrawLine(Pencil, Start, End);
        }

    }
}
