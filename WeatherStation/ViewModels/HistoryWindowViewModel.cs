using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.ViewModels
{
    public class HistoryWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<IMeasurement> Measurements { get; set; }

        public HistoryWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewMeasurement);
            Measurements = new ObservableCollection<IMeasurement>
            {
                new TemperatureMeasurement {Value = 123}
            };
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