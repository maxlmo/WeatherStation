using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WeatherStation.ViewModels.UnitSettings
{
    public class UnitSettingsWindowViewModel : INotifyPropertyChanged
    {
        private TemperatureUnit _temperatureUnit;
        private BarometricPressureUnit _barometricPressureUnit;
        
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
