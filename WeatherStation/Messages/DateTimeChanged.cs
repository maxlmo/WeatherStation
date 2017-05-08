using System;
using Prism.Events;

namespace WeatherStation.Messages
{
    public class DateTimeChanged : PubSubEvent<DateTime>
    {
    }
}