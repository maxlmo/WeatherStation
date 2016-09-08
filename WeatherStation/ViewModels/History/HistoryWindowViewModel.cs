using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using WeatherStation.Model;
using WeatherStation.Storage;

namespace WeatherStation.ViewModels.History
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
