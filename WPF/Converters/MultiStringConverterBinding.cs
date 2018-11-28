using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF.Converters
{
    public class MultiStringConverterBinding : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] != null)
                return values[1].ToString();
            if(values[0].ToString() == "{DependencyProperty.UnsetValue}")
                return "";
            return values[0].ToString();

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value, value};
        }
    }
}
