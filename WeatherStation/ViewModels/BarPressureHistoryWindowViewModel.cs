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
        public BarPressureHistoryWindowViewModel(IMeasurementsRepository<BarPressureMeasurement> measurementsRepository,
            IEventAggregator eventAggregator)
        {
            Measurements = new ObservableCollection<BarPressureMeasurement>();

            measurementsRepository.GetSavedMeasurements().ForEach(m => Measurements.Add(m));
            eventAggregator.GetEvent<BarometricPressureSaved>().Subscribe(NewBarPressure);
            BarometricPressureChartViewModel = new BarometricPressureChartViewModel(measurementsRepository, eventAggregator);
        }

        public override string HistoryWindowName => Resources.BarometricPressureHistoryWindowName;

        public ObservableCollection<BarPressureMeasurement> Measurements { get; set; }

        public BarometricPressureChartViewModel BarometricPressureChartViewModel { get; set; }

        private void NewBarPressure(IMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Measurements.Add((BarPressureMeasurement) newMeasurement);
            });
        }
    }
}