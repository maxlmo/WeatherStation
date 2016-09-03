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
        private TimeThread _timeThread;
        private IEventAggregator _eventAggregator;

        public void StartApplication()
        {
            _eventAggregator = new EventAggregator();
            var temperatureSensor = new TemperatureSensor(_eventAggregator);
            var barometricPressureSensor = new BarPressureSensor(_eventAggregator);
            var viewFactory = new MvvmViewFactory(_eventAggregator, temperatureSensor, barometricPressureSensor);
            var mainWindow = viewFactory.CreateMainWindow();
            
            StartThreads(barometricPressureSensor, temperatureSensor);
            mainWindow.Show();
        }

        private void StartThreads(ISensor barPressureSensor, ISensor temperatureSensor)
        {
            _temperatureThread = new MeasurementThread(temperatureSensor);
            _barPressureThread = new MeasurementThread(barPressureSensor);
            _timeThread = new TimeThread(_eventAggregator);
            _timeThread.StartThread();
            _temperatureThread.StartThread();
            _barPressureThread.StartThread();
        }

        public void CloseThreads()
        {
            _temperatureThread.CloseThread();
            _barPressureThread.CloseThread();
            _timeThread.CloseThread();
        }
    }
}