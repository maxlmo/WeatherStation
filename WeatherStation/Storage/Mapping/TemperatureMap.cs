using FluentNHibernate.Mapping;
using WeatherStation.Model;

namespace WeatherStation.Storage.Mapping
{
    public class TemperatureMap : ClassMap<TemperatureMeasurement>
    {
        public TemperatureMap()
        {
            Id(x => x.Id);
            Map(x => x.Value);
            Map(x => x.TimeStamp);
            Table("Temperature");
        }
    }
}
