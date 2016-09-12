using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation.MVVM
{
    public enum ViewType
    {
        TemperatureHistory,
        BarometricPressureHistory,
        MainWindow,
        UnitSettings
    }
}
