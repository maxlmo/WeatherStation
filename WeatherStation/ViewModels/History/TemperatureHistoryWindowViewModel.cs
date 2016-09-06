using WeatherStation.Properties;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels.History
{
    public class TemperatureHistoryWindowViewModel : HistoryWindowViewModel
    {
        public TemperatureHistoryWindowViewModel(IRepository repository) : base(repository)
        {
        }

        public string HistoryWindowName
        {
            get { return Resources.TemperatureHistoryWindowName; }
        }
    }
}