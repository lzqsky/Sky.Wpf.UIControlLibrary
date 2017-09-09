using Sky.Wpf.UIControlLibrary.control;
using Sky.Wpf.UIControlLibrary.control.dialogs;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Test.ItemMode;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : SkyWindow
    {
        Dictionary<string, UserControl> _ctrlDic = new Dictionary<string, UserControl>();
        public MainWindow()
        {
            InitializeComponent(); 
        }

        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    MessageBoxResult resutl = DialogManager.Show("没有什么不可能的？");

        //    tb_result.Text = resutl.ToString();
        //}

        //private void Button_Click1(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult resutl = DialogManager.Show("确定没有什么不可能的？", MessageBoxButton.OKCancel);

        //    tb_result.Text = resutl.ToString();
        //}

        //private void Button_Click2(object sender, RoutedEventArgs e)
        //{
        //    //TipManager.ShowTip();
        //    Sky.Wpf.UIControlLibrary.control.tip.TipManager.ShowTip("啊驾驶的空间安徽省科技对话框发坚实的后方可就");
        //}

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = e.NewValue as TreeViewItem;
            UserControl usctrol = null;
            if(item!= null)
            {
                string key = item.Header.ToString();
                if (_ctrlDic.ContainsKey(key))
                {
                    usctrol = _ctrlDic[key];
                }
                else
                {
                    switch (key)
                    {
                        case "按钮组":
                            usctrol = new ButtonControl();
                            break;
                        case "表单集合":
                            usctrol = new TextBoxControl();
                            break;

                    }

                    if(usctrol != null)
                        _ctrlDic.Add(key, usctrol);
                }
            }

            if(usctrol != null)
            {
                if(grid_content.Children.Count > 0)
                {
                    if (usctrol.Equals(grid_content.Children[0]))
                        return;
                    grid_content.Children.Clear();
                }
                
                grid_content.Children.Add(usctrol);
            }
        }
    }
}
