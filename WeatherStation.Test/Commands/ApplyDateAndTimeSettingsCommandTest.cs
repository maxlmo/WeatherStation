using System;
using Moq;
using NUnit.Framework;
using Prism.Events;
using WeatherStation.Commands;
using WeatherStation.Services;
using WeatherStation.ViewModels;

namespace WeatherStation.Test.Commands
{
    [TestFixture]
    public class ApplyDateAndTimeSettingsCommandTest
    {
        [Test]
        public void Execute_SavesTimeSpanOffsetInSettings()
        {
            var settingsService = new Mock<ISettingsService>();
            var cut = new ApplyDateAndTimeSettingsCommand(
                new Mock<IEventAggregator>().Object,
                new DateAndTimeSettingsWindowViewModel(), 
                settingsService.Object);

            cut.Execute(null);

            settingsService.Verify(s => s.SaveDateTimeOffset(It.IsAny<TimeSpan>()));
        }
    }
}