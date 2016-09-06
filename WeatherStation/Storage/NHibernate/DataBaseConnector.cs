using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using WeatherStation.Model;

namespace WeatherStation.Storage.Repository
{
    public class DataBaseConnector : IDataBaseConnector
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

        public void WriteMeasurementIntoDataBase(IMeasurement measurement)
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(measurement);
                transaction.Commit();
            }
        }
    }
}