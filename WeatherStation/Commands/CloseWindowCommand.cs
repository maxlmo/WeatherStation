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
        private readonly WindowType _windowType;

        public CloseWindowCommand(IEventAggregator eventAggregator, WindowType windowType)
        {
            _eventAggregator = eventAggregator;
            _windowType = windowType;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _eventAggregator.GetEvent<CloseWindow>().Publish(_windowType);
        }

        public event EventHandler CanExecuteChanged;
    }
}