using System;
using System.Globalization;
using System.Windows.Data;
using WeatherStation.ViewModels.UnitSettings;

namespace WeatherStation.Views.Converter
{
    public class TemperatureUnitToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temperatureUnit = (TemperatureUnit) value;

            if (parameter.ToString() == "Fahrenheit")
            {
                return temperatureUnit == TemperatureUnit.Fahrenheit;
            }
            if (parameter.ToString() == "Celsius")
            {
                return temperatureUnit == TemperatureUnit.Celsius;
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "Fahrenheit")
            {
                return TemperatureUnit.Fahrenheit;
            }
            if (parameter.ToString() == "Celsius")
            {
                return TemperatureUnit.Celsius;
            }
            throw new NotSupportedException();
        }
    }
}