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
using WeatherStation.Storage;

namespace WeatherStation.ViewModels
{
    public class BarometricPressureChartViewModel : INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private double _trend;

        public BarometricPressureChartViewModel(IMeasurementsRepository<BarPressureMeasurement> repository, IEventAggregator eventAggregator)
        {
            var measurements = repository.GetSavedMeasurements().ToList();
            var mapper = Mappers.Xy<BarPressureMeasurement>().X(model => model.TimeStamp.Ticks).Y(model => model.Value);
            Charting.For<BarPressureMeasurement>(mapper);
            ChartValues = new ChartValues<BarPressureMeasurement>();
            eventAggregator.GetEvent<NewBarPressure>().Subscribe(NewBarometricPressureMeasurement);

            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");

            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(20).Ticks; // and 8 seconds behind
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
                SetAxisLimits(now);

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