using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.MVVM;
using WeatherStation.Services;
using WeatherStation.ViewModels;

namespace WeatherStation.Commands
{
    public class ApplyMeasurementIntervalSettingsCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;
        private readonly MeasurementIntervalsSettingsWindowViewModel _viewModel;

        public ApplyMeasurementIntervalSettingsCommand(IEventAggregator eventAggregator,ISettingsService settingsService,
            MeasurementIntervalsSettingsWindowViewModel viewModel)
        {
            _eventAggregator = eventAggregator;
            _settingsService = settingsService;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var newSettings = new MeasurementIntervalsSettings(_viewModel.TemperatureMeasurementInterval,
                _viewModel.BarometricPressureMeasurementInterval);
            _settingsService.SaveMeasurementIntervals(newSettings);
            _eventAggregator.GetEvent<MeasurementIntervalChanged>().Publish(newSettings);
            _eventAggregator.GetEvent<CloseWindow>().Publish(WindowType.MeasurementIntervalsSettings);
        }

        public event EventHandler CanExecuteChanged;
    }
}