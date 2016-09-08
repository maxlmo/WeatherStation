using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Properties;
using WeatherStation.ViewModels.UnitSettings.Commands;

namespace WeatherStation.ViewModels.UnitSettings
{
    public class UnitSettingsWindowViewModel : INotifyPropertyChanged
    {
        private readonly Window _currentWindow;
        private TemperatureUnit _temperatureUnit;
        private BarometricPressureUnit _barometricPressureUnit;

        public UnitSettingsWindowViewModel(IEventAggregator eventAggregator, Window currentWindow)
        {
            _currentWindow = currentWindow;
            eventAggregator.GetEvent<SettingsSaved>().Subscribe(SettingsSaved);
            _temperatureUnit = (TemperatureUnit) Settings.Default.TemperatureUnit;
            _barometricPressureUnit = (BarometricPressureUnit) Settings.Default.BarometricPressureUnit;
        }

        private void SettingsSaved(object obj)
        {
            _currentWindow.Close();
        }

        public ICommand ApplySettingsCommand { get; set; }

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
