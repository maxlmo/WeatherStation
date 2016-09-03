using Prism.Events;
using WeatherStation.Model;

namespace WeatherStation.Messages
{
    public class NewBarPressure : PubSubEvent<BarPressureMeasurement>
    {
    }
}