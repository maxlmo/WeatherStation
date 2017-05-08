using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Sensor;
using WeatherStation.Services;

namespace WeatherStation.Threads
{
    public class TemperatureThread : MeasurementThread
    {
        private readonly IEventAggregator _eventAggregator;
        private int _measurementInterval;

        public TemperatureThread(ISensor sensor, IEventAggregator eventAggregator, ISettingsService settingsService) : base(sensor)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<MeasurementIntervalChanged>().Subscribe(MeasurementIntervalChanged);
            _measurementInterval = settingsService.LoadMeasurementIntervalsSettings().TemperatureInterval;
        }

        private void MeasurementIntervalChanged(MeasurementIntervalsSettings newSettings)
        {
            _measurementInterval = newSettings.TemperatureInterval;
        }

        protected override int GetMeasurementInterval()
        {
            return _measurementInterval;
        }

        protected override void SendWaitUpdate(int timeToWait)
        {
            _eventAggregator.GetEvent<TemperatureWaitTimeChanged>().Publish(timeToWait);
        }
    }
}
