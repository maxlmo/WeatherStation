using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public class NHibernateTemperatureRepository : INHibernateRepository
    {
        public List<TemperatureMeasurement> GetTemperatureMeasurements()
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                return session.Query<TemperatureMeasurement>().ToList();
            }
        }

        public List<BarPressureMeasurement> GetBarPressureMeasurements()
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {
                return session.Query<BarPressureMeasurement>().ToList();
            }
        }

        public void WriteTemperature(IMeasurement measurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(measurement);
                transaction.Commit();
            }
        }

        public void WriteBarometricPressure(IMeasurement measurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(measurement);
                transaction.Commit();
            }
        }

        public void Save(TemperatureMeasurement temperatureMeasurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(temperatureMeasurement);
                transaction.Commit();
            }
        }
    }
}