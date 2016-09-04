using System;

namespace WeatherStation.Model
{
    public interface IMeasurement
    {
        double Value { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
