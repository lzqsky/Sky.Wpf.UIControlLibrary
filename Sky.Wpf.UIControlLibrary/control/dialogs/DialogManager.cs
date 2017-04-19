using Sky.Wpf.UIControlLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sky.Wpf.UIControlLibrary.control.dialogs
{
    public static class DialogManager
    {
        public static MessageBoxResult Show(string msg)
        {
            return Show(msg, MessageBoxButton.OK, MessageBoxImage.None);
        }
        public static MessageBoxResult Show(string msg, MessageBoxButton button)
        {
            return Show(msg, button, MessageBoxImage.None);
        }
        public static MessageBoxResult Show(string msg, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageDialog dialog = new MessageDialog(button);
            dialog.Owner = win32.GetTopWindow();
            dialog.MsgContent = msg;
            dialog.ShowDialog();
            return dialog.MessageResult;
        }
    }
}
