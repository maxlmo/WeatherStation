using System.Collections.Generic;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public interface IMeasurementsRepository
    {
        IEnumerable<IMeasurement> GetSavedMeasurements();

        void SaveMeasurement(IMeasurement measurement);
    }
}