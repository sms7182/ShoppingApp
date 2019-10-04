﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ShoppingApp.ViewModels.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var persianCal = new PersianCalendar();
            var newDate = string.Format("{0}/{1}/{2} {3}:{4}", persianCal.GetYear(date), persianCal.GetMonth(date), persianCal.GetDayOfMonth(date), date.Hour, date.Minute);
            for (int i = 48; i < 58; i++)
            {
                newDate = newDate.Replace(System.Convert.ToChar(i), System.Convert.ToChar(1728 + i));
            }

            return newDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now;
        }
    }
}
