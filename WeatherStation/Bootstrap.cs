﻿using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.MVVM;
using WeatherStation.Sensor;
using WeatherStation.Storage;
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
        private IDataBaseConnector _temperatureDataBaseConnector;
        private IDataBaseConnector _barometricPressureDataBaseConnector;

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
                _temperatureDataBaseConnector, 
                _barometricPressureDataBaseConnector);
            var mainWindow = viewFactory.CreateMainWindow();
            
            StartThreads(barometricPressureSensor, temperatureSensor);
            mainWindow.Show();
        }

        private void InitializeDataBaseConnection()
        {
            FluentNHibernateHelper.InitializeDatabase();

            _barometricPressureDataBaseConnector = new BarometricPressureDataBaseConnector(_eventAggregator);
            _temperatureDataBaseConnector = new TemperatureDataBaseConnector(_eventAggregator);

            _eventAggregator.GetEvent<NewTemperature>().Subscribe(_temperatureDataBaseConnector.SaveMeasurement);
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(_barometricPressureDataBaseConnector.SaveMeasurement);
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