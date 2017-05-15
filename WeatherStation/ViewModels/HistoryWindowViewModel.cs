using Prism.Events;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels
{
    public abstract class HistoryWindowViewModel
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        private readonly IEventAggregator _eventAggregator;

        public HistoryWindowViewModel(IMeasurementsRepository measurementsRepository, IEventAggregator eventAggregator)
        {
            _measurementsRepository = measurementsRepository;
            _eventAggregator = eventAggregator;
        }

        public string HistoryWindowName
        {
            get { return "DefaultHistoryWindowName"; }
        }
    }
}
