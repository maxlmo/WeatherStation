using Prism.Events;
using WeatherStation.ViewModels.UnitSettings;

namespace WeatherStation.ViewModels.Main
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