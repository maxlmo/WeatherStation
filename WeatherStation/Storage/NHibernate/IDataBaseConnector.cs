using System.Collections.Generic;
using WeatherStation.Model;

namespace WeatherStation.Storage.Repository
{
    public interface IDataBaseConnector
    {
        List<TemperatureMeasurement> GetTemperatureMeasurements();
        List<BarPressureMeasurement> GetBarPressureMeasurements();
        void WriteMeasurementIntoDataBase(IMeasurement measurement);
    }
}