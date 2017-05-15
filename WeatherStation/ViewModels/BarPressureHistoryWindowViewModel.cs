using System.Collections.ObjectModel;
using System.Windows;
using NHibernate.Util;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Properties;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels
{
    public class BarPressureHistoryWindowViewModel : HistoryWindowViewModel
    {

        public BarPressureHistoryWindowViewModel(IMeasurementsRepository measurementsRepository, IEventAggregator eventAggregator) : base(measurementsRepository, eventAggregator)
        {
            Measurements = new ObservableCollection<BarPressureMeasurement>();
            measurementsRepository.GetSavedMeasurements().ForEach(m => Measurements.Add((BarPressureMeasurement)m));
            eventAggregator.GetEvent<BarometricPressureSaved>().Subscribe(NewBarPressure);
        }

        private void NewBarPressure(IMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate { Measurements.Add((BarPressureMeasurement) newMeasurement); });
        }

        public string HistoryWindowName
        {
            get { return Resources.BarometricPressureHistoryWindowName; }
        }

        public ObservableCollection<BarPressureMeasurement> Measurements { get; set; }

        
    }
}