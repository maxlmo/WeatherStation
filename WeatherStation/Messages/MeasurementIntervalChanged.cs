using Prism.Events;
using WeatherStation.Services;

namespace WeatherStation.Messages
{
    public class MeasurementIntervalChanged : PubSubEvent<MeasurementIntervalsSettings>
    {
    }
}
