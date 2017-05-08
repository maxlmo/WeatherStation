using Prism.Events;

namespace WeatherStation.ViewModels
{
    public class MeasurementUnitChanged : PubSubEvent<CurrentMeasurementUnit>
    {
    }

    public class CurrentMeasurementUnit
    {
        public TemperatureUnit Temperature { get; set; }
        public BarometricPressureUnit BarometricPressure { get; set; }
    }
}