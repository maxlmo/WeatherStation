using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Sensor;
using WeatherStation.Services;

namespace WeatherStation.Threads
{
    public class BarometricPressureThread : MeasurementThread
    {
        private int _measurementInterval;

        public BarometricPressureThread(ISensor sensor, IEventAggregator eventAggregator, ISettingsService settingsService) : base(sensor)
        {
            _measurementInterval = settingsService.LoadMeasurementIntervalsSettings().BarometricPressureInterval;
            eventAggregator.GetEvent<MeasurementIntervalChanged>().Subscribe(MeasurementIntervalChanged);
        }

        private void MeasurementIntervalChanged(MeasurementIntervalsSettings newSettings)
        {
            _measurementInterval = newSettings.BarometricPressureInterval;
        }

        protected override int MeasurementInterval()
        {
            return _measurementInterval;
        }
    }
}
