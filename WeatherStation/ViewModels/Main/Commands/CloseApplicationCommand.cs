using System;
using System.Windows;
using System.Windows.Input;

namespace WeatherStation.ViewModels.Main.Commands
{
    public class CloseApplicationCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.MainWindow.Close();
        }

        public event EventHandler CanExecuteChanged;
    }
}
