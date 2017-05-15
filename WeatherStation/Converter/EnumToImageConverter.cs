using System;
using System.Globalization;
using System.Windows.Data;
using WeatherStation.Model;

namespace WeatherStation.Converter
{
    public class EnumToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trend = (BarometricPressureTrend)value;
            switch (trend)
            {
                case BarometricPressureTrend.Stable:
                    return "../Images/stable.jpg";
                case BarometricPressureTrend.Falling:
                    return "../Images/falling.jpg";
                case BarometricPressureTrend.Rising:
                    return "../Images/rising.jpg";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}