using Moq;
using WeatherStation.Services;

namespace WeatherStation.Test.MockExtensions
{
    public static class SettingsServiceMockExtensions
    {
        public static Mock<ISettingsService> ReturnsIntervalSettings(this Mock<ISettingsService> mock,
            MeasurementIntervalsSettings settings)
        {
            mock.Setup(m => m.LoadMeasurementIntervalsSettings()).Returns(settings);
            return mock;
        }
    }
}
