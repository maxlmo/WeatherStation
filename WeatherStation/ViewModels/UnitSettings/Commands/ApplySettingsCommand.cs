using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.MVVM;
using WeatherStation.Properties;
using WeatherStation.ViewModels.Main;

namespace WeatherStation.ViewModels.UnitSettings.Commands
{
    class ApplySettingsCommand : ICommand
    {
        private readonly UnitSettingsWindowViewModel _viewModel;
        private readonly IEventAggregator _eventAggregator;

        public ApplySettingsCommand(UnitSettingsWindowViewModel viewModel, IEventAggregator eventAggregator)
        {
            _viewModel = viewModel;
            _eventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Settings.Default.BarometricPressureUnit = (int)_viewModel.CurrentBarometricPressureUnit;
            Settings.Default.TemperatureUnit = (int) _viewModel.CurrentTemperatureUnit;
            _eventAggregator.GetEvent<MeasurementUnitChanged>().Publish(new CurrentMeasurementUnit
            {
                Temperature = _viewModel.CurrentTemperatureUnit,
                BarometricPressure = _viewModel.CurrentBarometricPressureUnit
            });
            _eventAggregator.GetEvent<CloseWindow>().Publish(ViewType.UnitSettings);
        }

        public event EventHandler CanExecuteChanged;
    }
}
