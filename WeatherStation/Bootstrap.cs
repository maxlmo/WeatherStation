using Prism.Events;
using WeatherStation.MVVM;
using WeatherStation.Sensor;
using WeatherStation.Threads;

namespace WeatherStation
{
    public class Bootstrap
    {
        private MeasurementThread _temperatureThread;
        private MeasurementThread _barPressureThread;

        public void StartApplication()
        {
            var eventAggregator = new EventAggregator();
            var temperatureSensor = new TemperatureSensor(eventAggregator);
            var barPressureSensor = new BarPressureSensor(eventAggregator);
            var viewFactory = new MvvmViewFactory(eventAggregator, temperatureSensor);
            var mainWindow = viewFactory.CreateMainWindow();
            
            StartThreads(barPressureSensor, temperatureSensor);
            mainWindow.Show();
        }

        private void StartThreads(ISensor barPressureSensor, ISensor temperatureSensor)
        {
            _temperatureThread = new MeasurementThread(temperatureSensor);
            _barPressureThread = new MeasurementThread(barPressureSensor);
            _temperatureThread.StartThread();
            _barPressureThread.StartThread();
        }

        public void CloseThreads()
        {
            _temperatureThread.CloseThread();
            _barPressureThread.CloseThread();
        }
    }
}