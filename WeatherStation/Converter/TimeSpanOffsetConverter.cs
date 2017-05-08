using System;
using System.Globalization;
using System.Windows.Data;
using WeatherStation.Properties;

namespace WeatherStation.Converter
{
    public class TimeSpanOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return (DateTime)value + Settings.Default.TimeSpanOffset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
