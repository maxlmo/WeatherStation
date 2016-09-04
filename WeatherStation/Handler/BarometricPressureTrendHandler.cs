using System;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Handler
{
    public class BarometricPressureTrendHandler : IHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private double _lastBarometricPressure = 1015;
        private DateTime _lastMeasurementTime;
        private BarometricPressureTrend _nextTrend;

        public BarometricPressureTrendHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(CalculateBarometricPressureTrend);
        }

        private void CalculateBarometricPressureTrend(BarPressureMeasurement newBarPressure)
        {
            var barometricPressureTrend = newBarPressure.Value - _lastBarometricPressure;
            _lastBarometricPressure = newBarPressure.Value;
            _nextTrend = BarometricPressureTrend.Stable;

            if (barometricPressureTrend >= 4 || barometricPressureTrend <= -4)
            {
                var barPerMinute = barometricPressureTrend/
                            (newBarPressure.TimeStamp.Millisecond - _lastMeasurementTime.Millisecond)*60;
                _nextTrend = ConvertBarPerMinuteIntoTrend(barPerMinute);
                _lastMeasurementTime = newBarPressure.TimeStamp;
            }

            _eventAggregator.GetEvent<NewBarometricPressureTrend>().Publish(_nextTrend);
        }

        private BarometricPressureTrend ConvertBarPerMinuteIntoTrend(double barPerMinute)
        {
            if (barPerMinute > 10)
            {
                return BarometricPressureTrend.Rising;
            }
            if (barPerMinute < 10)
            {
                return BarometricPressureTrend.Falling;
            }
            return BarometricPressureTrend.Stable;
        }
    }
}