using Moq;
using NUnit.Framework;
using Prism.Events;
using WeatherStation.Commands;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.MVVM;
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
        public void Execute_PublishesMeasurementIntervalChanged()
        {
            var measurementIntervalChanged = new Mock<MeasurementIntervalChanged>();
            var cut = new ApplyMeasurementIntervalSettingsCommand(
                new Mock<IEventAggregator>()
                    .ReturnsEvent(new Mock<CloseWindow>().Object)
                    .ReturnsEvent(measurementIntervalChanged.Object).Object,
                new Mock<ISettingsService>().Object,
                new MeasurementIntervalsSettingsWindowViewModel(new Mock<ISettingsService>().ReturnsIntervalSettings(
                        new MeasurementIntervalsSettings(TEMPERATURE_INTERVAL, BAROMETRIC_PRESSURE_INTERVAL))
                    .Object));

            cut.Execute(null);

            measurementIntervalChanged.Verify(m => m.Publish(It.IsAny<MeasurementIntervalsSettings>()));
        }

        [Test]
        public void Execute_PublishesCloseWindow()
        {
            var closeWindow = new Mock<CloseWindow>();
            var cut = new ApplyMeasurementIntervalSettingsCommand(
                new Mock<IEventAggregator>()
                    .ReturnsEvent(new Mock<MeasurementIntervalChanged>().Object)
                    .ReturnsEvent(closeWindow.Object).Object,
                new Mock<ISettingsService>().Object,
                new MeasurementIntervalsSettingsWindowViewModel(new Mock<ISettingsService>().ReturnsIntervalSettings(
                        new MeasurementIntervalsSettings(TEMPERATURE_INTERVAL, BAROMETRIC_PRESSURE_INTERVAL))
                    .Object));

            cut.Execute(null);

            closeWindow.Verify(m => m.Publish(WindowType.MeasurementIntervalsSettings));
        }

        [Test]
        public void Execute_SavesTimeSpanOffsetInSettings()
        {
            var settingsService = new Mock<ISettingsService>()
                .ReturnsIntervalSettings(
                    new MeasurementIntervalsSettings(TEMPERATURE_INTERVAL, BAROMETRIC_PRESSURE_INTERVAL));
            var cut = new ApplyMeasurementIntervalSettingsCommand(
                new Mock<IEventAggregator>()
                    .ReturnsEvent(new Mock<CloseWindow>().Object)
                    .ReturnsEvent(new Mock<MeasurementIntervalChanged>().Object).Object,
                settingsService.Object,
                new MeasurementIntervalsSettingsWindowViewModel(settingsService.Object));

            cut.Execute(null);

            settingsService.Verify(s => s.SaveMeasurementIntervals(It.IsAny<MeasurementIntervalsSettings>()));
        }
    }
}