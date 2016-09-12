using System;
using System.Globalization;
using System.Windows.Data;
using WeatherStation.Properties;
using WeatherStation.ViewModels.UnitSettings;

namespace WeatherStation.Views.Converter
{
    public class TemperatureMeasurementUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temperatureValue = (double) value;
            var unit = (TemperatureUnit) Settings.Default.TemperatureUnit;

            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    return temperatureValue.ToString("F") + " °C";

                case TemperatureUnit.Fahrenheit:
                    return IntoFahrenheit(temperatureValue).ToString("F") + " °F";
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private double IntoFahrenheit(double value)
        {
            return value*1.8 + 32;
        }
    }
}