using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Storage.Repository;

namespace WeatherStation.Storage
{
    public class BarometricPressureRepository : IRepository, IHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataBaseConnector _dataBaseConnector;

        public BarometricPressureRepository(IEventAggregator eventAggregator, IDataBaseConnector dataBaseConnector)
        {
            _eventAggregator = eventAggregator;
            _dataBaseConnector = dataBaseConnector;
            Measurements = new ObservableCollection<IMeasurement>();
            dataBaseConnector.GetBarPressureMeasurements().ForEach(m => Measurements.Add(m));
            _eventAggregator.GetEvent<NewBarPressure>().Subscribe(UpdateMeasurements);
        }

        private void UpdateMeasurements(IMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate { Measurements.Add(newMeasurement); });
            _dataBaseConnector.WriteMeasurementIntoDataBase(newMeasurement);
        }

        public ObservableCollection<IMeasurement> Measurements { get; set; }
    }
}
