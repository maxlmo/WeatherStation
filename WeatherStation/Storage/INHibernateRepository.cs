using System.Collections.Generic;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public interface INHibernateRepository
    {
        List<TemperatureMeasurement> GetTemperatureMeasurements();
        List<BarPressureMeasurement> GetBarPressureMeasurements();
        void WriteTemperature(IMeasurement measurement);
        void WriteBarometricPressure(IMeasurement measurement);
    }
}