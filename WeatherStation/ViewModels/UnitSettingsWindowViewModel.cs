using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherStation.Properties;

namespace WeatherStation.ViewModels
{
    public class UnitSettingsWindowViewModel : INotifyPropertyChanged
    {
        private TemperatureUnit _temperatureUnit;
        private BarometricPressureUnit _barometricPressureUnit;

        public UnitSettingsWindowViewModel()
        {
            _temperatureUnit = (TemperatureUnit) Settings.Default.TemperatureUnit;
            _barometricPressureUnit = (BarometricPressureUnit) Settings.Default.BarometricPressureUnit;
        }
        
        public ICommand ApplySettingsCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public string CurrentUnitSettings = "hello";

        public TemperatureUnit CurrentTemperatureUnit
        {
            get { return _temperatureUnit; }
            set
            {
                _temperatureUnit = value;
                OnPropertyChanged();
            }
        }

        public BarometricPressureUnit CurrentBarometricPressureUnit
        {
            get { return _barometricPressureUnit; }
            set
            {
                _barometricPressureUnit = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public enum BarometricPressureUnit
    {
        MBar,
        InHg
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit
    }
}
