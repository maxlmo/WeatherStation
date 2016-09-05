using System.Collections.ObjectModel;
using System.Windows;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Properties;

namespace WeatherStation.ViewModels.History
{
    public class BarPressureHistoryWindowViewModel : HistoryWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public string HistoryWindowName
        {
            get { return Resources.BarometricPressureHistoryWindowName; }
        }

        public BarPressureHistoryWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewMeasurement);
            Measurements = new ObservableCollection<IMeasurement>();
        }
        
        private void NewMeasurement(BarPressureMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Measurements.Add(newMeasurement);
            });
        }
    }
}