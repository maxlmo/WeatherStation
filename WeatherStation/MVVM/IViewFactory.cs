using System.Windows;

namespace WeatherStation.MVVM
{
    public interface IViewFactory
    {
        Window CreateMainWindow();
        Window CreateTemperatureHistory();
        Window CreateBarPressureHistory();
        Window CreateUnitSettingsWindow();
    }
}