using Prism.Events;
using WeatherStation.Model;

namespace WeatherStation.Messages
{
    public class NewBarometricPressureTrend : PubSubEvent<BarometricPressureTrend>
    {
    }
}