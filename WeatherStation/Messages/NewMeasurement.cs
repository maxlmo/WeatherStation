using Prism.Events;

namespace WeatherStation.Messages
{
    public class NewMeasurement : PubSubEvent<Measurement>
    {
    }

    public class Measurement
    {
        public double Value { get; set; }
    }
}