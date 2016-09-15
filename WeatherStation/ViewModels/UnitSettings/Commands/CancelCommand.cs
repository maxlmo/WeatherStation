using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.MVVM;

namespace WeatherStation.ViewModels.UnitSettings.Commands
{
    public class CancelCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;

        public CancelCommand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _eventAggregator.GetEvent<CloseWindow>().Publish(ViewType.UnitSettings);
        }

        public event EventHandler CanExecuteChanged;
    }
}