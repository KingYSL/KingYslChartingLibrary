using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChartingLibrary1._4.Models
{
    internal interface ISeries
    {
        List<DataPoint> DataSource { get;set; } 
        IEnumerable<Point> Points { get; set; }

        public Plot Canvas {  get; set; }

    }
}
