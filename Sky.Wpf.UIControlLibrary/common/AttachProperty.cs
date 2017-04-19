using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Sky.Wpf.UIControlLibrary.common
{
    public static class AttachProperty
    {
        #region 图标

        #region 图标字体内容
        public static string GetIconString(DependencyObject obj)
        {
            return (string)obj.GetValue(IconStringProperty);
        }

        public static void SetIconString(DependencyObject obj, string value)
        {
            obj.SetValue(IconStringProperty, value);
        }

        //字体内容
        public static readonly DependencyProperty IconStringProperty =
            DependencyProperty.RegisterAttached("IconString", typeof(string), typeof(AttachProperty), new PropertyMetadata(""));

        //private static void IconStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var uc = d as FrameworkElement;
        //    if (uc == null) return;
             
        //    string value = (string)e.NewValue;
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        uc.Margin = GetIconMargin(uc);
        //    } 
        //}
        #endregion

        #region IconSizeProperty 字体图标大小
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.RegisterAttached("IconSize", typeof(double), typeof(AttachProperty), new FrameworkPropertyMetadata(12D));

        public static double GetIconSize(DependencyObject d)
        {
            return (double)d.GetValue(IconSizeProperty);
        }
        public static void SetIconSize(DependencyObject obj, double value)
        {
            obj.SetValue(IconSizeProperty, value);
        }
        #endregion

        #region IconMarginProperty 字体图标边距
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.RegisterAttached("IconMargin", typeof(Thickness), typeof(AttachProperty), new FrameworkPropertyMetadata(new Thickness(0)));

        public static Thickness GetIconMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(IconMarginProperty);
        }

        public static void SetIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(IconMarginProperty, value);
        }
        #endregion

        #region IsIconAnimationProperty 启用旋转动画
        /// <summary>
        /// 启用旋转动画
        /// </summary>
        public static readonly DependencyProperty IsIconAnimationProperty = DependencyProperty.RegisterAttached("IsIconAnimation", typeof(bool), typeof(AttachProperty), new FrameworkPropertyMetadata(false, IsIconAnimationChanged));

        public static bool GetIsIconAnimation(DependencyObject d)
        {
            return (bool)d.GetValue(IsIconAnimationProperty);
        }

        public static void SetIsIconAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(IsIconAnimationProperty, value);
        }
        /// <summary>
        /// 旋转动画刻度
        /// </summary>
        private static DoubleAnimation RotateAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(200)));
        /// <summary>
        /// 绑定动画事件
        /// </summary>
        private static void IsIconAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as FrameworkElement;
            if (uc == null) return;
            if (uc.RenderTransformOrigin == new Point(0, 0))
            {
                uc.RenderTransformOrigin = new Point(0.5, 0.5);
                RotateTransform trans = new RotateTransform(0);
                uc.RenderTransform = trans;
            }
            var value = (bool)e.NewValue;
            if (value)
            {
                RotateAnimation.To = 180;
                uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
            }
            else
            {
                RotateAnimation.To = 0;
                uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
            }
        }
        #endregion

        #endregion

        #region CornerRadiusProperty Border圆角
        /// <summary>
        /// Border圆角
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(AttachProperty), new FrameworkPropertyMetadata(null));

        public static CornerRadius GetCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }
        #endregion

        #region WatermarkProperty 水印
        /// <summary>
        /// 水印
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached(
            "Placeholder", typeof(string), typeof(AttachProperty), new FrameworkPropertyMetadata(""));

        public static string GetPlaceholder(DependencyObject d)
        {
            return (string)d.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }
        #endregion

        #region FocusBorderBrush 焦点边框色，输入控件

        public static readonly DependencyProperty FocusBorderBrushProperty = DependencyProperty.RegisterAttached(
            "FocusBorderBrush", typeof(Brush), typeof(AttachProperty), new FrameworkPropertyMetadata(null));
        public static void SetFocusBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }
        public static Brush GetFocusBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBorderBrushProperty);
        }

        #endregion       
        #region FocusBackground 获得焦点背景色
        public static readonly DependencyProperty FocusBackgroundProperty = DependencyProperty.RegisterAttached(
            "FocusBackground", typeof(Brush), typeof(AttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetFocusBackground(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBackgroundProperty, value);
        }

        public static Brush GetFocusBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBackgroundProperty);
        }

        #endregion
        #region MouseOverBorderBrush 鼠标进入边框色，输入控件
        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(AttachProperty),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        #endregion
        #region MouseOverBackgroundProperty 鼠标悬浮背景色

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverBackground", typeof(Brush), typeof(AttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBackgroundProperty, value);
        }

        public static Brush GetMouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverBackgroundProperty);
        }

        #endregion
        #region MouseOverForegroundProperty 鼠标悬浮前景色

        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverForeground", typeof(Brush), typeof(AttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverForegroundProperty, value);
        }

        public static Brush GetMouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverForegroundProperty);
        }

        #endregion
        #region PressedBorderBrushProperty 按下时边框的颜色
        public static Brush GetPressedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedBorderBrushProperty);
        }

        public static void SetPressedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedBorderBrushProperty, value);
        }
         
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.RegisterAttached("PressedBorderBrush", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(null));
        #endregion
        #region PressedBackgroundProperty 按下时的背景颜色
        public static Brush GetPressedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedBackgroundProperty);
        }

        public static void SetPressedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.RegisterAttached("PressedBackground", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(null));

        #endregion
        #region PressedForegroundProperty 按下时字体的颜色
        public static Brush GetPressedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedForegroundProperty);
        }
        public static void SetPressedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.RegisterAttached("PressedForeground", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(null));
        #endregion


        #region AttachContentProperty 附加组件模板
        /// <summary>
        /// 附加组件模板
        /// </summary>
        public static readonly DependencyProperty AttachContentProperty = DependencyProperty.RegisterAttached(
            "AttachContent", typeof(ControlTemplate), typeof(AttachProperty), new FrameworkPropertyMetadata(null));

        public static ControlTemplate GetAttachContent(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(AttachContentProperty);
        }

        public static void SetAttachContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(AttachContentProperty, value);
        }
        #endregion


        #region TextNumType 文本框只能输入数字类型


        public static NumTextType GetNumText(DependencyObject obj)
        {
            return (NumTextType)obj.GetValue(NumTextProperty);
        }

        public static void SetNumText(DependencyObject obj, NumTextType value)
        {
            obj.SetValue(NumTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for TextNumType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumTextProperty =
            DependencyProperty.RegisterAttached("NumText", typeof(NumTextType), typeof(AttachProperty), new PropertyMetadata(NumTextType.Default, NumTextChanged));


        private static void NumTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tb = d as TextBox;
            if (tb == null) return;

            tb.PreviewTextInput -= tbb_PreviewTextInput;
            tb.PreviewKeyDown -= tbb_PreviewKeyDown;
            DataObject.RemovePastingHandler(tb, OnClipboardPaste);
            NumTextType result = (NumTextType)e.NewValue;
            if (result != NumTextType.Default)
            {
                InputMethod.SetIsInputMethodEnabled(tb, false);
                tb.PreviewTextInput += tbb_PreviewTextInput;
                tb.PreviewKeyDown += tbb_PreviewKeyDown;
                DataObject.AddPastingHandler(tb, OnClipboardPaste);
            }
            else
            {
                InputMethod.SetIsInputMethodEnabled(tb, true);
            }

        }
        private static void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text,text,GetNumText(tb)))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }
        //isDigit是否是数字
        public static bool isNumberic(string _string,string allstr, NumTextType type)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (char c in _string)
            {
                switch (type)
                {
                    case NumTextType.Int:
                        if (!char.IsDigit(c))
                            return false;
                        break;
                    case NumTextType.Float:
                        if (!char.IsDigit(c) && c != '.')
                            return false;
                        break;
                    case NumTextType.Negative:
                        if (!char.IsDigit(c) && c != '.' && c != '-')
                            return false;
                        break;
                } 
            }
            if(type == NumTextType.Float || type == NumTextType.Negative)
            {
                if (!double.TryParse(allstr, out double result))
                    return false;
            } 
            return true;
        }
        static void tbb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!isNumberic(e.Text, tb.Text.Insert(tb.SelectionStart,e.Text), GetNumText(tb)))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }
        static void tbb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        #endregion
    } 

    public enum NumTextType
    {
        Default,
        Int,
        Float,
        //负值
        Negative
    }
}
