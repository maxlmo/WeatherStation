using System;

namespace WeatherStation.Model
{
    public class TemperatureMeasurement : IMeasurement
    {
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}