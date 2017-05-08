using Prism.Events;

namespace WeatherStation.Messages
{
    public class TemperatureWaitTimeChanged : PubSubEvent<int>
    {
    }

    public class BarometricPressureWaitTimeChanged : PubSubEvent<int>
    {
    }
}
