using Prism.Events;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels
{
    public abstract class HistoryWindowViewModel
    {
        private readonly IDataBaseConnector _dataBaseConnector;
        private readonly IEventAggregator _eventAggregator;

        public HistoryWindowViewModel(IDataBaseConnector dataBaseConnector, IEventAggregator eventAggregator)
        {
            _dataBaseConnector = dataBaseConnector;
            _eventAggregator = eventAggregator;
        }

        public string HistoryWindowName
        {
            get { return "DefaultHistoryWindowName"; }
        }
    }
}
