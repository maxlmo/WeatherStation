using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public class BarometricPressureMeasurementsRepository : IMeasurementsRepository
    {
        private readonly IEventAggregator _eventAggregator;

        public BarometricPressureMeasurementsRepository(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void SaveMeasurement(IMeasurement newMeasurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(newMeasurement);
                transaction.Commit();
            }
            _eventAggregator.GetEvent<BarometricPressureSaved>().Publish(newMeasurement);
        }

        public IEnumerable<IMeasurement> GetSavedMeasurements()
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                return session.Query<BarPressureMeasurement>().ToList();
            }
        }
    }
}