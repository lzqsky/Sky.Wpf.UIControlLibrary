using Microsoft.Practices.Prism.Regions;
using Sky.Wpf.UIControlLibrary.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : SkyWindow
    {
        IRegionManager _regionManager;
        public MainWindow(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent(); 
        }

        
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        { 
            TreeViewItem item = e.NewValue as TreeViewItem; 
            if (item.Tag == null)
                return;
            
            Uri uri = new Uri(item.Tag.ToString(), UriKind.Relative);
            //导航到MainRegion,把Uri指向的Page显示在PrismRegionShell里命名为MainRegion的Region里  
            _regionManager.RequestNavigate("MainRegion", uri); 
        }
    }
}
