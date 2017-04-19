using System.Windows;
using System.Windows.Controls;

namespace Sky.Wpf.UIControlLibrary.control
{
    public class TreeViewItemStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            //一级菜单
            TreeViewItem tvItem = item as TreeViewItem;
            tvItem.PreviewMouseLeftButtonDown += TvItem_MouseDown;
            tvItem.ItemContainerStyleSelector = new TreeViewItemStyleSelector();

            ForStyle(tvItem.Items);

            if (tvItem.Parent == null || !(tvItem.Parent is TreeViewItem))
                return Application.Current.FindResource("treeview.item.top") as Style;
            else
                return Application.Current.FindResource("treeview.item.child") as Style;
        }

        private void TvItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if(e.Source == item && item.HasItems)
                item.IsExpanded = !item.IsExpanded; 
        }

        private void ForStyle(ItemCollection items)
        {
            if (items == null)
                return;
            foreach (TreeViewItem t in items)
            {
                t.PreviewMouseLeftButtonDown += TvItem_MouseDown;
                t.Style = Application.Current.FindResource("treeview.item.child") as Style;
                ForStyle(t.Items);
            }
        }
    }
}
