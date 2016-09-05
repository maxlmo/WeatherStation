using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Sensor;
using WeatherStation.ViewModels;
using WeatherStation.ViewModels.Commands;
using WeatherStation.ViewModels.History;
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
            var historyWindowViewModel = new HistoryWindowViewModel(_eventAggregator);
            var historyWindow = new HistoryWindow{DataContext = historyWindowViewModel};
            return historyWindow;
        }

        public Window CreateMainWindow()
        {
            var averageTemperatureCalculator = new AverageTemperatureCalculator(_eventAggregator);
            var barometricPressureTrendHandler = new BarometricPressureTrendHandler(_eventAggregator);

            var mainWindowViewModel = new MainWindowViewModel(
                _eventAggregator,
                new OpenHistoryWindowCommand(this),
                new CloseApplicationCommand(), 
                new ReadTemperatureCommand(_temperatureSensor),
                new ReadBarPressureCommand(_barometricPressureSensor));

            mainWindowViewModel.RegisterHandler(averageTemperatureCalculator);
            mainWindowViewModel.RegisterHandler(barometricPressureTrendHandler);

            return new MainWindow {DataContext = mainWindowViewModel};
        }
    }
}