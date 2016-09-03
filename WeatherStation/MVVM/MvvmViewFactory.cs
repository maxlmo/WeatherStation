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
        private readonly ISensor _temperatureSensor;
        private readonly ISensor _barometricPressureSensor;

        public MvvmViewFactory(IEventAggregator eventAggregator, ISensor temperatureSensor, ISensor barometricPressureSensor)
        {
            _eventAggregator = eventAggregator;
            _temperatureSensor = temperatureSensor;
            _barometricPressureSensor = barometricPressureSensor;
        }

        public Window CreateHistory()
        {
            var historyWindow = new HistoryWindow();
            return historyWindow;
        }

        public Window CreateMainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel(
                _eventAggregator,
                new OpenHistoryWindowCommand(this),
                new CloseApplicationCommand(), 
                new ReadTemperatureCommand(_temperatureSensor),
                new ReadBarPressureCommand(_barometricPressureSensor));
            return new MainWindow {DataContext = mainWindowViewModel};
        }
    }
}