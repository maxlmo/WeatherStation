using System.Windows;
using WeatherStation.ViewModels;
using WeatherStation.ViewModels.Commands;
using WeatherStation.Views;

namespace WeatherStation.MVVM
{
    public class MvvmViewFactory : IViewFactory
    {
        public Window CreateHistory()
        {
            var historyWindow = new HistoryWindow();
            return historyWindow;
        }

        public Window CreateMainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel(new OpenHistoryWindowCommand(this));
            return new MainWindow {DataContext = mainWindowViewModel};
        }
    }
}