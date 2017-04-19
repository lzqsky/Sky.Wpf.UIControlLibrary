using Sky.Wpf.UIControlLibrary.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sky.Wpf.UIControlLibrary.control.tip
{
    public class TipMode : DependencyObject
    {
        public string TipContent { get; set; }
        public double TipSecond { get; set; }
        public RelayCommand RemoveCommand { get; set; }
    }
}
