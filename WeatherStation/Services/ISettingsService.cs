using System;

namespace WeatherStation.Services
{
    public interface ISettingsService
    {
        void SaveDateTimeOffset(TimeSpan offset);
    }
}
