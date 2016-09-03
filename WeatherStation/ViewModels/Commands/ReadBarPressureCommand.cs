using System;
using System.Windows.Input;
using WeatherStation.Sensor;

namespace WeatherStation.ViewModels.Commands
{
    public class ReadBarPressureCommand : ICommand
    {
        private readonly ISensor _sensor;

        public ReadBarPressureCommand(ISensor sensor)
        {
            _sensor = sensor;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _sensor.ReadMeasurement();
        }

        public event EventHandler CanExecuteChanged;
    }
}