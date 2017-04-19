using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sky.Wpf.UIControlLibrary.control.dialogs
{
    [TemplatePart(Name = PART_TOOL, Type = typeof(StackPanel))]
    public class MessageDialog : Window
    {
        private const string PART_TOOL = "PART_TOOL";

        public MessageDialog(MessageBoxButton button)
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Width = 390;
            this.Height = 231;
            _typebutton = button;
        }
        static MessageDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialog), new FrameworkPropertyMetadata(typeof(MessageDialog)));
        }

        private Button _okButton;
        private Button _canelButton;
        private MessageBoxButton _typebutton;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseMove += MessageDialog_MouseMove;

            StackPanel panel = GetTemplateChild(PART_TOOL) as StackPanel;
            if (panel != null)
            {
                switch (_typebutton)
                {
                    case MessageBoxButton.OK:
                        {
                            _okButton = new Button() { Content = "确定" };
                            _okButton.Click += _okButton_Click;
                            panel.Children.Add(_okButton);
                        }
                        break;
                    case MessageBoxButton.OKCancel:
                    case MessageBoxButton.YesNo:
                        {
                            _okButton = new Button() { Content = "确定" };
                            _okButton.Click += _okButton_Click;
                            panel.Children.Add(_okButton);

                            _canelButton = new Button() { Content = "取消", Margin = new Thickness(30, 0, 0, 0) };
                            _canelButton.Click += _canelButton_Click;
                            panel.Children.Add(_canelButton);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void _canelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageResult = MessageBoxResult.Cancel;
            this.DialogResult = false;
        }

        private void _okButton_Click(object sender, RoutedEventArgs e)
        {
            MessageResult = MessageBoxResult.OK;
            this.DialogResult = true;
        }

        private void MessageDialog_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        //结果信息
        public MessageBoxResult MessageResult { get; set; }

        #region 属性


        public string TitleContent
        {
            get { return (string)GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        //标题
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(string), typeof(MessageDialog), new PropertyMetadata(""));




        public string MsgContent
        {
            get { return (string)GetValue(MsgContentProperty); }
            set { SetValue(MsgContentProperty, value); }
        }

        //提示内容
        public static readonly DependencyProperty MsgContentProperty =
            DependencyProperty.Register("MsgContent", typeof(string), typeof(MessageDialog), new PropertyMetadata(""));


        #endregion
    }
}
