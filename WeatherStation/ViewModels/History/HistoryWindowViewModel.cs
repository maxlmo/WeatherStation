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
        private readonly IRepository _repository;
        
        public HistoryWindowViewModel(IRepository repository)
        {
            _repository = repository;
        }

        public string HistoryWindowName
        {
            get { return "DefaultHistoryWindowName"; }
        }

        public ObservableCollection<IMeasurement> Measurements
        {
            get { return _repository.Measurements; }
        }
    }
}
