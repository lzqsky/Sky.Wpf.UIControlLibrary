using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        demoBehavior behavior = new demoBehavior();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Rectangle rect = new Rectangle() {
            //    RadiusX = 5,
            //    RadiusY =5,
            //    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF818181")),
            //    StrokeDashArray=new DoubleCollection(new double[] { 5, 2, 1, 2 }),
            //    StrokeThickness = 2
            //};
            //AttachedProperty.SetDropAction(this.grid, (f,isDrop, d) =>
            //{
            //    if (isDrop)
            //    {
            //        ListBoxItem item = AttachedProperty.FindVisualParent<ListBoxItem>(d);
            //        if (item != null && f != item)
            //        {
            //            int index = this.lst.Items.IndexOf(item);
            //            if (this.lst.Items.Contains(f))
            //            {
            //                this.lst.Items.Remove(f);
            //            }
            //            if (index == -1)
            //                return;

            //            rect.Width = f.ActualWidth - 10;
            //            rect.Height = f.ActualHeight;
            //            //插入一个虚拟的框
            //            this.lst.Items.Remove(rect);
            //            this.lst.Items.Insert(index, rect);
            //        }
            //    }
            //    else
            //    {
            //        //是否还包含 要拖动的控件
            //        if (this.lst.Items.Contains(f))
            //        { return; }
            //        int index = this.lst.Items.IndexOf(rect);
            //        if (index > 0)
            //        {
            //            this.lst.Items.Remove(rect);
            //            this.lst.Items.Insert(index, f);
            //        }
            //    }
            //});
            //foreach (ListBoxItem item in this.lst.Items)
            //{
            //    AttachedProperty.SetIsDrop(item, true);
            //    AttachedProperty.SetDropParent(item, this.grid);
            //} 
            
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //this.item1.Items.Add(new TreeViewItem() { Header = "asdasd" });
        }

       

        private void ListBox_Drop(object sender, DragEventArgs e)
        {

        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb t = sender as Thumb;
            FrameworkElement item = t.Parent as FrameworkElement;
            if (item != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (t.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange,
                            item.ActualHeight - item.MinHeight);
                        item.Height -= deltaVertical;
                        break;
                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange,
                            item.ActualHeight - item.MinHeight);
                        Canvas.SetTop(item, Canvas.GetTop(item) + deltaVertical);
                        item.Height -= deltaVertical;
                        break;
                    default:
                        break;
                }

                switch (t.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange,
                            item.ActualWidth - item.MinWidth);
                        Canvas.SetLeft(item, Canvas.GetLeft(item) + deltaHorizontal);
                        item.Width -= deltaHorizontal;
                        break;
                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange,
                            item.ActualWidth - item.MinWidth);
                        item.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }
    }


    public class AttachedProperty
    {
        #region 拖动

        public static bool GetIsDrop(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDropProperty);
        }

        public static void SetIsDrop(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDropProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsDrop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropProperty =
            DependencyProperty.RegisterAttached("IsDrop", typeof(bool), typeof(AttachedProperty), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                UIElement uielement = s as UIElement;
                if (uielement == null)
                    return;
                if ((bool)e.NewValue)
                {
                    uielement.AllowDrop = true;
                    uielement.PreviewMouseDown -= uielement_PreviewMouseDown;
                    uielement.PreviewMouseDown += uielement_PreviewMouseDown;

                    uielement.PreviewMouseMove -= user_PreviewMouseMove;
                    uielement.PreviewMouseMove += user_PreviewMouseMove;
                }
                else
                {
                    uielement.AllowDrop = false;
                    uielement.PreviewMouseDown -= uielement_PreviewMouseDown;
                    uielement.PreviewMouseMove -= user_PreviewMouseMove;
                }
            })));


        public static UIElement GetDropParent(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(DropParentProperty);
        }

        public static void SetDropParent(DependencyObject obj, UIElement value)
        {
            obj.SetValue(DropParentProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropParent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropParentProperty =
            DependencyProperty.RegisterAttached("DropParent", typeof(UIElement), typeof(AttachedProperty), new PropertyMetadata(null));



        public static Action<FrameworkElement,bool, DependencyObject> GetDropAction(DependencyObject obj)
        {
            return (Action<FrameworkElement, bool, DependencyObject>)obj.GetValue(DropActionProperty);
        }

        public static void SetDropAction(DependencyObject obj, Action<FrameworkElement, bool, DependencyObject> value)
        {
            obj.SetValue(DropActionProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropActionProperty =
            DependencyProperty.RegisterAttached("DropAction", typeof(Action<FrameworkElement, bool, DependencyObject>), typeof(AttachedProperty), new PropertyMetadata(null));




        public static Func<FrameworkElement, UIElement> GetDragAction(DependencyObject obj)
        {
            return (Func<FrameworkElement, UIElement>)obj.GetValue(DragActionProperty);
        }

        public static void SetDragAction(DependencyObject obj, Func<FrameworkElement, UIElement> value)
        {
            obj.SetValue(DragActionProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragActionProperty =
            DependencyProperty.RegisterAttached("DragAction", typeof(Func<FrameworkElement, UIElement>), typeof(AttachedProperty), new PropertyMetadata(null));


        private static FrameworkElement _dropCtrl;
        private static UIElement _uiElement = null;
        static void uielement_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _uiElement = sender as UIElement;
        }

        static void user_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (_uiElement == null || _uiElement != element)
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _dropCtrl = sender as FrameworkElement;
                StartDrag(_dropCtrl);
                _dropCtrl = null;
            }
        }

        private static void StartDrag(FrameworkElement user)
        {
            UIElement parent = GetDropParent(user);
            if (parent == null)
            {
                return;
            }
            parent.AllowDrop = true;
            //拖动显示的模板
            Func<FrameworkElement, UIElement> dragAction = GetDragAction(user);
            UIElement dragUIelement = (UIElement)user;
            if (dragAction != null)
            {
                dragUIelement = dragAction(user);
            }

            DragAdorner adorner = new DragAdorner(parent, dragUIelement, 0.5);
            DragEventHandler draghandler = new DragEventHandler((s, e) =>
            {
                if (adorner != null)
                {
                    adorner.LeftOffset = e.GetPosition(parent).X;
                    adorner.TopOffset = e.GetPosition(parent).Y;

                    Action<FrameworkElement,bool, DependencyObject> action1 = GetDropAction(parent);
                    if (action1 != null)
                    {
                        action1(user, true,(DependencyObject)e.OriginalSource);
                    }
                }
            });
            parent.PreviewDragOver += draghandler;
          
            parent.GiveFeedback += m_itemsControl_GiveFeedback;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(parent as Visual);

            layer.Add(adorner);

            DataObject data = new DataObject(typeof(FrameworkElement), user);
            System.Windows.DragDrop.DoDragDrop(user, data, DragDropEffects.Move);
            if (adorner == null)
            {
                return;
            }

            layer.Remove(adorner);

            parent.PreviewDragOver -= draghandler;
            parent.GiveFeedback -= m_itemsControl_GiveFeedback;

            Action<FrameworkElement, bool, DependencyObject> action = GetDropAction(parent);
            if (action != null)
            {
                action(user, false, null);
            }

        } 
        static void m_itemsControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            Mouse.SetCursor(Cursors.Arrow);
            e.UseDefaultCursors = false;
            e.Handled = true;
        }

        public  static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
        #endregion
    }

    public class DragAdorner : Adorner
    {
        #region 变量
        protected UIElement _child;
        protected VisualBrush _brush;
        protected UIElement _owner;
        protected double XCenter;
        protected double YCenter;
        private double _leftOffset;
        private double _topOffset;
        public double Scale = 1.0;
        #endregion
        #region 构造函数
        public DragAdorner(UIElement owner) : base(owner) { }
        public DragAdorner(UIElement owner, UIElement adornElement, double opacity)
            : base(owner)
        {
            this._owner = owner;
            VisualBrush _brush = new VisualBrush(adornElement);
            _brush.Opacity = opacity;
            Rectangle r = new Rectangle();
            r.RadiusX = 3;
            r.RadiusY = 3;
            r.Width = adornElement.RenderSize.Width;
            r.Height = adornElement.RenderSize.Height;
            XCenter = 0;// adornElement.RenderSize.Width / 2;
            YCenter = 0;// adornElement.RenderSize.Height / 2;
            r.Fill = _brush;
            this._child = r;
        }
        #endregion
        #region 属性
        public double LeftOffset
        {
            get { return this._leftOffset; }
            set
            {
                this._leftOffset = value - XCenter;
                this.UpdatePosition();
            }
        }
        public double TopOffset
        {
            get { return this._topOffset; }
            set
            {
                this._topOffset = value - YCenter;
                this.UpdatePosition();
            }
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }
        #endregion
        #region 方法
        private void UpdatePosition()
        {
            AdornerLayer adorner = (AdornerLayer)this.Parent;
            if (adorner != null)
            {
                adorner.Update(this.AdornedElement);
            }
        }
        protected override Visual GetVisualChild(int index)
        {
            return _child;
        }
        protected override Size MeasureOverride(Size finalSize)
        {
            this._child.Measure(finalSize);
            return this._child.DesiredSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            this._child.Arrange(new Rect(_child.DesiredSize));
            return finalSize;
        }
        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(this._leftOffset, this._topOffset));
            return result;
        }
        #endregion
    }

    public class DragInCanvasBehavior : Behavior<UIElement>
    {
        private Canvas canvas;

        protected override void OnAttached()
        {
            base.OnAttached();

            // Hook up event handlers.              
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // Detach event handlers.  
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        // Keep track of when the element is being dragged.  
        private bool isDragging = false;

        // When the element is clicked, record the exact position  
        // where the click is made.  
        private Point mouseOffset;

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Find the canvas.  
            if (canvas == null) canvas = VisualTreeHelper.GetParent(this.AssociatedObject) as Canvas;

            // Dragging mode begins.  
            isDragging = true;

            // Get the position of the click relative to the element  
            // (so the top-left corner of the element is (0,0).  
            mouseOffset = e.GetPosition(AssociatedObject);

            // Capture the mouse. This way you'll keep receiveing  
            // the MouseMove event even if the user jerks the mouse  
            // off the element.  
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Get the position of the element relative to the Canvas.  
                Point point = e.GetPosition(canvas);

                // Move the element.  
                AssociatedObject.SetValue(Canvas.TopProperty, point.Y - mouseOffset.Y);
                AssociatedObject.SetValue(Canvas.LeftProperty, point.X - mouseOffset.X);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                AssociatedObject.ReleaseMouseCapture();
                isDragging = false;
            }
        }
    }

    public class demoBehavior : Behavior<UIElement>
    {
        //添加该 动画触发
        protected override void OnAttached()
        {
            base.OnAttached();

            //< DoubleAnimationUsingKeyFrames Storyboard.TargetProperty = "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName = "treeView" >
            //      < EasingDoubleKeyFrame KeyTime = "0" Value = "-132" />
            //         < EasingDoubleKeyFrame KeyTime = "0:0:0.4" Value = "28.5" />
            //        </ DoubleAnimationUsingKeyFrames >

            DoubleAnimation RotateAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(200)));
            TranslateTransform transForm = new TranslateTransform();
            this.AssociatedObject.RenderTransform = transForm;
            RotateAnimation.From = -132;
            transForm.BeginAnimation(TranslateTransform.YProperty, RotateAnimation); 
        }

        //删除该动画 触发
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
