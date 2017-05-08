using System;
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
    public class ApplyDateAndTimeSettingsCommandTest
    {
        [Test]
        public void Execute_PublishesCloseWindow()
        {
            var closeWindow = new Mock<CloseWindow>();
            var cut = new ApplyDateAndTimeSettingsCommand(
                new Mock<IEventAggregator>()
                    .ReturnsEvent(new Mock<DateTimeOffsetChanged>().Object)
                    .ReturnsEvent(closeWindow.Object)
                    .Object,
                new DateAndTimeSettingsWindowViewModel(),
                new Mock<ISettingsService>().Object);

            cut.Execute(null);

            closeWindow.Verify(a => a.Publish(WindowType.DateAndTimeSettings));
        }

        [Test]
        public void Execute_PublishesDateTimeOffsetChanged()
        {
            var dateTimeOffsetChanged = new Mock<DateTimeOffsetChanged>();
            var cut = new ApplyDateAndTimeSettingsCommand(
                new Mock<IEventAggregator>().ReturnsEvent(dateTimeOffsetChanged.Object)
                    .ReturnsEvent(new Mock<CloseWindow>().Object)
                    .Object,
                new DateAndTimeSettingsWindowViewModel(),
                new Mock<ISettingsService>().Object);

            cut.Execute(null);

            dateTimeOffsetChanged.Verify(a => a.Publish(It.IsAny<TimeSpan>()));
        }

        [Test]
        public void Execute_SavesTimeSpanOffsetInSettings()
        {
            var settingsService = new Mock<ISettingsService>();
            var cut = new ApplyDateAndTimeSettingsCommand(
                new Mock<IEventAggregator>()
                    .ReturnsEvent(new Mock<DateTimeOffsetChanged>().Object)
                    .ReturnsEvent(new Mock<CloseWindow>().Object)
                    .Object,
                new DateAndTimeSettingsWindowViewModel(),
                settingsService.Object);

            cut.Execute(null);

            settingsService.Verify(s => s.SaveDateTimeOffset(It.IsAny<TimeSpan>()));
        }
    }
}