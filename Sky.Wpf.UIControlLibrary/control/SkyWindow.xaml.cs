using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sky.Wpf.UIControlLibrary.control
{
    [TemplatePart(Name = PART_TitleBackground, Type = typeof(Rectangle))]
    [TemplatePart(Name = PART_Icon, Type = typeof(UIElement))]
    [TemplatePart(Name = PART_MinimizeButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_RestoreButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_ResizeGrid, Type = typeof(Grid))]
    public partial class SkyWindow : Window
    {
        private const string PART_TitleBackground = "PART_TitleBackground";
        private const string PART_Icon = "PART_Icon";
        private const string PART_MinimizeButton = "PART_MinimizeButton";
        private const string PART_RestoreButton = "PART_RestoreButton";
        private const string PART_CloseButton = "PART_CloseButton";
        private const string PART_ResizeGrid = "PART_ResizeGrid";

        private Button _minimizeButton;
        private Button _restoreButton;
        private Button _closeButton;
        private HwndSource _hwndSource;
        public SkyWindow()
        {
            PreviewMouseMove += OnPreviewMouseMove;
        }
        static SkyWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SkyWindow), new FrameworkPropertyMetadata(typeof(SkyWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Rectangle rectangle = GetTemplateChild(PART_TitleBackground) as Rectangle;
            if (rectangle != null)
                rectangle.PreviewMouseMove += Rectangle_PreviewMouseMove;
            _minimizeButton = GetTemplateChild(PART_MinimizeButton) as Button;
            if (_minimizeButton != null)
            {
                _minimizeButton.Click += _minimizeButton_Click;
            }
            _restoreButton = GetTemplateChild(PART_RestoreButton) as Button;
            if (_restoreButton != null)
                _restoreButton.Click += _restoreButton_Click;
            _closeButton = GetTemplateChild(PART_CloseButton) as Button;
            if (_closeButton != null)
                _closeButton.Click += _closeButton_Click;

            if (IsResizeGrid)
            {
                Grid resizeGrid = GetTemplateChild(PART_ResizeGrid) as Grid;
                if (resizeGrid != null)
                {
                    foreach (UIElement element in resizeGrid.Children)
                    {
                        Rectangle resizeRectangle = element as Rectangle;
                        if (resizeRectangle != null)
                        {
                            resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                            resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                        }
                    }
                }
            }
        }

        private void Rectangle_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                switch (rectangle.Name)
                {
                    case "Top":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Top);
                        break;
                    case "Bottom":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Bottom);
                        break;
                    case "Left":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Left);
                        break;
                    case "Right":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Right);
                        break;
                    case "TopLeft":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.TopLeft);
                        break;
                    case "TopRight":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.TopRight);
                        break;
                    case "BottomLeft":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.BottomLeft);
                        break;
                    case "BottomRight":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.BottomRight);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ResizeRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                switch (rectangle.Name)
                {
                    case "Top":
                        Cursor = Cursors.SizeNS;
                        break;
                    case "Bottom":
                        Cursor = Cursors.SizeNS;
                        break;
                    case "Left":
                        Cursor = Cursors.SizeWE;
                        break;
                    case "Right":
                        Cursor = Cursors.SizeWE;
                        break;
                    case "TopLeft":
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case "TopRight":
                        Cursor = Cursors.SizeNESW;
                        break;
                    case "BottomLeft":
                        Cursor = Cursors.SizeNESW;
                        break;
                    case "BottomRight":
                        Cursor = Cursors.SizeNWSE;
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += MainWindow_SourceInitialized;

            base.OnInitialized(e);
        }

        protected void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }


        private void ResizeWindow(ResizeDirection direction)
        {
            models.win32.SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }


        private void _closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void _restoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        private void _minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #region 属性
        public int TitlebarHeight
        {
            get { return (int)GetValue(TitlebarHeightProperty); }
            set { SetValue(TitlebarHeightProperty, value); }
        }

        //标题栏高度
        public static readonly DependencyProperty TitlebarHeightProperty =
            DependencyProperty.Register("TitlebarHeight", typeof(int), typeof(SkyWindow), new PropertyMetadata(30));




        public Brush TitlebarBrush
        {
            get { return (Brush)GetValue(TitlebarBrushProperty); }
            set { SetValue(TitlebarBrushProperty, value); }
        }

        //标题栏颜色
        public static readonly DependencyProperty TitlebarBrushProperty =
            DependencyProperty.Register("TitlebarBrush", typeof(Brush), typeof(SkyWindow), new PropertyMetadata(Brushes.Gray));




        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        //Icon 模板
        public static readonly DependencyProperty IconTemplateProperty =
            DependencyProperty.Register("IconTemplate", typeof(DataTemplate), typeof(SkyWindow), new PropertyMetadata(null));




        public bool IsResizeGrid
        {
            get { return (bool)GetValue(IsResizeGridProperty); }
            set { SetValue(IsResizeGridProperty, value); }
        }

        //是否可以控制窗体的上左下右
        public static readonly DependencyProperty IsResizeGridProperty =
            DependencyProperty.Register("IsResizeGrid", typeof(bool), typeof(SkyWindow), new PropertyMetadata(true));





        public bool IsShowCloseButton
        {
            get { return (bool)GetValue(IsShowCloseButtonProperty); }
            set { SetValue(IsShowCloseButtonProperty, value); }
        }
        public static readonly DependencyProperty IsShowCloseButtonProperty =
            DependencyProperty.Register("IsShowCloseButton", typeof(bool), typeof(SkyWindow), new PropertyMetadata(true));



        public bool IsShowMaxButton
        {
            get { return (bool)GetValue(IsShowMaxButtonProperty); }
            set { SetValue(IsShowMaxButtonProperty, value); }
        }
        public static readonly DependencyProperty IsShowMaxButtonProperty =
            DependencyProperty.Register("IsShowMaxButton", typeof(bool), typeof(SkyWindow), new PropertyMetadata(true));



        public bool IsShowMinButton
        {
            get { return (bool)GetValue(IsShowMinButtonProperty); }
            set { SetValue(IsShowMinButtonProperty, value); }
        }
        public static readonly DependencyProperty IsShowMinButtonProperty =
            DependencyProperty.Register("IsShowMinButton", typeof(bool), typeof(SkyWindow), new PropertyMetadata(true));

        #endregion
    }


    public enum ResizeDirection
    {
        Left = 1,
        Right = 2,
        Top = 3,
        TopLeft = 4,
        TopRight = 5,
        Bottom = 6,
        BottomLeft = 7,
        BottomRight = 8,
    }
}
