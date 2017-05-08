using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.MVVM;

namespace WeatherStation.Commands
{
    public class CloseWindowCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly WindowType _windowToClose;

        public CloseWindowCommand(IEventAggregator eventAggregator, WindowType windowToClose)
        {
            _eventAggregator = eventAggregator;
            _windowToClose = windowToClose;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _eventAggregator.GetEvent<CloseWindow>().Publish(_windowToClose);
        }

        public event EventHandler CanExecuteChanged;
    }
}