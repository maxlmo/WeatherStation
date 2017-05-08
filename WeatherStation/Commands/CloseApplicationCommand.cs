using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.MVVM;

namespace WeatherStation.Commands
{
    public class CloseApplicationCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;

        public CloseApplicationCommand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _eventAggregator.GetEvent<CloseWindow>().Publish(WindowType.MainWindow);
        }

        public event EventHandler CanExecuteChanged;
    }
}
