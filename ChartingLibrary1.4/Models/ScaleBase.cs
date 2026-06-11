using ChartingLibrary1._4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models
{
    public abstract class ScaleBase : IScale
    {
        public virtual double MinValue { get; set; }
        public virtual double MaxValue { get; set; }
        public virtual double MajorInterval { get; set; }
        public virtual double MinorTicks { get; set; }
        public virtual string LabelFormat { get; set; }
        public virtual bool Reverse { get; set; }

        public const double MAXAMOUNTOFTICKS = 18;

        public double ZoomFactor { get; set; }
        public Point PanOffset { get; set; }
        public virtual double CanvasDimension { get; set; }
        public abstract void CalculateTickPositions();
        public abstract void DrawTicks(DrawingContext dc);
        public abstract void Translate(double pixelDelta);           // pan

        public abstract void Zoom(double factor, Point centerPixel); // zoom around a point

        public abstract double ValueFromPixel(double pixelCoord);
    }
}

