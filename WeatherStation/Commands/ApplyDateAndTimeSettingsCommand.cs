using System;
using System.Windows.Input;
using Prism.Events;

namespace WeatherStation.Commands
{
    public class ApplyDateAndTimeSettingsCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;

        public ApplyDateAndTimeSettingsCommand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
