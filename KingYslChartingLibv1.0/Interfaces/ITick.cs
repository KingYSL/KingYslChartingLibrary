using KingYslChartingLibv1._0.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KingYslChartingLibv1._0.Interfaces
{
    public interface ITick
    {
        public double Value { get; set; }
        public TickOrientation Orientation { get; set; }
        public System.Windows.Point Position { get; set; }
        public String Label { get; set; }

        public void Draw(DrawingContext canvas);

    }
}
