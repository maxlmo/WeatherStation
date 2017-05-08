namespace WeatherStation.Services
{
    public class MeasurementIntervalsSettings
    {
        public MeasurementIntervalsSettings(int temperature, int barometricPressure)
        {
            TemperatureInterval = temperature;
            BarometricPressureInterval = barometricPressure;
        }

        public int BarometricPressureInterval { get; }

        public int TemperatureInterval { get; }
    }
}