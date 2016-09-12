using WeatherStation.Handler;
using WeatherStation.Views;
using WeatherStation.Views.History;

namespace WeatherStation.MVVM
{
    public interface IViewFactory
    {
        IWindow CreateMainWindow(ApplicationWindowHandler windowHandler);
        IWindow CreateTemperatureHistory();
        IWindow CreateBarPressureHistory();
        IWindow CreateUnitSettingsWindow();
    }
}