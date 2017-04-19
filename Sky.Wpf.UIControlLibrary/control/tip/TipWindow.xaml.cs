using Sky.Wpf.UIControlLibrary.common;
using Sky.Wpf.UIControlLibrary.control.animating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Sky.Wpf.UIControlLibrary.control.tip
{
    public class TipWindow : Window
    {
        DispatcherTimer _timer = new DispatcherTimer();
        public TipWindow()
        {
            this.Closed += Window2_Closed;
            TipList = new BindingList<TipMode>();
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();

            this.AllowsTransparency = true;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStyle = WindowStyle.None;
            this.Background = Brushes.Transparent;
            this.ShowInTaskbar = false;
            this.Topmost = true;
            this.WindowState = WindowState.Maximized;

            this.DataContext = this;
        }
        static TipWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TipWindow), new FrameworkPropertyMetadata(typeof(TipWindow)));
        }

        public BindingList<TipMode> TipList { get; set; }

        private void Window2_Closed(object sender, EventArgs e)
        {
            if (_timer != null && _timer.IsEnabled)
            {
                _timer.Stop();
                _timer = null;
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (TipList == null)
                return;

            List<TipMode> copyList = new List<TipMode>();
            foreach (TipMode mode in TipList)
            {
                if (mode.TipSecond < 0)
                    continue;

                if (mode.TipSecond > 0)
                    mode.TipSecond--;

                if (mode.TipSecond == 0)
                    copyList.Add(mode);

            }

            foreach (TipMode mode in copyList)
            {
                RemoveItem(mode);
            }

            if (this.TipList.Count == 0)
                this.Close();
        }


        int i = 1;
        public void AddItem(string content, int tipsecond = 15)
        {
            TipMode mode = new TipMode
            {
                TipContent = content,
                RemoveCommand = new RelayCommand(RemoveItem),
                TipSecond = tipsecond
            };
            TipList.Add(mode);
        }


        private void RemoveItem(object item)
        {
            TipMode mode = item as TipMode;
            if (mode != null)
            {
                ItemAnimating.RemoveItem(TipList, mode);
            }
        }
    }
}
