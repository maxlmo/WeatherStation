using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public class TemperatureRepository : IRepository, IHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INHibernateRepository _nHibernateRepository;

        public TemperatureRepository(IEventAggregator eventAggregator, INHibernateRepository nHibernateRepository)
        {
            _eventAggregator = eventAggregator;
            _nHibernateRepository = nHibernateRepository;
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(UpdateMeasurements);
            Measurements = new ObservableCollection<IMeasurement>();
            nHibernateRepository.GetTemperatureMeasurements().ForEach(m => Measurements.Add(m));
        }

        private void UpdateMeasurements(IMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate { Measurements.Add(newMeasurement); });
            _nHibernateRepository.WriteTemperature(newMeasurement);
        }

        public ObservableCollection<IMeasurement> Measurements { get; set; }
    }

    public interface IRepository
    {
        ObservableCollection<IMeasurement> Measurements { get; set; }
    }
}
