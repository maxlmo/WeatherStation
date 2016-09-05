using System.Collections.ObjectModel;
using System.Windows;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.ViewModels.History
{
    public class BarPressureHistoryWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public BarPressureHistoryWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewMeasurement);
            Measurements = new ObservableCollection<IMeasurement>();
        }

        public ObservableCollection<IMeasurement> Measurements { get; set; }

        private void NewMeasurement(BarPressureMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Measurements.Add(newMeasurement);
            });
        }
    }
}