using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.MVVM;

namespace WeatherStation.ViewModels.Main.Commands
{
    public class OpenHistoryWindowCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;

        public OpenHistoryWindowCommand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var type = (ViewType) Enum.Parse(typeof(ViewType), parameter.ToString());
            _eventAggregator.GetEvent<OpenNewWindow>().Publish(type);
        }

        public event EventHandler CanExecuteChanged;
    }
}