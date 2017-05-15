using System.Collections.Generic;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public interface IMeasurementsRepository<TMeasurement> where TMeasurement: IMeasurement
    {
        IEnumerable<TMeasurement> GetSavedMeasurements();

        void SaveMeasurement(TMeasurement measurement);
    }
}