using System;

namespace WeatherStation.Model
{
    public class BarPressureMeasurement : IMeasurement
    {
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}