using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SyteLineDevTools.Converters
{
    public class BooleanConverter : DependencyObject, IValueConverter
    {

        public static readonly DependencyProperty FalseValueProperty = DependencyProperty.Register("FalseValue", typeof(object), typeof(BooleanConverter));
        public object FalseValue
        {
            get { return GetValue(FalseValueProperty); }
            set { SetValue(FalseValueProperty, value); }
        }


        public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register("TrueValue", typeof(object), typeof(BooleanConverter));
        public object TrueValue
        {
            get { return GetValue(TrueValueProperty); }
            set { SetValue(TrueValueProperty, value); }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return FalseValue;
            }

            if ((bool)value)
            {
                return TrueValue;
            }
            return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
