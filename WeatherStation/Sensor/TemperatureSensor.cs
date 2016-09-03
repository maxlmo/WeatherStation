using System;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Sensor
{
    public class TemperatureSensor : ISensor
    {
        private readonly IEventAggregator _eventAggregator;
        public static Random Random = new Random();

        public TemperatureSensor(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void ReadMeasurement()
        {
            var randomNumber = Random.NextDouble()*60 - 20;
            _eventAggregator.GetEvent<NewTemperature>().Publish(new TemperatureMeasurement {Value = randomNumber});
        }
    }
}