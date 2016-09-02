using System.Windows.Input;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ICommand openHistoryWindowCommand)
        {
            OpenHistoryWindowCommand = openHistoryWindowCommand;
        }

        public ICommand OpenHistoryWindowCommand { get; }
    }
}