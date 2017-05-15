using Prism.Events;
using WeatherStation.Commands;
using WeatherStation.Handler;
using WeatherStation.Model;
using WeatherStation.Properties;
using WeatherStation.Sensor;
using WeatherStation.Services;
using WeatherStation.Storage;
using WeatherStation.ViewModels;
using WeatherStation.Views;
using WeatherStation.Views.DateAndTime;
using WeatherStation.Views.History;
using WeatherStation.Views.Main;
using WeatherStation.Views.MeasurementIntervals;
using WeatherStation.Views.UnitSettings;

namespace WeatherStation.MVVM
{
    public class MvvmWindowFactory : IWindowFactory
    {
        private readonly IMeasurementsRepository<BarPressureMeasurement> _barometricPressureMeasurementsRepository;
        private readonly ISensor _barometricPressureSensor;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;
        private readonly IMeasurementsRepository<TemperatureMeasurement> _temperatuMeasurementsRepository;
        private readonly ISensor _temperatureSensor;

        public MvvmWindowFactory(
            IEventAggregator eventAggregator,
            ISensor temperatureSensor,
            ISensor barometricPressureSensor,
            IMeasurementsRepository<TemperatureMeasurement> temperatuMeasurementsRepository,
            IMeasurementsRepository<BarPressureMeasurement> barometricPressureMeasurementsRepository,
            ISettingsService settingsService)
        {
            _eventAggregator = eventAggregator;
            _temperatureSensor = temperatureSensor;
            _barometricPressureSensor = barometricPressureSensor;
            _temperatuMeasurementsRepository = temperatuMeasurementsRepository;
            _barometricPressureMeasurementsRepository = barometricPressureMeasurementsRepository;
            _settingsService = settingsService;
        }

        public IWindow CreateMainWindow()
        {
            var averageTemperatureCalculator = new AverageTemperatureCalculator(_eventAggregator);
            var barometricPressureTrendHandler = new BarometricPressureTrendHandler(_eventAggregator);

            var mainWindowViewModel = BuildMainWindowViewModel();

            mainWindowViewModel.RegisterHandler(averageTemperatureCalculator);
            mainWindowViewModel.RegisterHandler(barometricPressureTrendHandler);

            return new MainWindow {DataContext = mainWindowViewModel, Tag = WindowType.MainWindow};
        }

        public IWindow CreateTemperatureHistory()
        {
            var temperatureViewModel = new TemperatureHistoryWindowViewModel(_temperatuMeasurementsRepository,
                _eventAggregator);
            var historyWindow = new HistoryWindow
            {
                DataContext = temperatureViewModel,
                Tag = WindowType.TemperatureHistory
            };
            return historyWindow;
        }

        public IWindow CreateBarPressureHistory()
        {
            var barPressureViewModel = new BarPressureHistoryWindowViewModel(_barometricPressureMeasurementsRepository,
                _eventAggregator)
            {
                BarometricPressureChartViewModel =
                    new BarometricPressureChartViewModel(_eventAggregator, _settingsService)
            };
            var historyWindow = new HistoryWindow
            {
                DataContext = barPressureViewModel,
                Tag = WindowType.BarometricPressureHistory
            };
            return historyWindow;
        }

        public IWindow CreateUnitSettingsWindow()
        {
            var viewModel = new UnitSettingsWindowViewModel();
            viewModel.ApplySettingsCommand = new ApplyUnitSettingsCommand(viewModel, _eventAggregator);
            viewModel.CancelCommand = new CloseWindowCommand(_eventAggregator, WindowType.UnitSettings);
            var unitSettingsWindow = new UnitSettingsWindow
            {
                DataContext = viewModel,
                Tag = WindowType.UnitSettings
            };
            return unitSettingsWindow;
        }

        public IWindow CreateDateAndTimeSettingsWindow()
        {
            var viewModel = new DateAndTimeSettingsWindowViewModel
            {
                CloseWindowCommand =
                    new CloseWindowCommand(_eventAggregator, WindowType.DateAndTimeSettings)
            };
            viewModel.ApplySettingsCommand =
                new ApplyDateAndTimeSettingsCommand(_eventAggregator, viewModel, _settingsService);
            var dateAndTimeSettingsWindow = new DateAndTimeSettingsWindow
            {
                DataContext = viewModel,
                Tag = WindowType.DateAndTimeSettings
            };
            return dateAndTimeSettingsWindow;
        }

        public IWindow CreateMeasurementIntervalsSettingsWindow()
        {
            var viewModel = new MeasurementIntervalsSettingsWindowViewModel(_settingsService);
            viewModel.ApplySettingsCommand = new ApplyMeasurementIntervalSettingsCommand(_eventAggregator, _settingsService, viewModel);
            viewModel.CloseWindowCommand = new CloseWindowCommand(_eventAggregator, WindowType.MeasurementIntervalsSettings);
            var measurementIntervalsSettingsWindow = new MeasurementIntervalsSettingsWindow
            {
                DataContext = viewModel,
                Tag = WindowType.MeasurementIntervalsSettings
            };
            return measurementIntervalsSettingsWindow;
        }

        private MainWindowViewModel BuildMainWindowViewModel()
        {
            var viewModel = new MainWindowViewModel(
                _eventAggregator,
                new OpenWindowCommand(_eventAggregator),
                new CloseWindowCommand(_eventAggregator, WindowType.MainWindow),
                new ReadTemperatureCommand(_temperatureSensor),
                new ReadBarPressureCommand(_barometricPressureSensor));

            var initialMeasurementUnit = new CurrentMeasurementUnit
            {
                BarometricPressure = (BarometricPressureUnit) Settings.Default.BarometricPressureUnit,
                Temperature = (TemperatureUnit) Settings.Default.TemperatureUnit
            };
            viewModel.MeasurementUnitChanged(initialMeasurementUnit);
            return viewModel;
        }
    }
}