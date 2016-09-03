using System;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Sensor
{
    public class BarPressureSensor : ISensor
    {
        private readonly IEventAggregator _eventAggregator;
        public static Random Random = new Random();

        public BarPressureSensor(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void ReadMeasurement()
        {
            var randomNumber = Random.NextDouble() * 70 + 980;
            _eventAggregator.GetEvent<NewBarPressure>().Publish(new BarPressureMeasurement { Value = randomNumber });
        }
    }
}