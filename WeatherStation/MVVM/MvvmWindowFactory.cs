﻿using Prism.Events;
using WeatherStation.Commands;
using WeatherStation.Handler;
using WeatherStation.Properties;
using WeatherStation.Sensor;
using WeatherStation.Services;
using WeatherStation.Storage;
using WeatherStation.ViewModels;
using WeatherStation.Views;
using WeatherStation.Views.DateAndTime;
using WeatherStation.Views.History;
using WeatherStation.Views.Main;
using WeatherStation.Views.UnitSettings;

namespace WeatherStation.MVVM
{
    public class MvvmWindowFactory : IWindowFactory
    {
        private readonly IDataBaseConnector _barometricPressureDataBaseConnector;
        private readonly ISensor _barometricPressureSensor;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;
        private readonly IDataBaseConnector _temperatuDataBaseConnector;
        private readonly ISensor _temperatureSensor;

        public MvvmWindowFactory(
            IEventAggregator eventAggregator,
            ISensor temperatureSensor,
            ISensor barometricPressureSensor,
            IDataBaseConnector temperatuDataBaseConnector,
            IDataBaseConnector barometricPressureDataBaseConnector,
            ISettingsService settingsService)
        {
            _eventAggregator = eventAggregator;
            _temperatureSensor = temperatureSensor;
            _barometricPressureSensor = barometricPressureSensor;
            _temperatuDataBaseConnector = temperatuDataBaseConnector;
            _barometricPressureDataBaseConnector = barometricPressureDataBaseConnector;
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
            var temperatureViewModel = new TemperatureHistoryWindowViewModel(_temperatuDataBaseConnector,
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
            var barPressureViewModel = new BarPressureHistoryWindowViewModel(_barometricPressureDataBaseConnector,
                _eventAggregator);
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