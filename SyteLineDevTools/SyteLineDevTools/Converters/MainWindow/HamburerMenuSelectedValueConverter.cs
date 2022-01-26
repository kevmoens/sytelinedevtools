using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SyteLineDevTools.Models.MainWindow;

namespace SyteLineDevTools.Converters.MainWindow
{
    public class HamburerMenuSelectedValueConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return string.Empty;
            }
            var view = HamburgerMenuItem.GetValue(((System.Windows.Controls.ListViewItem)value));
            return view;
        }
    }
}
