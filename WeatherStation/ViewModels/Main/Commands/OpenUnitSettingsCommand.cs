using System;
using System.Windows.Input;
using WeatherStation.MVVM;

namespace WeatherStation.ViewModels.Main.Commands
{
    public class OpenUnitSettingsCommand : ICommand
    {
        private readonly IViewFactory _viewFactory;

        public OpenUnitSettingsCommand(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = _viewFactory.CreateUnitSettingsWindow();
            window.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}