using System.Collections.ObjectModel;
using System.Windows;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.ViewModels.History
{
    public class TemperatureHistoryWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public TemperatureHistoryWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(NewMeasurement);
            Measurements = new ObservableCollection<IMeasurement>();
        }

        public ObservableCollection<IMeasurement> Measurements { get; set; }

        private void NewMeasurement(TemperatureMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Measurements.Add(newMeasurement);
            });
        }
    }
}