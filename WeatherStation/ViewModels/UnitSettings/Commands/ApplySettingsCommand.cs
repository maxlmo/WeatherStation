using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Properties;

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
            _eventAggregator.GetEvent<SettingsSaved>().Publish(null);
        }

        public event EventHandler CanExecuteChanged;
    }
}
