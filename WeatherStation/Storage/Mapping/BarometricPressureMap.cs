using FluentNHibernate.Mapping;
using WeatherStation.Model;

namespace WeatherStation.Mapping
{
    public class BarometricPressureMap : ClassMap<BarPressureMeasurement>
    {
        public BarometricPressureMap()
        {
            Id(x => x.Id);
            Map(x => x.Value);
            Map(x => x.TimeStamp);
            Table("BarometricPressure");
        }
    }
}