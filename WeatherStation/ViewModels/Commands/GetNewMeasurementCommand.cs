using System;
using System.Windows.Input;
using WeatherStation.Sensor;

namespace WeatherStation.ViewModels.Commands
{
    public class GetNewMeasurementCommand : ICommand
    {
        private readonly ISensor _sensor;

        public GetNewMeasurementCommand(ISensor sensor)
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