using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherStation.Properties;

namespace WeatherStation.ViewModels
{
    public class DateAndTimeSettingsWindowViewModel : INotifyPropertyChanged
    {
        private string _currentDate;
        private string _currentTime;

        public DateAndTimeSettingsWindowViewModel()
        {
            var dateTimeOffset = DateTime.Now + Settings.Default.TimeSpanOffset;
            _currentDate = dateTimeOffset.ToString("d");
            _currentTime = dateTimeOffset.ToString("t");
        }

        public string CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged();
            }
        }

        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
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
