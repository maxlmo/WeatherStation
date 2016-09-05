﻿using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Sensor;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly ISensor _temperatureSensor;

        public MvvmViewFactory(IEventAggregator eventAggregator, ISensor temperatureSensor,
            ISensor barometricPressureSensor)
        {
            _eventAggregator = eventAggregator;
            _temperatureSensor = temperatureSensor;
            _barometricPressureSensor = barometricPressureSensor;
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

            return new MainWindow { DataContext = mainWindowViewModel };
        }

        public Window CreateTemperatureHistory()
        {
            var temperatureViewModel = new TemperatureHistoryWindowViewModel(_eventAggregator);
            var historyWindow = new HistoryWindow {DataContext = temperatureViewModel};
            return historyWindow;
        }

        public Window CreateBarPressureHistory()
        {
            var barPressureViewModel = new BarPressureHistoryWindowViewModel(_eventAggregator);
            var historyWindow = new HistoryWindow {DataContext = barPressureViewModel};
            return historyWindow;
        }
    }
}