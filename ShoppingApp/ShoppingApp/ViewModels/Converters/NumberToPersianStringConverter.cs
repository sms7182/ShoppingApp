using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ShoppingApp.ViewModels.Converters
{
    public class NumberToPersianStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            for (int i = 48; i < 58; i++)
            {
                str = str.Replace(System.Convert.ToChar(i), System.Convert.ToChar(1728 + i));
            }

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            for (int i = (1728+48); i < (1728+58); i++)
            {
                str = str.Replace(System.Convert.ToChar(i), System.Convert.ToChar(i- 1728));
            }
            return str;
        }
    }
}
