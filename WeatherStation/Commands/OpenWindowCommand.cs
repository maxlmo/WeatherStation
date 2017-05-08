using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.MVVM;

namespace WeatherStation.Commands
{
    public class OpenWindowCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;

        public OpenWindowCommand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var type = (WindowType) Enum.Parse(typeof(WindowType), parameter.ToString());
            _eventAggregator.GetEvent<OpenNewWindow>().Publish(type);
        }

        public event EventHandler CanExecuteChanged;
    }
}