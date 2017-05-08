using System;
using Moq;
using NUnit.Framework;
using WeatherStation.Commands;
using WeatherStation.Services;
using WeatherStation.Test.MockExtensions;
using WeatherStation.ViewModels;

namespace WeatherStation.Test.Commands
{
    [TestFixture]
    public class ApplyMeasurementIntervalSettingsCommandTest
    {
        private const int TEMPERATURE_INTERVAL = 200;
        private const int BAROMETRIC_PRESSURE_INTERVAL = 500;

        [Test]
        public void Execute_SavesTimeSpanOffsetInSettings()
        {
            var settingsService = new Mock<ISettingsService>()
                .ReturnsIntervalSettings(
                    new MeasurementIntervalsSettings(TEMPERATURE_INTERVAL, BAROMETRIC_PRESSURE_INTERVAL));
            var cut = new ApplyMeasurementIntervalSettingsCommand(settingsService.Object,
                new MeasurementIntervalsSettingsWindowViewModel(settingsService.Object));

            cut.Execute(null);

            settingsService.Verify(s => s.SaveMeasurementIntervals(It.IsAny<MeasurementIntervalsSettings>()));
        }
    }
}