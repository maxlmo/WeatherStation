using System;

namespace WeatherStation.Model
{
    public interface IMeasurement
    {
        Guid Id { get; set; }
        double Value { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
