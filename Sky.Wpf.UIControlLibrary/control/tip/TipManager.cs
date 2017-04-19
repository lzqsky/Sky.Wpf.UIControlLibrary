using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sky.Wpf.UIControlLibrary.control.tip
{
    public class TipManager
    {
        static TipWindow win = null;
        public static void ShowTip(string tipMsg, int second = 15)
        {
            if (win == null)
            {
                win = new TipWindow();
                win.Closed += Win_Closed;
                win.Show();
            }

            if (win.Visibility == Visibility.Collapsed)
                win.Visibility = Visibility.Visible;

            win.AddItem(tipMsg, second);
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            win = null;
        }
    }
}
