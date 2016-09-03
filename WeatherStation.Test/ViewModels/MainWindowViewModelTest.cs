using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Moq;
using NUnit.Framework;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.ViewModels;

namespace WeatherStation.Test.ViewModels
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        [Test]
        public void MainWindowViewModel_UpdatesTemperature_OnTemperatureRead()
        {
            const double newTemperature = 123.34;
            const string expectedResult = "123.34";
            var eventAggregator = new EventAggregator();
            var classUnderTest = new MainWindowViewModel(
                eventAggregator, 
                Mock.Of<ICommand>(),
                Mock.Of<ICommand>(),
                Mock.Of<ICommand>(),
                Mock.Of<ICommand>());

            eventAggregator.GetEvent<NewTemperature>().Publish(new TemperatureMeasurement {Value = newTemperature });

            Assert.That(classUnderTest.Temperature, Is.EqualTo(expectedResult));
        }
    }
}
