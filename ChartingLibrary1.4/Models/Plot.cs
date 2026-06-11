using ChartingLibrary1._4.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChartingLibrary1._4.Models
{
    public class Plot : Canvas
    {

        private bool isMouseWithinCanvas;

        private Point currentMousePosition;

        public Point CurrentMousePosition
        {
            get
            {
                return currentMousePosition;
            }
            set
            {
                currentMousePosition = value;

            }
        }

        private Point previousmouseposition;
        public Point PreviousMousePosition
        {
            get
            {
                return previousmouseposition;
            }
            set
            {
                previousmouseposition = value;

            }
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
        public Plot()
        {
            SizeChanged += Plot_SizeChanged;
            MouseMove += Plot_MouseMove;
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;

        }
        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            isMouseWithinCanvas = true;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            isMouseWithinCanvas = false;
            InvalidateVisual();
        }
        private void Plot_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isMouseWithinCanvas) return;
            PreviousMousePosition = CurrentMousePosition;
            CurrentMousePosition = e.GetPosition(this);

        }

        private void Plot_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {


        }




    }
}
