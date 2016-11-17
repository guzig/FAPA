using System;
using System.Windows;

namespace FaPA.GUI.Feautures.Fattura
{
    public static class WindowsUtil
    {
        public static void Size_Changed( SizeChangedEventArgs sizeChangedEventArgs )
        {
            var mainWindow = ( Window ) sizeChangedEventArgs.Source;
            if (mainWindow.WindowState == WindowState.Maximized)
                return;
            mainWindow.Width = Double.NaN;
            mainWindow.Height = Double.NaN;
            mainWindow.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}