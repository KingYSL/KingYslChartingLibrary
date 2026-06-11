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
    public abstract class RenderableBase : IRenderable
    {
        public Point Start { get; set ; }
        public Point End { get; set; }
        public Brush Color { get; set; }

        public abstract void OnRender(DrawingContext context);
        
    }
}
