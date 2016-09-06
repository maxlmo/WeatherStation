using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Sensor;
using WeatherStation.Storage;
using WeatherStation.Storage.Repository;
using WeatherStation.ViewModels.History;
using WeatherStation.ViewModels.Main;
using WeatherStation.ViewModels.Main.Commands;
using WeatherStation.Views.History;
using WeatherStation.Views.Main;

namespace WeatherStation.MVVM
{
    public class MvvmViewFactory : IViewFactory
    {
        private readonly ISensor _barometricPressureSensor;
        private readonly IRepository _temperatuRepository;
        private readonly IRepository _barometricPressureRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISensor _temperatureSensor;
        private TemperatureRepository _repository;

        public MvvmViewFactory(
            IEventAggregator eventAggregator, 
            ISensor temperatureSensor,
            ISensor barometricPressureSensor, 
            IRepository temperatuRepository, 
            IRepository barometricPressureRepository)
        {
            _eventAggregator = eventAggregator;
            _temperatureSensor = temperatureSensor;
            _barometricPressureSensor = barometricPressureSensor;
            _temperatuRepository = temperatuRepository;
            _barometricPressureRepository = barometricPressureRepository;
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
            mainWindowViewModel.RegisterHandler(_temperatuRepository);
            mainWindowViewModel.RegisterHandler(_barometricPressureRepository);

            return new MainWindow { DataContext = mainWindowViewModel };
        }

        public Window CreateTemperatureHistory()
        {
            var temperatureViewModel = new TemperatureHistoryWindowViewModel( _temperatuRepository);
            var historyWindow = new HistoryWindow {DataContext = temperatureViewModel};
            return historyWindow;
        }

        public Window CreateBarPressureHistory()
        {
            var barPressureViewModel = new BarPressureHistoryWindowViewModel( _barometricPressureRepository);
            var historyWindow = new HistoryWindow {DataContext = barPressureViewModel};
            return historyWindow;
        }
    }
}