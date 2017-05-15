using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public class BarometricPressureMeasurementsRepository : IMeasurementsRepository<BarPressureMeasurement>
    {
        private readonly IEventAggregator _eventAggregator;

        public BarometricPressureMeasurementsRepository(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void SaveMeasurement(BarPressureMeasurement newMeasurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(newMeasurement);
                transaction.Commit();
            }
            _eventAggregator.GetEvent<BarometricPressureSaved>().Publish(newMeasurement);
        }

        public IEnumerable<BarPressureMeasurement> GetSavedMeasurements()
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                return session.Query<BarPressureMeasurement>().ToList();
            }
        }
    }
}