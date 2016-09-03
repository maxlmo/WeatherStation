using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Messages;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private double _measurementValue = 0;

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
            _eventAggregator.GetEvent<NewMeasurement>().Subscribe(ReceivedMeasurement);
        }

        private void ReceivedMeasurement(Measurement newMeasurement)
        {
            MeasurementValue = newMeasurement.Value;
        }

        public double MeasurementValue
        {
            get
            {
                return _measurementValue;
            }
            set
            {
                _measurementValue = value;
                OnPropertyChanged("MeasurementValue");
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