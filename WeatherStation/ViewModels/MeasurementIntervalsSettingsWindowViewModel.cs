using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherStation.Services;

namespace WeatherStation.ViewModels
{
    public class MeasurementIntervalsSettingsWindowViewModel : INotifyPropertyChanged
    {
        private int _barometricPressureMeasurementInterval;
        private int _temperatureMeasurementInterval;

        public MeasurementIntervalsSettingsWindowViewModel(ISettingsService settingsService)
        {
            var settings = settingsService.LoadMeasurementIntervalsSettings();
            _temperatureMeasurementInterval = settings.TemperatureInterval;
            _barometricPressureMeasurementInterval = settings.BarometricPressureInterval;
        }

        public ICommand ApplySettingsCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public int TemperatureMeasurementInterval
        {
            get { return _temperatureMeasurementInterval; }
            set
            {
                _temperatureMeasurementInterval = value;
                OnPropertyChanged();
            }
        }

        public int BarometricPressureMeasurementInterval
        {
            get { return _barometricPressureMeasurementInterval; }
            set
            {
                _barometricPressureMeasurementInterval = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}