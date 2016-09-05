using System;
using System.Windows.Input;
using WeatherStation.MVVM;

namespace WeatherStation.ViewModels.Main.Commands
{
    public class OpenHistoryWindowCommand : ICommand
    {
        private readonly IViewFactory _viewFactory;

        public OpenHistoryWindowCommand(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var view = _viewFactory.CreateHistory();
            view.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}