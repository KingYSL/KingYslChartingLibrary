using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models
{
    public class NumericScale : ScaleBase
    {
        public List<DefaultTick> Ticks { get; set; }

        public NumericScale()
        {
            // Default constructor
            MinValue = 0;
            MaxValue = 1000;
            MajorInterval = 100;
            MinorTicks = 5;
            Reverse = true;
            Ticks = new List<DefaultTick>();
        }
        public NumericScale(double height)
        {
            // Default constructor
            MinValue = 0;
            MaxValue = 1000;
            MajorInterval = 100;
            MinorTicks = 5;
            Reverse = false;
            Ticks = new List<DefaultTick>();
            CanvasDimension = height;
        }

        public override void DrawTicks(DrawingContext dc)
        {

            Ticks.Clear();
            CalculateTickPositions();


            foreach (var tick in Ticks)
            {
                tick.Draw(dc);
            }
        }

        public override void CalculateTickPositions()
        {
            double tickspacing = CanvasDimension / MAXAMOUNTOFTICKS;
            double Canvasrange = Reverse ? CanvasDimension : 0;
            double ScaleRange = MaxValue - MinValue;
            double scaleInterval = ScaleRange / MAXAMOUNTOFTICKS;
            double currentvalue = Reverse ? MinValue : 0;
            switch (Reverse)
            {
                case true:
                    {
                        for (int i = 0; i <= MAXAMOUNTOFTICKS; i++)
                        {



                            Point tickPoint = new Point();
                            tickPoint.X = 0;
                            tickPoint.Y = Canvasrange;

                     var x=        Math.Round(currentvalue += scaleInterval);
                            Ticks.Add(new DefaultTick(x, tickPoint));
                            Canvasrange -= tickspacing;
                         

                        }
                    }

                    break;

                case false:
                    {
                        for (int i = 0; i <= MAXAMOUNTOFTICKS; i++)
                        {
                            Point tickPoint = new Point();

                            tickPoint.X = 0;
                            tickPoint.Y = Canvasrange;


                            Ticks.Add(new DefaultTick(currentvalue, tickPoint));
                            Canvasrange += tickspacing;
                            Math.Round(currentvalue += scaleInterval);
                        }

                    }
                    break;
            }
        }

        public override void Translate(double pixelDelta)
        {
            double dataDelta = pixelDelta * (MaxValue - MinValue) / CanvasDimension;
            MinValue -= dataDelta;
            MaxValue -= dataDelta;
        }

        public override void Zoom(double factor, Point centerPixel)
        {
            // Keep the value under the cursor fixed
            double valueAtCenter = ValueFromPixel(centerPixel.X);
            double newRange = (MaxValue - MinValue) / factor;

            MinValue = valueAtCenter - newRange * (centerPixel.X / CanvasDimension);
            MaxValue = MinValue + newRange;
        }

        public override double ValueFromPixel(double pixelCoord)
            => MinValue + pixelCoord * (MaxValue - MinValue) / CanvasDimension;
    }
}

