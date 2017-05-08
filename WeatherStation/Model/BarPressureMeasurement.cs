using System;

namespace WeatherStation.Model
{
    public class BarPressureMeasurement : IMeasurement
    {
        public virtual Guid Id { get; set; }
        public virtual double Value { get; set; }
        public virtual DateTime TimeStamp { get; set; }
    }
}