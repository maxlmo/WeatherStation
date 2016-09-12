using System;
using System.Globalization;
using System.Windows.Data;
using WeatherStation.Properties;
using WeatherStation.ViewModels.UnitSettings;

namespace WeatherStation.Views.Converter
{
    public class BarometricPressureMeasurementUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnObject = (double)value;
            var unit = (BarometricPressureUnit)Settings.Default.BarometricPressureUnit;
            var multiplier = Constants.InHgMultiplier;

            switch (unit)
            {
                case BarometricPressureUnit.InHg:
                    return (returnObject * multiplier).ToString("F") + " inHg";

                case BarometricPressureUnit.MBar:
                    return returnObject.ToString("F") + " mBar";
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}