using System.Windows;
using WeatherStation.Handler;

namespace WeatherStation.MVVM
{
    public interface IViewFactory
    {
        Window CreateMainWindow(ApplicationWindowHandler windowHandler);
        Window CreateTemperatureHistory();
        Window CreateBarPressureHistory();
        Window CreateUnitSettingsWindow();
    }
}