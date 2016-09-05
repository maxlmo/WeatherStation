using System;
using System.Management.Instrumentation;
using System.Windows;
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
            Window view;
            switch (parameter.ToString())
            {
                case "Temperature":
                    view = _viewFactory.CreateTemperatureHistory();
                    break;
                case "BarometricPressure":
                    view = _viewFactory.CreateBarPressureHistory();
                    break;
                default:
                    throw new InstanceNotFoundException("Window not found: " + parameter);
            }
            
            view.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}