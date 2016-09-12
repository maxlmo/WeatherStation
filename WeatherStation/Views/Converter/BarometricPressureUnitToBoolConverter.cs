using System;
using System.Globalization;
using System.Windows.Data;
using WeatherStation.ViewModels.UnitSettings;

namespace WeatherStation.Views.Converter
{
    public class BarometricPressureToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unit = (BarometricPressureUnit)value;

            if (parameter.ToString() == "MBar")
            {
                return unit == BarometricPressureUnit.MBar;
            }
            if (parameter.ToString() == "InHg")
            {
                return unit == BarometricPressureUnit.InHg;
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "MBar")
            {
                return BarometricPressureUnit.MBar;
            }
            if (parameter.ToString() == "InHg")
            {
                return BarometricPressureUnit.InHg;
            }
            throw new NotSupportedException();
        }
    }
}