using Sky.Wpf.UIControlLibrary.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sky.Wpf.UIControlLibrary.control
{
    public class IconButton : Button
    { 

        public IconButton() { }
        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //if (AttachProperty.GetMouseOverBackground(this) == Brushes.Transparent)
            //{
            //    AttachProperty.SetMouseOverBackground(this, this.Background);
            //    AttachProperty.SetPressedBackground(this, this.Background);
            //}
        }

    }
}
