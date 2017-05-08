using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public interface IDataBaseConnector
    {
        IEnumerable<IMeasurement> GetSavedMeasurements();

        void SaveMeasurement(IMeasurement measurement);
    }
}