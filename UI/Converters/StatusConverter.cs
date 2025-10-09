using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Domain;

namespace UI.Converters
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RepairRequestStatus status)
            {
                return status.ToPrettyString();
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
      
            return DependencyProperty.UnsetValue;
        }
    }
}