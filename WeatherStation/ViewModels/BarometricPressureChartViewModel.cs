using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Services;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels
{
    public class BarometricPressureChartViewModel : INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;
        private double _axisMax;
        private double _axisMin;
        private long _interval;

        public BarometricPressureChartViewModel(IEventAggregator eventAggregator, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            var mapper = Mappers.Xy<BarPressureMeasurement>()
                .X(model => model.TimeStamp.Ticks)
                .Y(model => model.Value);
            Charting.For<BarPressureMeasurement>(mapper);
            ChartValues = new ChartValues<BarPressureMeasurement>();
            eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewBarometricPressureMeasurement);
            eventAggregator.GetEvent<MeasurementIntervalChanged>().Subscribe(MeasurementIntervalChanged);

            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");

            _interval = _settingsService.LoadMeasurementIntervalsSettings().BarometricPressureInterval;
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now, _interval);
        }

        private void MeasurementIntervalChanged(MeasurementIntervalsSettings measurementIntervalsSettings)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _interval = measurementIntervalsSettings.BarometricPressureInterval;
            });
        }

        private void SetAxisLimits(DateTime now, long interval)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(interval + 1).Ticks;
            AxisMin = now.Ticks - TimeSpan.FromSeconds(interval + 10).Ticks;
            AxisStep = TimeSpan.FromSeconds(_interval * 10 + 2).Ticks;
        }

        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        private void NewBarometricPressureMeasurement(BarPressureMeasurement barPressureMeasurement)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var now = DateTime.Now;
                ChartValues.Add(barPressureMeasurement);
                if (ChartValues.Count > 20)
                {
                    ChartValues.RemoveAt(0);
                }
                SetAxisLimits(now, _interval);
            });
        }

        public ChartValues<BarPressureMeasurement> ChartValues { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}