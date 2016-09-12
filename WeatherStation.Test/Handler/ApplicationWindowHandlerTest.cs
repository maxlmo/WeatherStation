using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.MVVM;

namespace WeatherStation.Test.Handler
{
    [TestFixture]
    public class ApplicationWindowHandlerTest
    {
        [Test]
        public void OpenNewWindow_OpensCorrectWindow()
        {
            var eventAggregator = new Mock<IEventAggregator>();
            var viewFactory = new Mock<IViewFactory>();
            var classUnderTest = new ApplicationWindowHandler(eventAggregator.Object, viewFactory.Object);
        }
    }
}
