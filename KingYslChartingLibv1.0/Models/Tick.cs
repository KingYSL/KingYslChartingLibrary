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
    public abstract class Tick : ITick
    {
        public double Value { get; set; }
        public TickOrientation Orientation { get; set; }
        public System.Windows.Point Position { get; set; }
        public string Label { get; set; }

        public Tick(double rate, Point point)
        {
            Value = rate;

            Position = point;

        }
        public abstract void Draw(DrawingContext canvas);



    }


}
