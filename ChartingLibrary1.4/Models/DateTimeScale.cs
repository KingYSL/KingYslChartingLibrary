using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models
{
    public class DateTimeScale : ScaleBase
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public new const double MAXAMOUNTOFTICKS = 10;
        public DateTime Now => DateTime.Now;

        List<DateTimeTick> Ticks = new List<DateTimeTick>();
        public DateTimeScale()
        {
            MinDate = Now.Date.AddDays(-MAXAMOUNTOFTICKS);
            MaxDate = Now;
            MinValue = MinDate.ToOADate();
            MaxValue = MaxDate.ToOADate();
             Reverse = false;
        }

        public DateTimeScale(DateTime minDate, DateTime maxDate, double width)
        {
            MinDate = minDate;
            MaxDate = maxDate;
            MinValue = minDate.ToOADate();
            MaxValue = maxDate.ToOADate();
            width = CanvasDimension;
        }



        public override void DrawTicks(DrawingContext dc)
        {
            Ticks.Clear();
            //space between tick positions

            CalculateTickPositions();

            foreach (var tick in Ticks)
            {
                tick.Draw(dc);
            }
        }
        public override void CalculateTickPositions()
        {
            double canvasrange = Reverse ? CanvasDimension : 0;
            double tickspacing = CanvasDimension / MAXAMOUNTOFTICKS;
            double scalerange = MaxDate.ToOADate() - MinDate.ToOADate();
            MajorInterval = scalerange / MAXAMOUNTOFTICKS;
            double currentvalue = MinDate.ToOADate();
            switch (Reverse)
            {
                case true:
                    for (int i = 0; i <= MAXAMOUNTOFTICKS; i++)
                    {
                        Point tickpos = new Point();
                        tickpos.X = canvasrange;
                        tickpos.Y = 0;

                        Ticks.Add(new DateTimeTick(currentvalue, tickpos));
                        canvasrange -= tickspacing;
                        currentvalue += MajorInterval;

                    }
                    break;
                case false:
                    for (int i = 0; i <= MAXAMOUNTOFTICKS; i++)
                    {
                        Point TickPoint = new Point();
                        TickPoint.X = canvasrange;
                        TickPoint.Y = 0;
                        Ticks.Add(new DateTimeTick(currentvalue, TickPoint));
                        canvasrange += tickspacing;
                        currentvalue += MajorInterval;
                    }
                    break;
            }

        }

        public override void Translate(double pixelDelta)
        {
            double dataDelta = pixelDelta * (MaxValue - MinValue) / CanvasDimension;
            MinValue -= dataDelta;
            MaxValue -= dataDelta;
            MinDate = DateTime.FromOADate(MinValue);
            MaxDate = DateTime.FromOADate(MaxValue);
        }

        public override void Zoom(double factor, Point centerPixel)
        {
            // --------------------------------------------------------------------
            // 1. Validate input
            // --------------------------------------------------------------------
            if (factor <= 0)
                throw new ArgumentOutOfRangeException(nameof(factor),
                    "Zoom factor must be a positive number.");

            // If the scale has no visual width we cannot perform a zoom.
            if (CanvasDimension == 0) return;

            // --------------------------------------------------------------------
            // 2. Determine the data value that lies under the cursor
            // --------------------------------------------------------------------
            double valueAtCenter = ValueFromPixel(centerPixel.X);

            // --------------------------------------------------------------------
            // 3. Compute the new numeric range after applying the zoom factor.
            //    *factor > 1 → zoom‑in (range becomes smaller)
            //    *factor < 1 → zoom‑out (range becomes larger)
            // --------------------------------------------------------------------
            double oldRange = MaxValue - MinValue;
            double newRange = oldRange / factor;   // <-- the key line

            // --------------------------------------------------------------------
            // 4. Find the relative position of the cursor along the axis.
            //    For a reversed scale we flip the direction so that pixel 0
            //    still corresponds to the left‑hand side of the visual area.
            // --------------------------------------------------------------------
            double relPos = Reverse
                ? 1.0 - (centerPixel.X / CanvasDimension)   // right → left
                : (centerPixel.X / CanvasDimension);          // left → right

            // --------------------------------------------------------------------
            // 5. Update Min/Max so that the value under the cursor stays at the same pixel.
            // --------------------------------------------------------------------
            MinValue = valueAtCenter - newRange * relPos;
            MaxValue = MinValue + newRange;

            // --------------------------------------------------------------------
            // 6. Keep the date boundaries in sync with the numeric range
            //    (so that tick generation continues to work correctly).
            // --------------------------------------------------------------------
            MinDate = DateTime.FromOADate(MinValue);
            MaxDate = DateTime.FromOADate(MaxValue);

            // Optional: trigger a redraw of ticks if your framework requires it.
        }
        public override double ValueFromPixel(double pixelCoord)
            => MinValue + pixelCoord * (MaxValue - MinValue) / CanvasDimension;
    }
}

