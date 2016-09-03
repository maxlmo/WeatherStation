using Prism.Events;
using WeatherStation.MVVM;
using WeatherStation.Sensor;
using WeatherStation.Threads;

namespace WeatherStation
{
    public class Bootstrap
    {
        private MeasurementThread _temperatureThread;

        public void StartApplication()
        {
            var eventAggregator = new EventAggregator();
            var temperatureSensor = new TemperatureSensor(eventAggregator);
            var viewFactory = new MvvmViewFactory(eventAggregator, temperatureSensor);
            var mainWindow = viewFactory.CreateMainWindow();

            _temperatureThread = new MeasurementThread(temperatureSensor);
            _temperatureThread.StartThread();

            mainWindow.Show();
        }

        public void CloseThreads()
        {
            _temperatureThread.CloseThread();
        }
    }
}