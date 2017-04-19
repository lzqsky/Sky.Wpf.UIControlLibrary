using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sky.Wpf.UIControlLibrary.control.animating
{
    public static class ItemAnimating
    {
        public static bool GetRemoveItemBegin(DependencyObject obj)
        {
            return (bool)obj.GetValue(RemoveItemBeginProperty);
        }

        public static void SetRemoveItemBegin(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveItemBeginProperty, value);
        }

        //Item 删除 开始
        public static readonly DependencyProperty RemoveItemBeginProperty =
            DependencyProperty.RegisterAttached("RemoveItemBegin", typeof(bool), typeof(ItemAnimating), new PropertyMetadata(false));


        public static bool GetRemoveItemComplete(DependencyObject obj)
        {
            return (bool)obj.GetValue(RemoveItemCompleteProperty);
        }

        public static void SetRemoveItemComplete(DependencyObject obj, bool value)
        {
            obj.SetValue(RemoveItemCompleteProperty, value);
        }

        //Item 删除 完成
        public static readonly DependencyProperty RemoveItemCompleteProperty =
            DependencyProperty.RegisterAttached("RemoveItemComplete", typeof(bool), typeof(ItemAnimating), new PropertyMetadata(OnRemoveItemComplete));

        static Hashtable itemsToRemove = new Hashtable();
        public static void RemoveItem<T>(this ICollection<T> dataSource, T item) where T : DependencyObject
        {
            if (itemsToRemove.Contains(item))
            {
                dataSource.Remove(item);
                itemsToRemove.Remove(item);
            }
            itemsToRemove.Add(item, dataSource);
            SetRemoveItemBegin(item, true);
        }

        static void OnRemoveItemComplete(DependencyObject container, DependencyPropertyChangedEventArgs e)
        {
            object data = null;
            if (container is ContentControl)
            {
                data = ((ContentControl)container).Content;
            }

            if (container is ContentPresenter)
            {
                data = ((ContentPresenter)container).Content;
            }
            if (data == null)
                return;

            if (!itemsToRemove.Contains(data) || (bool)e.NewValue == false)
            {
                return;
            }
            var dataSource = itemsToRemove[data];
            dataSource.GetType().GetMethod("Remove").Invoke(dataSource, new object[] { data });
            itemsToRemove.Remove(data);
        }
    }
}
