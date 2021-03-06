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
    public class TemperatureHistoryWindowViewModel : HistoryWindowViewModel
    {

        public TemperatureHistoryWindowViewModel(IDataBaseConnector dataBaseConnector, IEventAggregator eventAggregator) : base(dataBaseConnector,eventAggregator)
        {
            Measurements = new ObservableCollection<TemperatureMeasurement>();
            dataBaseConnector.GetSavedMeasurements().ForEach(m => Measurements.Add((TemperatureMeasurement)m));
            eventAggregator.GetEvent<TemperatureSaved>().Subscribe(NewTemperature);
        }

        private void NewTemperature(IMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate { Measurements.Add((TemperatureMeasurement)newMeasurement); });
        }

        public string HistoryWindowName
        {
            get { return Resources.TemperatureHistoryWindowName; }
        }

        public ObservableCollection<TemperatureMeasurement> Measurements { get; set; }
    }
}