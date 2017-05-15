using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.MVVM;
using WeatherStation.Sensor;
using WeatherStation.Services;
using WeatherStation.Storage;
using WeatherStation.Threads;

namespace WeatherStation
{
    public class Bootstrap
    {
        private IMeasurementsRepository _barometricPressureMeasurementsRepository;
        private MeasurementThread _barPressureThread;
        private IEventAggregator _eventAggregator;
        private IMeasurementsRepository _temperatureMeasurementsRepository;
        private MeasurementThread _temperatureThread;
        private TimeThread _timeThread;
        private IWindowFactory _windowFactory;
        private ISettingsService _settingsService;

        public void StartApplication()
        {
            _eventAggregator = new EventAggregator();
            _settingsService = new ApplicationSettingsService();
            InitializeDataBaseConnection();
            var temperatureSensor = new TemperatureSensor(_eventAggregator);
            var barometricPressureSensor = new BarPressureSensor(_eventAggregator);
            _windowFactory = new MvvmWindowFactory(
                _eventAggregator,
                temperatureSensor,
                barometricPressureSensor,
                _temperatureMeasurementsRepository,
                _barometricPressureMeasurementsRepository, 
                _settingsService);
            
            var applicationWindowHandler = CreateApplicationWindowHandler();

            applicationWindowHandler.OpenNewWindow(WindowType.MainWindow);
            StartThreads(barometricPressureSensor, temperatureSensor);
        }

        private void InitializeDataBaseConnection()
        {
            FluentNHibernateHelper.InitializeDatabase();

            _barometricPressureMeasurementsRepository = new BarometricPressureMeasurementsRepository(_eventAggregator);
            _temperatureMeasurementsRepository = new TemperatureMeasurementsRepository(_eventAggregator);

            _eventAggregator.GetEvent<NewTemperature>().Subscribe(_temperatureMeasurementsRepository.SaveMeasurement);
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(_barometricPressureMeasurementsRepository.SaveMeasurement);
        }

        private void StartThreads(ISensor barPressureSensor, ISensor temperatureSensor)
        {
            _temperatureThread = new TemperatureThread(temperatureSensor,_eventAggregator, _settingsService);
            _barPressureThread = new BarometricPressureThread(barPressureSensor, _eventAggregator, _settingsService);
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

        private ApplicationWindowHandler CreateApplicationWindowHandler()
        {
            var applicationWindowHandler = new ApplicationWindowHandler(_eventAggregator, _windowFactory);
            _eventAggregator.GetEvent<OpenNewWindow>().Subscribe(applicationWindowHandler.OpenNewWindow);
            _eventAggregator.GetEvent<CloseWindow>().Subscribe(applicationWindowHandler.CloseWindow);
            _eventAggregator.GetEvent<WindowClosed>().Subscribe(applicationWindowHandler.WindowClosed);
            return applicationWindowHandler;
        }
    }
}