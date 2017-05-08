using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Converter;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private string _averageTemperature;
        private string _barometricPressureLabel;
        private BarometricPressureTrend _barometricPressureTrend;
        private int _barometricPressureWaitTime;
        private string _barPressure;
        private BarometricPressureUnit _currentBarometricPressureUnit;
        private DateTime _currentDateTime;
        private TemperatureUnit _currenTemperatureUnit;
        private List<object> _handler;
        private string _temperature;
        private string _temperatureLabel;
        private int _temperatureWaitTime;

        public MainWindowViewModel(
            IEventAggregator eventAggregator,
            ICommand openWindowCommand,
            ICommand closeApplicationCommand,
            ICommand readTemperatureCommand,
            ICommand readBarometricPressureCommand)
        {
            _eventAggregator = eventAggregator;
            ReadBarometricPressureCommand = readBarometricPressureCommand;
            ReadTemperatureCommand = readTemperatureCommand;
            CloseApplicationCommand = closeApplicationCommand;
            OpenWindowCommand = openWindowCommand;
            SubscribeForEvents();
        }

        public DateTime CurrentDateTime
        {
            get { return _currentDateTime; }
            set
            {
                _currentDateTime = value;
                OnPropertyChanged();
            }
        }

        public int TemperatureWaitTime
        {
            get { return _temperatureWaitTime; }
            set
            {
                _temperatureWaitTime = value;
                OnPropertyChanged();
            }
        }

        public int BarometricPressureWaitTime
        {
            get { return _barometricPressureWaitTime; }
            set
            {
                _barometricPressureWaitTime = value;
                OnPropertyChanged();
            }
        }

        public string TemperatureLabel
        {
            get { return _temperatureLabel; }
            set
            {
                _temperatureLabel = value;
                OnPropertyChanged();
            }
        }

        public string BarometricPressureLabel
        {
            get { return _barometricPressureLabel; }
            set
            {
                _barometricPressureLabel = value;
                OnPropertyChanged();
            }
        }

        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        public string BarometricPressure
        {
            get { return _barPressure; }
            set
            {
                _barPressure = value;
                OnPropertyChanged();
            }
        }

        public BarometricPressureTrend BarometricPressureTrend
        {
            get { return _barometricPressureTrend; }
            set
            {
                _barometricPressureTrend = value;
                OnPropertyChanged();
            }
        }

        public string AverageTemperature
        {
            get { return _averageTemperature; }
            set
            {
                _averageTemperature = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenWindowCommand { get; }
        public ICommand CloseApplicationCommand { get; }
        public ICommand ReadTemperatureCommand { get; }
        public ICommand ReadBarometricPressureCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RegisterHandler(object handler)
        {
            if (_handler == null)
                _handler = new List<object>();
            _handler.Add(handler);
        }

        public void MeasurementUnitChanged(CurrentMeasurementUnit newMeasurementUnit)
        {
            _currenTemperatureUnit = newMeasurementUnit.Temperature;
            _currentBarometricPressureUnit = newMeasurementUnit.BarometricPressure;
            TemperatureLabel = _currenTemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F";
            BarometricPressureLabel = _currentBarometricPressureUnit == BarometricPressureUnit.MBar ? "mBar" : "inHg";
        }

        private void SubscribeForEvents()
        {
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(NewTemperatureMeasurement);
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewBarPressureMeasurement);
            _eventAggregator.GetEvent<NewAverageTemperature>().Subscribe(NewAverageTemperatureUpdate);
            _eventAggregator.GetEvent<NewBarometricPressureTrend>().Subscribe(BarometricPressureTrendUpdate);
            _eventAggregator.GetEvent<DateTimeChanged>().Subscribe(DateTimeUpdate);
            _eventAggregator.GetEvent<MeasurementUnitChanged>().Subscribe(MeasurementUnitChanged);
            _eventAggregator.GetEvent<TemperatureWaitTimeChanged>().Subscribe(TemperatureWaitTimeChanged);
            _eventAggregator.GetEvent<BarometricPressureWaitTimeChanged>().Subscribe(BarometricPressureWaitTimeChanged);
        }

        private void TemperatureWaitTimeChanged(int timeToWait)
        {
            TemperatureWaitTime = timeToWait;
        }

        private void BarometricPressureWaitTimeChanged(int timeToWait)
        {
            BarometricPressureWaitTime = timeToWait;
        }

        private void BarometricPressureTrendUpdate(BarometricPressureTrend barometricPressureTrend)
        {
            BarometricPressureTrend = barometricPressureTrend;
        }

        private void NewAverageTemperatureUpdate(AverageTemperature averageTemperature)
        {
            var newValue = averageTemperature.Value;
            if (_currenTemperatureUnit == TemperatureUnit.Fahrenheit)
                newValue = UnitConverter.IntoFahrenheit(newValue);
            AverageTemperature = newValue.ToString("F");
        }

        private void DateTimeUpdate(DateTime newTime)
        {
            CurrentDateTime = newTime;
        }

        private void NewTemperatureMeasurement(TemperatureMeasurement newMeasurement)
        {
            var newValue = newMeasurement.Value;
            if (_currenTemperatureUnit == TemperatureUnit.Fahrenheit)
                newValue = UnitConverter.IntoFahrenheit(newValue);
            Temperature = newValue.ToString("F");
        }

        private void NewBarPressureMeasurement(BarPressureMeasurement newMeasurement)
        {
            var newValue = newMeasurement.Value;
            if (_currentBarometricPressureUnit == BarometricPressureUnit.InHg)
                newValue = UnitConverter.IntoInHg(newValue);
            BarometricPressure = newValue.ToString("F");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}