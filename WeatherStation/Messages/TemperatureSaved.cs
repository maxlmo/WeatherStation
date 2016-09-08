using Prism.Events;
using WeatherStation.Model;

namespace WeatherStation.Messages
{
    public sealed class TemperatureSaved : PubSubEvent<IMeasurement>
    {
    }
}