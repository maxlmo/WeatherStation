using Prism.Events;
using WeatherStation.Model;

namespace WeatherStation.Messages
{
    public class NewTemperature : PubSubEvent<TemperatureMeasurement>
    {
    }
}