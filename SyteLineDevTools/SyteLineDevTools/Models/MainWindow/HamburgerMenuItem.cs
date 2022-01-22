using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SyteLineDevTools.Models.MainWindow
{
    public class HamburgerMenuItem : ListViewItem
    {
        public static string GetValue(DependencyObject Obj)
        {
            return (string)Obj.GetValue(ValueProperty);
        }
        public static void SetValue(DependencyObject Obj, string Value)
        {
            Obj.SetValue(ValueProperty, Value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value",
                 typeof(string), typeof(HamburgerMenuItem), new FrameworkPropertyMetadata(""));

    }
}
