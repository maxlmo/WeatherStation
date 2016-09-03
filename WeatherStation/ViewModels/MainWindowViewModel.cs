using System;
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
        private double _temperature;

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
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(ReceivedMeasurement);
        }

        private void ReceivedMeasurement(TemperatureMeasurement newMeasurement)
        {
            Temperature = newMeasurement.Value;
        }

        public double Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenHistoryWindowCommand { get; }
        public ICommand CloseApplicationCommand { get; }
        public ICommand GetNewMeasurementCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}