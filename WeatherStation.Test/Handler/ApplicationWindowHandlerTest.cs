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
using WeatherStation.Views;

namespace WeatherStation.Test.Handler
{
    [TestFixture]
    public class ApplicationWindowHandlerTest
    {
        [Test]
        public void OpenMainWindow_OpenNewMainWindow()
        {
            
            var mainWindow = new Mock<IWindow>();
            var viewFactory = new Mock<IViewFactory>();
            viewFactory.Setup(v => v.CreateMainWindow()).Returns(mainWindow.Object);
            var classUnderTest = new ApplicationWindowHandler(CreatEventAggregator(),viewFactory.Object);

            classUnderTest.OpenNewWindow(ViewType.MainWindow);

            mainWindow.Verify( w => w.Show());
        }

        public IEventAggregator CreatEventAggregator()
        {
            var eventAgrregator = new Mock<IEventAggregator>();
            var openNewWindow = new OpenNewWindow();
            eventAgrregator.Setup(ea => ea.GetEvent<OpenNewWindow>()).Returns(openNewWindow);
            eventAgrregator.Setup(ea => ea.GetEvent<CloseWindow>()).Returns(new CloseWindow());
            eventAgrregator.Setup(ea => ea.GetEvent<WindowClosed>()).Returns(new WindowClosed());
            return eventAgrregator.Object;
        }
    }
}
