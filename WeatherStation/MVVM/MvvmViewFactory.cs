using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Sensor;
using WeatherStation.Storage;
using WeatherStation.ViewModels.History;
using WeatherStation.ViewModels.Main;
using WeatherStation.ViewModels.Main.Commands;
using WeatherStation.ViewModels.UnitSettings;
using WeatherStation.ViewModels.UnitSettings.Commands;
using WeatherStation.Views;
using WeatherStation.Views.History;
using WeatherStation.Views.Main;
using WeatherStation.Views.UnitSettings;

namespace WeatherStation.MVVM
{
    public class MvvmViewFactory : IViewFactory
    {
        private readonly ISensor _barometricPressureSensor;
        private readonly IDataBaseConnector _temperatuDataBaseConnector;
        private readonly IDataBaseConnector _barometricPressureDataBaseConnector;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISensor _temperatureSensor;

        public MvvmViewFactory(
            IEventAggregator eventAggregator, 
            ISensor temperatureSensor,
            ISensor barometricPressureSensor, 
            IDataBaseConnector temperatuDataBaseConnector, 
            IDataBaseConnector barometricPressureDataBaseConnector)
        {
            _eventAggregator = eventAggregator;
            _temperatureSensor = temperatureSensor;
            _barometricPressureSensor = barometricPressureSensor;
            _temperatuDataBaseConnector = temperatuDataBaseConnector;
            _barometricPressureDataBaseConnector = barometricPressureDataBaseConnector;
        }

        public IWindow CreateMainWindow()
        { 
            var averageTemperatureCalculator = new AverageTemperatureCalculator(_eventAggregator);
            var barometricPressureTrendHandler = new BarometricPressureTrendHandler(_eventAggregator);

            var mainWindowViewModel = new MainWindowViewModel(
                _eventAggregator,
                new OpenHistoryWindowCommand(_eventAggregator),
                new CloseApplicationCommand(_eventAggregator),
                new ReadTemperatureCommand(_temperatureSensor),
                new ReadBarPressureCommand(_barometricPressureSensor),
                new OpenUnitSettingsCommand(_eventAggregator));

            mainWindowViewModel.RegisterHandler(averageTemperatureCalculator);
            mainWindowViewModel.RegisterHandler(barometricPressureTrendHandler);

            return new MainWindow { DataContext = mainWindowViewModel, Tag = ViewType.MainWindow };
        }

        public IWindow CreateTemperatureHistory()
        {
            var temperatureViewModel = new TemperatureHistoryWindowViewModel( _temperatuDataBaseConnector, _eventAggregator);
            var historyWindow = new HistoryWindow {DataContext = temperatureViewModel, Tag = ViewType.TemperatureHistory};
            return historyWindow;
        }

        public IWindow CreateBarPressureHistory()
        {
            var barPressureViewModel = new BarPressureHistoryWindowViewModel( _barometricPressureDataBaseConnector, _eventAggregator);
            var historyWindow = new HistoryWindow {DataContext = barPressureViewModel, Tag = ViewType.BarometricPressureHistory};
            return historyWindow;
        }

        public IWindow CreateUnitSettingsWindow()
        {
            var unitSettingsWindow = new UnitSettingsWindow();
            var viewModel = new UnitSettingsWindowViewModel();
            viewModel.ApplySettingsCommand = new ApplySettingsCommand(viewModel, _eventAggregator);

            unitSettingsWindow.DataContext = viewModel;
            unitSettingsWindow.Tag = ViewType.UnitSettings;
            return unitSettingsWindow;
        }
    }
}