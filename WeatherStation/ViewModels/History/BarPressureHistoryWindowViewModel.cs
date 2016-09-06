using WeatherStation.Properties;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels.History
{
    public class BarPressureHistoryWindowViewModel : HistoryWindowViewModel
    {
        public BarPressureHistoryWindowViewModel(IRepository repository) : base(repository)
        {
        }

        public string HistoryWindowName
        {
            get { return Resources.BarometricPressureHistoryWindowName; }
        }
    }
}