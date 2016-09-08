using System;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Model;
using WeatherStation.MVVM;
using WeatherStation.Sensor;
using WeatherStation.Storage;
using WeatherStation.Storage.Repository;
using WeatherStation.Threads;

namespace WeatherStation
{
    public class Bootstrap
    {
        private MeasurementThread _temperatureThread;
        private MeasurementThread _barPressureThread;
        private TimeThread _timeThread;
        private IDataBaseConnector _dataBaseConnector;
        private IEventAggregator _eventAggregator;
        private IRepository _temperatureRepository;
        private IRepository _barometricPressureRepository;

        public void StartApplication()
        {
            _eventAggregator = new EventAggregator();
            InitializeDataBaseConnection();
            var temperatureSensor = new TemperatureSensor(_eventAggregator);
            var barometricPressureSensor = new BarPressureSensor(_eventAggregator);
            var viewFactory = new MvvmViewFactory(
                _eventAggregator, 
                temperatureSensor, 
                barometricPressureSensor, 
                _temperatureRepository, 
                _barometricPressureRepository);
            var mainWindow = viewFactory.CreateMainWindow();
            
            StartThreads(barometricPressureSensor, temperatureSensor);
            mainWindow.Show();
        }

        private void InitializeDataBaseConnection()
        {
            FluentNHibernateHelper.InitializeDatabase();
            _dataBaseConnector = new DataBaseConnector();
            _barometricPressureRepository = new BarometricPressureRepository(_eventAggregator,_dataBaseConnector);
            _temperatureRepository = new TemperatureRepository(_eventAggregator, _dataBaseConnector);
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