using System.Windows.Input;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ICommand openHistoryWindowCommand, ICommand closeApplicationCommand)
        {
            CloseApplicationCommand = closeApplicationCommand;
            OpenHistoryWindowCommand = openHistoryWindowCommand;
        }

        public ICommand OpenHistoryWindowCommand { get; }
        public ICommand CloseApplicationCommand { get; }
    }
}