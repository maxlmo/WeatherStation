using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherStation.Converter
{
    public class TimeStampToDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnObject = (DateTime) value;
            return returnObject.ToString("d");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
