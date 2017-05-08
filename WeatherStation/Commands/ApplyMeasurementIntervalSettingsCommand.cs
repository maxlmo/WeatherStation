using System;
using System.Windows.Input;
using WeatherStation.Services;
using WeatherStation.ViewModels;

namespace WeatherStation.Commands
{
    public class ApplyMeasurementIntervalSettingsCommand : ICommand
    {
        private readonly ISettingsService _settingsService;
        private readonly MeasurementIntervalsSettingsWindowViewModel _viewModel;

        public ApplyMeasurementIntervalSettingsCommand(ISettingsService settingsService,
            MeasurementIntervalsSettingsWindowViewModel viewModel)
        {
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
        }

        public event EventHandler CanExecuteChanged;
    }
}