using System.Collections.ObjectModel;
using WeatherStation.Model;

namespace WeatherStation.Storage
{
    public interface IRepository
    {
        ObservableCollection<IMeasurement> Measurements { get; set; }
    }
}