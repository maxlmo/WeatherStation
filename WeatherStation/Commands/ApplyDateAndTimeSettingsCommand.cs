using System;
using System.Globalization;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.MVVM;
using WeatherStation.Services;
using WeatherStation.ViewModels;

namespace WeatherStation.Commands
{
    public class ApplyDateAndTimeSettingsCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;
        private readonly DateAndTimeSettingsWindowViewModel _viewModel;

        public ApplyDateAndTimeSettingsCommand(IEventAggregator eventAggregator,
            DateAndTimeSettingsWindowViewModel viewModel, ISettingsService settingsService)
        {
            _eventAggregator = eventAggregator;
            _viewModel = viewModel;
            _settingsService = settingsService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var formats = new[] {"dd.MM.yyyy H:mm", "dd.MM.yyyy HH:mm"};
            var newDateTime = DateTime.ParseExact($"{_viewModel.CurrentDate} {_viewModel.CurrentTime}", formats,
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            var timeSpan = newDateTime - DateTime.Now;
            _settingsService.SaveDateTimeOffset(timeSpan);
            _eventAggregator.GetEvent<DateTimeOffsetChanged>().Publish(timeSpan);
            _eventAggregator.GetEvent<CloseWindow>().Publish(WindowType.DateAndTimeSettings);
        }

        public event EventHandler CanExecuteChanged;
    }
}