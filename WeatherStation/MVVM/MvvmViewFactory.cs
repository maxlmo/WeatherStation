using System.Windows;
using Prism.Events;
using WeatherStation.Sensor;
using WeatherStation.ViewModels;
using WeatherStation.ViewModels.Commands;
using WeatherStation.Views;

namespace WeatherStation.MVVM
{
    public class MvvmViewFactory : IViewFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISensor _sensor;

        public MvvmViewFactory(IEventAggregator eventAggregator, ISensor sensor)
        {
            _eventAggregator = eventAggregator;
            _sensor = sensor;
        }

        public Window CreateHistory()
        {
            var historyWindow = new HistoryWindow();
            return historyWindow;
        }

        public Window CreateMainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel(
                new OpenHistoryWindowCommand(this),
                new CloseApplicationCommand(),_eventAggregator, 
                new GetNewMeasurementCommand(_sensor));
            return new MainWindow {DataContext = mainWindowViewModel};
        }
    }
}