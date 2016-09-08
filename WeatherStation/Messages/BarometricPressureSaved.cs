using Prism.Events;
using WeatherStation.Model;

namespace WeatherStation.Messages
{
    public sealed class BarometricPressureSaved : PubSubEvent<IMeasurement>
    {
    }
}