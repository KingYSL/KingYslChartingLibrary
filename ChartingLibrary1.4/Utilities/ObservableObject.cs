using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChartingLibrary1._4.Utilities
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public static event PropertyChangedEventHandler SPropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void PC([CallerMemberName] string callerName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
        }
        static protected void StaticPC([CallerMemberName] string callerName = "")
        {
            SPropertyChanged?.Invoke(SPropertyChanged, new PropertyChangedEventArgs(callerName));
        }
        static protected void StaticRaisePropertyChangedEvent(string propertyName)
        {
            StaticPropertyChanged?.Invoke(StaticPropertyChanged, new PropertyChangedEventArgs(propertyName));
        }
    }
}
