using System;
using System.Collections.ObjectModel;
using System.Windows;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.ViewModels
{
    public class HistoryWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public HistoryWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewMeasurement);
            Measurements = new ObservableCollection<HistoryMeasurement>();
        }

        public ObservableCollection<HistoryMeasurement> Measurements { get; set; }

        private void NewMeasurement(BarPressureMeasurement newMeasurement)
        {

            Application.Current.Dispatcher.Invoke(delegate
            {
                var historyMeasurement = new HistoryMeasurement
                {
                    Value = newMeasurement.Value,
                    TimeStamp = newMeasurement.TimeStamp
                };
                Measurements.Add(historyMeasurement);
            });
        }
    }

    public class HistoryMeasurement : IMeasurement
    {
        public string RoundedValue => Value.ToString("F");
        public string Date => TimeStamp.ToString("d");
        public string Time => TimeStamp.ToString("T");
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}