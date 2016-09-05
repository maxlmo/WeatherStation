using System.Collections.ObjectModel;
using System.Windows;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Properties;

namespace WeatherStation.ViewModels.History
{
    public class TemperatureHistoryWindowViewModel : HistoryWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public string HistoryWindowName
        {
            get { return Resources.TemperatureHistoryWindowName; }
        }

        public TemperatureHistoryWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(NewMeasurement);
            Measurements = new ObservableCollection<IMeasurement>();
        }

        private void NewMeasurement(TemperatureMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Measurements.Add(newMeasurement);
            });
        }
    }
}