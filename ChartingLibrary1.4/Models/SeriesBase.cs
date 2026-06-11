using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChartingLibrary1._4.Models
{
    public class SeriesBase : ISeries
    {
        public List<DataPoint> DataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<Point> Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Plot Canvas { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
