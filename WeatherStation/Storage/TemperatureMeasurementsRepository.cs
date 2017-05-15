using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public class TemperatureMeasurementsRepository : IMeasurementsRepository<TemperatureMeasurement>
    {
        private readonly IEventAggregator _eventAggregator;

        public TemperatureMeasurementsRepository(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void SaveMeasurement(TemperatureMeasurement newMeasurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(newMeasurement);
                transaction.Commit();
            }
            _eventAggregator.GetEvent<TemperatureSaved>().Publish(newMeasurement);
        }

        public IEnumerable<TemperatureMeasurement> GetSavedMeasurements()
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                return session.Query<TemperatureMeasurement>().ToList();
            }
        }
    }
}