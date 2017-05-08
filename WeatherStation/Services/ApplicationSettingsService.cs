using System;
using WeatherStation.Properties;

namespace WeatherStation.Services
{
    public class ApplicationSettingsService : ISettingsService
    {
        public void SaveDateTimeOffset(TimeSpan offset)
        {
            Settings.Default.TimeSpanOffset = offset;
            Settings.Default.Save();
        }

        public void SaveMeasurementIntervals(MeasurementIntervalsSettings newMeasurementIntervalsSettings)
        {
            Settings.Default.BarometricPressureMeasurementInterval =
                newMeasurementIntervalsSettings.BarometricPressureInterval;
            Settings.Default.TemperatureMeasurementInterval = newMeasurementIntervalsSettings.TemperatureInterval;
            Settings.Default.Save();
        }

        public MeasurementIntervalsSettings LoadMeasurementIntervalsSettings()
        {
            return new MeasurementIntervalsSettings(Settings.Default.TemperatureMeasurementInterval,
                Settings.Default.BarometricPressureMeasurementInterval);
        }
    }
}