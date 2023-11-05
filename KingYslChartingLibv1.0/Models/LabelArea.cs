using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace KingYslChartingLibv1._0.Models
{
    public class LabelArea
    {

        public Orientation Orientation { get; set; } // Orientation (Horizontal/Vertical)
        public double Thickness { get; set; } // Thickness of the label area

        public Rect RectArea { get; set; }

        Brush Brush { get; set; }

        public LabelArea(Orientation orientation, double thickness)
        {

            Orientation = orientation;
            Thickness = thickness;


        }

        // Method to generate and return a Rect for the label area
        public Rect GenerateRect(double canvasWidth, double canvasHeight, Point axisStart, Point axisEnd)
        {
            double edgeBuffer = 5;  // Minimum distance from the edge of the canvas

            if (Orientation == Orientation.Horizontal)
            {
                // Check and adjust so that the rectangle stays within the canvas
                double availableSpaceBottom = canvasHeight - axisEnd.Y;
                double shift = availableSpaceBottom < edgeBuffer ? edgeBuffer - availableSpaceBottom : 0;

                return RectArea = new Rect(axisStart.X, axisEnd.Y - shift, canvasWidth, Thickness);
            }
            else
            {
                // Check and adjust so that the rectangle stays within the canvas
                double availableSpaceLeft = axisEnd.X;
                double shift = availableSpaceLeft < edgeBuffer ? edgeBuffer - availableSpaceLeft : 0;

                return RectArea = new Rect(axisEnd.X - shift, 0, Thickness, canvasHeight);

            }
        }
    }
}

