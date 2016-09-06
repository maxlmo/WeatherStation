using System.Windows;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Properties;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels.History
{
    public class BarPressureHistoryWindowViewModel : HistoryWindowViewModel
    {
        public string HistoryWindowName
        {
            get { return Resources.BarometricPressureHistoryWindowName; }
        }

        public BarPressureHistoryWindowViewModel(IRepository repository) : base(repository)
        {
        }
    }
}