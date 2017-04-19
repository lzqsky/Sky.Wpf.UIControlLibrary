using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Sky.Wpf.UIControlLibrary.control.tip
{
    public class TipPanel : System.Windows.Controls.Panel
    {
        public TipPanel()
        {
            _availableSize = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
            ItemWidth = 200;
        }
        public double ItemWidth { get; set; } 
        private Size _availableSize = new Size();

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement ui in InternalChildren)
            {
                ui.Measure(new Size(ItemWidth, availableSize.Height));

            }
            return new Size(ItemWidth, _availableSize.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var next = new Point(1, 0);
            foreach (UIElement element in InternalChildren)
            {
                element.Arrange(new Rect(new Point(), element.DesiredSize));

                TranslateTransform transform = null;
                TransformGroup group = element.RenderTransform as TransformGroup;
                if (group != null)
                {
                    var from = group.Children.FirstOrDefault(o => o is TranslateTransform);
                    if (from != null)
                        transform = from as TranslateTransform;
                }
                if (transform == null)
                {
                    transform = element.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                    }
                    element.RenderTransform = transform;
                }
                next.Y += element.RenderSize.Height;
                double y = _availableSize.Height - next.Y;
                if (y < 0)
                {
                    next.X += 1;
                    next.Y = element.RenderSize.Height;
                }
                double transX = _availableSize.Width - element.RenderSize.Width * next.X - (next.X * 20);
                if (transform.X != transX)
                {
                    transform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(transX + element.RenderSize.Width, transX, new Duration(TimeSpan.FromMilliseconds(300))));
                }

                double transY = _availableSize.Height - next.Y;
                if (transform.Y != transY)
                {
                    transform.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(transY, new Duration(TimeSpan.FromSeconds(0))));
                }
            }
            return finalSize;
        }
    }
}
