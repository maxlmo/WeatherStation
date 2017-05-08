using System;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Sensor;
using WeatherStation.Services;

namespace WeatherStation.Threads
{
    public class TemperatureThread : MeasurementThread
    {
        private int _measurementInterval;

        public TemperatureThread(ISensor sensor, IEventAggregator eventAggregator, ISettingsService settingsService) : base(sensor)
        {
            eventAggregator.GetEvent<MeasurementIntervalChanged>().Subscribe(MeasurementIntervalChanged);
            _measurementInterval = settingsService.LoadMeasurementIntervalsSettings().TemperatureInterval;
        }

        private void MeasurementIntervalChanged(MeasurementIntervalsSettings newSettings)
        {
            _measurementInterval = newSettings.TemperatureInterval;
        }

        protected override int MeasurementInterval()
        {
            return _measurementInterval;
        }
    }
}
