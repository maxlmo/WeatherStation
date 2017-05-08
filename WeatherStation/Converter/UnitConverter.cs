namespace WeatherStation.Converter
{
    public static class UnitConverter
    {
        public static double IntoFahrenheit(double celsius)
        {
            return (celsius * 1.8 + 32);
        }

        public static double IntoInHg(double mbar)
        {
            return (Constants.InHgMultiplier*mbar);
        }
    }
}
