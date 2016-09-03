using System;
using Prism.Events;
using WeatherStation.Messages;

namespace WeatherStation.Sensor
{
    public class RandomizedSensor : ISensor
    {
        private readonly IEventAggregator _eventAggregator;
        public static Random Random = new Random();

        public RandomizedSensor(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void ReadTemp()
        {
            var randomNumber = Random.NextDouble()*60 - 20;
            _eventAggregator.GetEvent<NewMeasurement>().Publish(new Measurement {Value = randomNumber});
        }

        public void ReadBar()
        {
            var randomNumber = Random.NextDouble()*70 + 980;
            _eventAggregator.GetEvent<NewMeasurement>().Publish(new Measurement {Value = randomNumber});
        }
    }
}