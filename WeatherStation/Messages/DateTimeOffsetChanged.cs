using System;
using Prism.Events;

namespace WeatherStation.Messages
{
    public class DateTimeOffsetChanged : PubSubEvent<TimeSpan>
    {
    }
}
