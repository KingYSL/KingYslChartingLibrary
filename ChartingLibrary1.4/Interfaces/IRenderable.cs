using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartingLibrary1._4.Interfaces
{
    public interface IRenderable
    {
     
        public Point Start { get; set; }
     
        public Point End { get; set; }

        public Brush Color { get; set; }

        public void OnRender(DrawingContext context);
    }
}
