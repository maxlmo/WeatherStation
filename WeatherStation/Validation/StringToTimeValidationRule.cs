using System;
using System.Globalization;
using System.Windows.Controls;

namespace WeatherStation.Validation
{
    public class StringToTimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return null;
            }
            DateTime result;
            var isValid = DateTime.TryParseExact((string)value, new[] {"HH:mm","H:mm"}, CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces, out result);
            return isValid ? new ValidationResult(true, null) : new ValidationResult(false, "Please enter a valid date string(HH:mm)");
        }
    }
}
