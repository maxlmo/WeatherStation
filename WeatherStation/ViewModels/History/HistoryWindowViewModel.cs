using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStation.Model;

namespace WeatherStation.ViewModels.History
{
    public abstract class HistoryWindowViewModel
    {
        public string HistoryWindowName
        {
            get { return "DefaultHistoryWindowName"; }
        }

        public ObservableCollection<IMeasurement> Measurements { get; set; }
    }
}
