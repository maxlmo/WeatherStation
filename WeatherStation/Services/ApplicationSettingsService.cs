using System;
using WeatherStation.Properties;

namespace WeatherStation.Services
{
    public class ApplicationSettingsService : ISettingsService
    {
        public void SaveDateTimeOffset(TimeSpan offset)
        {
            Settings.Default.TimeSpanOffset = offset;
            Settings.Default.Save();
        }


    }
}
