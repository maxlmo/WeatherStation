using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WeatherStation.Model;

namespace WeatherStation.ViewModels.Converter
{
    public class EnumToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trend = (BarometricPressureTrend) value;
            switch (trend)
            {
                case BarometricPressureTrend.Stable:
                    return "Images/stable.png";
                case BarometricPressureTrend.Falling:
                    return "Images/falling.png";
                case BarometricPressureTrend.Rising:
                    return "Images/rising.png";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}