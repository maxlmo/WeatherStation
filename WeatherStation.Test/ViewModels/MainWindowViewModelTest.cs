using System.Windows.Input;
using Moq;
using NUnit.Framework;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.ViewModels.Main;

namespace WeatherStation.Test.ViewModels
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        private IEventAggregator _eventAggregator;

        private MainWindowViewModel NewTestContext()
        {
            return new MainWindowViewModel(
                _eventAggregator,
                Mock.Of<ICommand>(),
                Mock.Of<ICommand>(),
                Mock.Of<ICommand>(),
                Mock.Of<ICommand>());
        }

        [Test]
        public void MainWindowViewModel_UpdatesTemperature_OnTemperatureRead()
        {
            const double newTemperature = 123.34;
            const string expectedResult = "123.34";
            _eventAggregator = new EventAggregator();
            var classUnderTest = NewTestContext();

            _eventAggregator.GetEvent<NewTemperature>().Publish(new TemperatureMeasurement {Value = newTemperature});

            Assert.That(classUnderTest.Temperature, Is.EqualTo(expectedResult));
        }
    }
}