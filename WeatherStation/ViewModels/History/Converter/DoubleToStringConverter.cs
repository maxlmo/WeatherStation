using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherStation.ViewModels.History.Converter
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnObject = (double) value;
            return returnObject.ToString("F");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}