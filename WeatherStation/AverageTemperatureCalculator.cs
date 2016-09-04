using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Handler
{
    public class AverageTemperatureCalculator : IHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private double _averageDegree;
        private int _degreeCounter;
        private double _degreeSum;

        public AverageTemperatureCalculator(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(CalculateNewAverageTemperature);
        }

        private void CalculateNewAverageTemperature(TemperatureMeasurement newMeasurement)
        {
            _degreeSum = _degreeSum + newMeasurement.Value;
            _degreeCounter++;
            _averageDegree = _degreeSum/_degreeCounter;
            _eventAggregator.GetEvent<NewAverageTemperature>().Publish(new AverageTemperature { Value = _averageDegree});
        }
    }
}