using System;
using System.Globalization;
using System.Windows.Controls;

namespace WeatherStation.Validation
{
    public class StringToDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return null;
            }
            DateTime result;
            var isValid = DateTime.TryParseExact((string)value, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal, out result);
            return isValid ? new ValidationResult(true, null) : new ValidationResult(false, "Please enter a valid date string(mm.dd.yyyy)");
        }
    }
}
