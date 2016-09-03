using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private string _barPressure;
        private string _date;
        private string _temperature;
        private string _time;

        public MainWindowViewModel(
            ICommand openHistoryWindowCommand,
            ICommand closeApplicationCommand,
            IEventAggregator eventAggregator,
            ICommand getNewMeasurement)
        {
            _eventAggregator = eventAggregator;
            GetNewMeasurementCommand = getNewMeasurement;
            CloseApplicationCommand = closeApplicationCommand;
            OpenHistoryWindowCommand = openHistoryWindowCommand;
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(NewTemperatureMeasurement);
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewBarPressureMeasurement);
            _eventAggregator.GetEvent<NewDateTime>().Subscribe(DateTimeUpdate);
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

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenHistoryWindowCommand { get; }
        public ICommand CloseApplicationCommand { get; }
        public ICommand GetNewMeasurementCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void DateTimeUpdate(CurrentDateTime newTime)
        {
            Date = newTime.Current.ToString("d");
            Time = newTime.Current.ToString("T");
        }

        private void NewTemperatureMeasurement(TemperatureMeasurement newMeasurement)
        {
            Temperature = newMeasurement.Value.ToString("F");
        }

        private void NewBarPressureMeasurement(BarPressureMeasurement newMeasurement)
        {
            BarometricPressure = newMeasurement.Value.ToString("F");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}