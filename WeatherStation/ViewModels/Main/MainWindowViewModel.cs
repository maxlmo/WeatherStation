using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.ViewModels.UnitSettings;
using WeatherStation.Views.Converter;

namespace WeatherStation.ViewModels.Main
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private string _averageTemperature;
        private string _barometricPressureLabel;
        private BarometricPressureTrend _barometricPressureTrend;
        private string _barPressure;
        private BarometricPressureUnit _currentBarometricPressureUnit;
        private TemperatureUnit _currenTemperatureUnit;
        private string _date;
        private List<object> _handler;
        private string _temperature;
        private string _temperatureLabel;
        private string _time;

        public MainWindowViewModel(
            IEventAggregator eventAggregator,
            ICommand openHistoryWindowCommand,
            ICommand closeApplicationCommand,
            ICommand readTemperatureCommand,
            ICommand readBarometricPressureCommand,
            ICommand openUnitSettingsCommand)
        {
            _eventAggregator = eventAggregator;
            OpenUnitSettingsCommand = openUnitSettingsCommand;
            ReadBarometricPressureCommand = readBarometricPressureCommand;
            ReadTemperatureCommand = readTemperatureCommand;
            CloseApplicationCommand = closeApplicationCommand;
            OpenHistoryWindowCommand = openHistoryWindowCommand;
            OpenUnitSettingsCommand = openUnitSettingsCommand;
            SubscribeForEvents();
        }

        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
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

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
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

        public ICommand OpenHistoryWindowCommand { get; }
        public ICommand CloseApplicationCommand { get; }
        public ICommand ReadTemperatureCommand { get; }
        public ICommand ReadBarometricPressureCommand { get; }
        public ICommand OpenUnitSettingsCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RegisterHandler(object handler)
        {
            if (_handler == null)
            {
                _handler = new List<object>();
            }
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
            _eventAggregator.GetEvent<NewDateTime>().Subscribe(DateTimeUpdate);
            _eventAggregator.GetEvent<MeasurementUnitChanged>().Subscribe(MeasurementUnitChanged);
        }

        private void BarometricPressureTrendUpdate(BarometricPressureTrend barometricPressureTrend)
        {
            BarometricPressureTrend = barometricPressureTrend;
        }

        private void NewAverageTemperatureUpdate(AverageTemperature averageTemperature)
        {
            var newValue = averageTemperature.Value;
            if (_currenTemperatureUnit == TemperatureUnit.Fahrenheit)
            {
                newValue = UnitConverter.IntoFahrenheit(newValue);
            }
            AverageTemperature = newValue.ToString("F");
        }

        private void DateTimeUpdate(CurrentDateTime newTime)
        {
            Date = newTime.Current.ToString("d");
            Time = newTime.Current.ToString("T");
        }

        private void NewTemperatureMeasurement(TemperatureMeasurement newMeasurement)
        {
            var newValue = newMeasurement.Value;
            if (_currenTemperatureUnit == TemperatureUnit.Fahrenheit)
            {
                newValue = UnitConverter.IntoFahrenheit(newValue);
            }
            Temperature = newValue.ToString("F");
        }

        private void NewBarPressureMeasurement(BarPressureMeasurement newMeasurement)
        {
            var newValue = newMeasurement.Value;
            if (_currentBarometricPressureUnit == BarometricPressureUnit.InHg)
            {
                newValue = UnitConverter.IntoInHg(newValue);
            }
            BarometricPressure = newValue.ToString("F");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}