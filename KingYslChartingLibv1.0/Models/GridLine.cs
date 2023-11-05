using KingYslChartingLibv1._0.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KingYslChartingLibv1._0.Models
{
    public class GridLine
    {
        public GridLineOrientation Orientation { get; set; }

        public Point Position { get; set; }
        public double Thickness { get; set; }
        public Brush Brush { get; set; }

        public GridLine(GridLineOrientation gridLineOrientation, Point point, double thickness, Brush brush)
        {

            Orientation = gridLineOrientation;
            Position = point;
            Thickness = thickness;
            Brush = brush;

        }


        public void Draw(DrawingContext drawingContext)
        {

        }

    }
}
