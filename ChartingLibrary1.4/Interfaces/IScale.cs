using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Interfaces
{
    public interface IScale
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double MajorInterval { get; set; }
        public double MinorTicks { get; set; }
        public string LabelFormat { get; set; }
        public bool Reverse { get; set; }

        public const double MAXAMOUNTOFTICKS = 18;
        public double CanvasDimension { get; set; }

        public double ZoomFactor { get; set; }
        public Point PanOffset { get; set; }
        public void CalculateTickPositions();
        public void DrawTicks(DrawingContext dc);
        void Translate(double pixelDelta);           // pan

        void Zoom(double factor, Point centerPixel); // zoom around a point

        double ValueFromPixel(double pixelCoord);
    }
}
